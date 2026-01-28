using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// Linq is for sorting lists

public class BistroManager : MonoBehaviour
{
    // Manages the movement queue
    public List<Employee> allCharacters = new List<Employee>();
    public Transform selectionCircle; // Drag your SelectionCircle object here

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Everything below is for my waypoints
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                InteractableObject obj = hit.collider.GetComponent<InteractableObject>();

                if (obj != null)
                {
                    // We clicked a table! Use its waypoint!
                    HandleClick(obj.GetWalkToPoint(), obj);
                    return; // Exit so we don't process it as a floor click
                }
            }

            // If we didn't hit an object, proceed with a normal floor click
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HandleClick(mouseWorldPos, null);
        }
    }

    void HandleClick(Vector3 worldPos, InteractableObject obj)
        {
            Employee bestCandidate = GetNextAvailableEmployee();

            if (bestCandidate != null)
            {
                // Use the centering logic from earlier
                float gridSize = 1.0f;
                float x = Mathf.Floor(worldPos.x / gridSize) * gridSize + (gridSize / 2f);
                float y = Mathf.Floor(worldPos.y / gridSize) * gridSize + (gridSize / 2f);
                Vector3 finalPos = new Vector3(x, y, 0);

                // Should send the employee
                bestCandidate.GoTo(finalPos, obj);
                bestCandidate.hasMovedThisRound = true;
            }
            else
            {
                Debug.Log("Everyone is busy or has already moved!");
            }
        }

    Employee GetNextAvailableEmployee()
        {
            var eligible = allCharacters.Where(c => !c.IsBusy() && !c.hasMovedThisRound).ToList();

            // If everyone has moved, reset the 'round' so they can move again
            if (eligible.Count == 0 && !allCharacters.Any(c => c.IsBusy()))
            {
                foreach (var c in allCharacters) c.hasMovedThisRound = false;
                eligible = allCharacters.Where(c => !c.IsBusy()).ToList();
            }

            return eligible
                .OrderByDescending(c => c.moveSpeedStat)
                .ThenBy(c => c.priorityOrder)
                .FirstOrDefault();
        }

    void UpdateSelectionCircle()
        {
            Employee nextWinner = GetNextAvailableEmployee();

            if (nextWinner != null && selectionCircle != null)
            {
                selectionCircle.gameObject.SetActive(true);
                selectionCircle.position = nextWinner.transform.position;
            }
            else if (selectionCircle != null)
            {
                // Hide if literally everyone is busy/moved
                selectionCircle.gameObject.SetActive(false);
            }
        }

    void LateUpdate() //This ensures the circle follows smoothly
    {
        UpdateSelectionCircle();
    }
    
}
