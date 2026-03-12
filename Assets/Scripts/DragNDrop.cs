using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    // Dragging logic
    Vector3 dragOffset;
    private Camera mCam;
    bool holding;

    // THEORY: AILERP MUST BE DISABLED TO ENABLE SNAPPING & ENSURE CLICKS REGISTER CORRECTLY
    [HideInInspector] public AILerp aiLerp;

    public Customer customer;


    // Snapping logic
    public List<Transform> snapPoints;
    public float snapRange = 0.5f;

    private void Awake()
    {
        mCam = Camera.main;
        aiLerp = GetComponent<AILerp>();
    }

    private void Start()
    {
        customer.GetComponent<Customer>();
    }

    void Update()
    {
        if (holding)
        {
            transform.position = GetMousePos() + dragOffset;
        }

        aiLerp.canMove = false;
    }
    
    private void OnMouseDown()
    {
        holding = true;
        dragOffset = transform.position - GetMousePos();
        Debug.Log("Customer Selected!");
    }

    private void OnMouseUp()
    {
        holding = false;
        SnapObject(this.transform);
    }

    Vector3 GetMousePos()
    {
        var mousePos = mCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    public void SnapObject(Transform obj)
    {
        foreach(Transform point in snapPoints)
        {
            if (Vector2.Distance(point.position, obj.position) <= snapRange)
            {
                obj.position = point.position;
                return;
            }
            else
            {
                transform.position = customer.myTarget.transform.position;
            }
        }
    }


}
