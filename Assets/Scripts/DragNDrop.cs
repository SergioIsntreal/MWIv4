using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    public static bool IsDragging { get; private set; }

    private CursorManager cursorMgr;
    Customer _customer;
    private AIDestinationSetter destSetter;

    private void Awake()
    {
        // cursorMgr = FindFirstObjectByType<CursorManager>();
    }

    private void Update()
    {
        cursorMgr = FindFirstObjectByType<CursorManager>();
        _customer = GetComponent<Customer>();
    }

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
        Debug.Log("Mouse Down");
        IsDragging = true;
        cursorMgr.SetGrab(); // Change to grab icon
        //_customer.aiLerp.canMove = false;
    }

    void OnMouseDrag()
    {
        Debug.Log("Dragging");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _customer.currentState = Customer_State.Dragged;
        _customer.aiLerp.canMove = false;
        mousePos.z = 0;
        transform.position = mousePos; // Follow the finger/mouse
    }

    void OnMouseUp()
    {
        // 1. Look for a TableStation within a small radius of the drop point
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.5f);

        if (hit != null)
        {
            TableStation table = hit.GetComponent<TableStation>();

            // 2. If it's a table and it's not already taken...
            if (table != null && !table.isOccupied)
            {
                _customer.SeatAtTable(table);
                GetComponent<CustomerPatience>().UpdateOriginalPosition();
            }
            else
            {
                Debug.Log("Mouse Up");
                SnapBackToWaitingSeat();
            }
        }
        else
        {
            Debug.Log("Mouse Up");
            SnapBackToWaitingSeat();
        }
    }

    public void SnapBackToWaitingSeat()
    {   
        _customer.currentState = Customer_State.Waiting;
        IsDragging = false;
        cursorMgr.SetHover();
        _customer.aiLerp.enabled = true;
        _customer.aiLerp.canMove = true;
        _customer.aiLerp.autoRepath.mode = AutoRepathPolicy.Mode.Never;

        Debug.Log("No valid table found. Snapping back to waiting seat.");
        // Teleport back to the waiting chair
        transform.position = _customer.currentSlot.position;
        destSetter.target.position = _customer.currentSlot.position;
        //destSetter.target = myTarget.transform;
        
    }
}
