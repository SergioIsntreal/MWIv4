using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Customer : MonoBehaviour
{
    public static bool IsDragging { get; private set; }

    public enum CustomerState { Entering, Waiting, Dragged, Seated, Eating, Paying, Leaving }
    public CustomerState currentState = CustomerState.Entering;

    private Transform currentSlot;
    private Vector3 doorPosition;
    private AILerp aiLerp;
    private AIDestinationSetter destSetter;
    private GameObject myTarget;
    private TableStation assignedTable;

    private CursorManager cursorMgr;

    void Awake()
    {
        cursorMgr = FindFirstObjectByType<CursorManager>();

        aiLerp = GetComponent<AILerp>();
        destSetter = GetComponent<AIDestinationSetter>();

        // Check if the components are actually there
        if (aiLerp == null || destSetter == null)
        {
            Debug.LogError($"Missing AI Components on {gameObject.name}! Make sure AILerp and AIDestinationSetter are attached to the prefab.");
            return;
        }

        // Create the target
        myTarget = new GameObject(name + "_Target");
        destSetter.target = myTarget.transform;

        doorPosition = transform.position; // Spawns at door
    }

    void Start()
    {
        // 1. Force the AI to be ready to move
        if (aiLerp != null)
        {
            aiLerp.canMove = true;
            aiLerp.canSearch = true;
        }

        // 2. Find a seat immediately
        MoveToWaitingArea();
    }

    void Update()
    {
        // DEBUG: Check if we have arrived at the waiting chair
        if (currentState == CustomerState.Entering)
        {
            float dist = Vector3.Distance(transform.position, myTarget.transform.position);

            // If we are close to the chair (within 0.2 units)
            if (dist < 0.2f)
            {
                Debug.Log($"[Customer] {gameObject.name} arrived at Waiting Chair. Switching state to WAITING.");
                currentState = CustomerState.Waiting;
                // This state change is what triggers the Patience Timer!
            }
        }
    }

    void MoveToWaitingArea()
    {
        if (WaitingAreaManager.Instance == null)
        {
            Debug.LogError("WaitingAreaManager is missing from the scene!");
            return;
        }

        currentSlot = WaitingAreaManager.Instance.GetClosestSlot(transform.position);

        if (currentSlot != null)
        {
            myTarget.transform.position = currentSlot.position;
            currentState = CustomerState.Entering;

            Debug.Log($"[Customer] {gameObject.name} found a seat at {currentSlot.position}. Walking there now...");

            if (aiLerp != null) aiLerp.SearchPath();
        }
        else
        {
            Debug.LogWarning("[Customer] Cafe full! Leaving immediately.");
            LeaveBistro();
        }
    }

    public void SeatAtTable(TableStation table)
    {
        assignedTable = table;

        // Teleport to seat anchor
        Vector3 seatPos = table.GetSeatPosition();
        transform.position = seatPos;

        // Keep the AI target in the same spot just in case
        myTarget.transform.position = seatPos;

        // Occupy Table
        table.isOccupied = true;
        table.currentCustomer = this;
        currentState = CustomerState.Seated;

        // Visuals & Layer
        this.gameObject.layer = LayerMask.NameToLayer("SeatedCustomer");

        // Reset and Restart Patience for the "Waiting for Order" phase
        CustomerPatience patience = GetComponent<CustomerPatience>();
        if (patience != null)
        {
            patience.ResetPatience();
            patience.enabled = true;
        }

        // Notify Table
        table.MarkForOrder();

        Debug.Log($"Customer snapped to table at {seatPos}");
    }

    public void LeaveBistro()
    {
        // 1. If they were in a chair, free it up for the next person
        if (currentSlot != null)
        {
            WaitingAreaManager.Instance.ReleaseSlot(currentSlot);
            currentSlot = null;
        }

        // 2. If they were at a table, free that up too
        // (We'll need to find which table they were at)
        if (assignedTable != null)
        {
            assignedTable.isOccupied = false;
            assignedTable.currentCustomer = null;
            assignedTable = null;
        }

        // 3. Re-enable AI
        if (aiLerp != null)
        {
            aiLerp.enabled = true;
            aiLerp.canMove = true;
            aiLerp.Teleport(transform.position);
        }

        if (destSetter != null) destSetter.enabled = true;

        // 4. Change Layer back to default/Customer so they sort correctly
        gameObject.layer = LayerMask.NameToLayer("Customer");

        // 5. Move to the door (using your saved doorPosition)
        myTarget.transform.position = doorPosition;
        currentState = CustomerState.Leaving;

        // 6. Self-destruct once they reach the door
        StartCoroutine(DestroyAtDoor());
    }

    private System.Collections.IEnumerator DestroyAtDoor()
    {
        // Wait until they are close to the door position
        while (Vector3.Distance(transform.position, doorPosition) > 0.5f)
        {
            yield return null;
        }
        Destroy(myTarget); // Clean up the invisible target
        Destroy(gameObject);
    }

    // DRAG AND DROP LOGIC

    void OnMouseEnter()
    {
        if (!IsDragging) cursorMgr.SetHover();
    }

    void OnMouseExit()
    {
        if (!IsDragging) cursorMgr.SetDefault();
    }

    void OnMouseDown()
    {
        if (currentState == CustomerState.Waiting || currentState == CustomerState.Entering)
        {
            IsDragging = true;
            currentState = CustomerState.Dragged;
            cursorMgr.SetGrab(); // Change to grab icon
            if (aiLerp != null) aiLerp.canMove = false;
        }
    }

    void OnMouseDrag()
    {
        if (currentState == CustomerState.Dragged)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos; // Follow the finger/mouse
        }
    }

    void OnMouseUp()
    {
        if (currentState == CustomerState.Dragged)
        {
            IsDragging = false;

            // Create a LayerMask for your "Stations" layer (assuming Tables are on Layer 7)
            int stationLayerMask = LayerMask.GetMask("Stations");

            // 1. Look for a TableStation within a small radius of the drop point
            Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.5f, stationLayerMask);

            if (hit != null)
            {
                TableStation table = hit.GetComponent<TableStation>();

                // 2. If it's a table and it's not already taken...
                if (table != null && !table.isOccupied)
                {
                    SeatAtTable(table);
                    GetComponent<CustomerPatience>().UpdateOriginalPosition();
                }
                else
                {
                    SnapBackToWaitingSeat();
                }
            }
            else
            {
                SnapBackToWaitingSeat();
            }
        }

        cursorMgr.SetDefault();
    }

    void SnapBackToWaitingSeat()
    {
        if (currentSlot != null)
        {
            Debug.Log("No valid table found. Snapping back to waiting seat.");

            // Teleport back to the waiting chair
            transform.position = currentSlot.position;

            // Update the AI target so they don't try to walk back to where you dropped them
            myTarget.transform.position = currentSlot.position;

            currentState = CustomerState.Waiting;

            // Re-enable AI
            if (aiLerp != null)
            {
                aiLerp.enabled = true;
                aiLerp.canMove = true;
            }
        }
    }
}
