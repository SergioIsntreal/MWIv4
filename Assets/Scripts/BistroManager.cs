using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// Linq is for sorting lists

public class BistroManager : MonoBehaviour
{
    public List<Employee> allCharacters = new List<Employee>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Customer.IsDragging) return;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                InteractableObject obj = hit.collider.GetComponent<InteractableObject>();
                if (obj != null)
                {
                    HandleClick(obj.GetWalkToPoint(), obj);
                    return;
                }
            }

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HandleClick(mouseWorldPos, null);
        }
    }

    void HandleClick(Vector3 worldPos, InteractableObject obj)
    {
        Employee bestCandidate = GetNextAvailableEmployee();

        if (bestCandidate != null)
        {
            float gridSize = 1.0f;
            float x = Mathf.Floor(worldPos.x / gridSize) * gridSize + (gridSize / 2f);
            float y = Mathf.Floor(worldPos.y / gridSize) * gridSize + (gridSize / 2f);
            Vector3 finalPos = new Vector3(x, y, 0);

            // Set this BEFORE calling GoTo to ensure the selection circle moves immediately
            bestCandidate.hasMovedThisRound = true;
            bestCandidate.GoTo(finalPos, obj);
        }
        else
        {
            Debug.Log("Everyone has already moved this round!");
        }
    }

    Employee GetNextAvailableEmployee()
    {
        // 1. First, try to find someone who hasn't moved yet AND isn't busy.
        // This maintains the 1-2-3-4 order.
        var nextInQueue = allCharacters
            .Where(c => !c.hasMovedThisRound && !c.IsBusy())
            .OrderByDescending(c => c.moveSpeedStat)
            .ThenBy(c => c.priorityOrder)
            .FirstOrDefault();

        // 2. If everyone has 'hasMovedThisRound = true', check if we can reset the round.
        if (nextInQueue == null)
        {
            // We only allow a reset if the FIRST person in the priority chain is no longer busy.
            // This prevents the "target switching" bug because the busy person isn't eligible yet.
            bool canResetRound = allCharacters.Any(c => !c.IsBusy());

            if (canResetRound)
            {
                // Reset 'hasMovedThisRound' ONLY for employees who are actually standing still.
                foreach (var c in allCharacters)
                {
                    if (!c.IsBusy())
                    {
                        c.hasMovedThisRound = false;
                    }
                }

                // Try the search again now that we've freed up the idle employees.
                nextInQueue = allCharacters
                    .Where(c => !c.hasMovedThisRound && !c.IsBusy())
                    .OrderByDescending(c => c.moveSpeedStat)
                    .ThenBy(c => c.priorityOrder)
                    .FirstOrDefault();
            }
        }

        return nextInQueue;
    }
}
