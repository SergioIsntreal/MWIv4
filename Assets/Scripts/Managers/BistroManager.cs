using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
// Linq is for sorting lists

public class BistroManager : MonoBehaviour
{
    // Manages the movement queue
    public List<Employee> allCharacters = new List<Employee>();
    public Transform selectionCircle; // Drag your SelectionCircle object here

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ForceResetRound();
        }

        // CURRENT BUG: After dragging a customer, the employees AND customers begin to jitter >_<
        if (Input.GetMouseButtonDown(0))
        {
            if (Customer.IsDragging) return;

            // 1. Perform ONE raycast to see what we hit
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // This mask includes everything EXCEPT the SeatedCustomer layer
            int mask = ~(1 << LayerMask.NameToLayer("SeatedCustomer"));
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, mask);

            if (hit.collider != null)
            {
                // 2. Check: Is it a Customer?
                if (hit.collider.GetComponent<Customer>() != null)
                {
                    Debug.Log("Clicked a Customer - Handing control to Customer script.");
                    return; // STOP HERE. The Customer script will handle its own OnMouseDown.
                }

                // 3. Check: Is it a Table or Station?
                InteractableObject obj = hit.collider.GetComponent<InteractableObject>();
                HandleClick(mousePos, obj);
            }

            // If we didn't hit an object, proceed with a normal floor click
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HandleClick(mouseWorldPos, null);
        }
    }

    public void HandleClick(Vector3 worldPos, InteractableObject obj)
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
            bestCandidate.hasMovedThisRound = true;
            bestCandidate.GoTo(finalPos, obj);
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

    public void ForceResetRound()
    {
        Debug.Log("Manually resetting all employees.");
        foreach (var c in allCharacters)
        {
            c.StopMoving(); // Tell employees to stop
            c.hasMovedThisRound = false;
        }
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
