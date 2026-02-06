using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableStation : MonoBehaviour
{
    public Transform seatAnchor; // Drag a child GameObject here

    public bool isOccupied = false;
    public bool needsOrder = false; // New: Employee looks for this
    public string currentOrder;

    // Reference to the customer
    public Customer currentCustomer;

    public Vector3 GetSeatPosition()
    {
        return seatAnchor != null ? seatAnchor.position : transform.position;
    }

    public void MarkForOrder()
    {
        needsOrder = true;
        // You could also trigger a "!" icon on the table here
    }
}
