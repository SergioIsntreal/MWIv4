using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableStation : MonoBehaviour
{
    public Transform seatAnchor; // Drag a child GameObject here

    public bool isOccupied = false;
    public bool needsOrder = false; // New: Employee looks for this
    public string currentOrder;

    private bool waiterHasArrived = false;

    // Reference to the customer
    public Customer currentCustomer;

    public Vector3 GetSeatPosition()
    {
        return seatAnchor != null ? seatAnchor.position : transform.position;
    }

    public bool HasWaiterArrived()
    {
        return waiterHasArrived;
    }

    public void MarkForOrder()
    {
        needsOrder = true;
        // You could also trigger a "!" icon on the table here
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Employee"))
        {
            if (collision.GetComponent<Employee>() == GetComponent<InteractableObject>().GetAssignedWaiter())
            {
                Debug.Log("Assigned Waiter.");
                waiterHasArrived = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Employee"))
        {
            if (collision.GetComponent<Employee>() == GetComponent<InteractableObject>().GetAssignedWaiter())
            {
                Debug.Log("Assigned Waiter.");
                waiterHasArrived = false;
            }
        }
    }
}
