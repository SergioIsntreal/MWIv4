using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DragAndDrop : MonoBehaviour
{
    public static bool IsDragging { get; private set; }
    private Vector3 doorPosition;
    private AIDestinationSetter destSetter;
    private CursorManager cursorMgr;
    private Customer customer;


    private void Start()
    {
        cursorMgr = FindFirstObjectByType<CursorManager>();
        customer = GetComponent<Customer>();
    }

    void Update()
    {
      

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
        //if (customer.currentState == Customer_State.Waiting || customer.currentState == Customer_State.Entering)
        //{
            IsDragging = true;
            Debug.Log("Mouse Down");
            //customer.currentState = Customer_State.Dragged;
            cursorMgr.SetGrab(); // Change to grab icon
            if (customer.aiLerp != null) customer.aiLerp.canMove = false;
        //}
    }

    void OnMouseDrag()
    {
        //if (customer.currentState == Customer_State.Dragged)
        //{
        Debug.Log("Dragging");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos; // Follow the finger/mouse
        //}
    }

    /*void OnMouseUp()
    {
        if (customer.currentState == Customer_State.Dragged)
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
                    customer.SeatAtTable(table);
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
    }*/

    void SnapBackToWaitingSeat()
    {
        if (customer.currentSlot != null)
        {
            Debug.Log("No valid table found. Snapping back to waiting seat.");

            // Teleport back to the waiting chair
            transform.position = customer.currentSlot.position;

            // Update the AI target so they don't try to walk back to where you dropped them
            customer.myTarget.transform.position = customer.currentSlot.position;

            customer.currentState = Customer_State.Waiting;

            // Re-enable AI
            if (customer.aiLerp != null)
            {
                customer.aiLerp.enabled = true;
                customer.aiLerp.canMove = true;
            }
        }
    }

}
