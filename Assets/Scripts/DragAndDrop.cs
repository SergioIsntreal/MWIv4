using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public LayerMask layerMask;
    public LayerMask releaseLayerMask;

    private bool dragging = false;

    private Transform selectedObject;
    private Vector3 worldPosition;

    private Vector3 offset;
    public bool snapBack = false;
    private Vector3 origin;
    [HideInInspector] public bool movingBack = false;
    public float snapBackSpeed;
    public int orderIndex;

    private void Start()
    {
        origin = transform.position;
    }

    void Update()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 1f, layerMask);
            if (hit.collider == null) { return; }
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                print(hit.collider.name);
                selectedObject = hit.collider.transform;
                offset = worldPosition - selectedObject.position;
                dragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            movingBack = true;
            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 1f, releaseLayerMask);
            //if (hit.collider != null && hit.collider.transform.GetChild(0).GetComponent<Table>().orderIndex == orderIndex)
            //{
            //    print("Order served");

            //}
            //else
            //{
            //    transform.position = origin;
            //}
        }

        if (dragging)
        {
            selectedObject.position = worldPosition - offset;
        }
        if (snapBack && movingBack)
        {
            float step = snapBackSpeed * Time.deltaTime;
            SnapBack(step);
        }

    }

    void SnapBack(float step)
    {
        transform.position = Vector2.MoveTowards(transform.position, origin, step);
        if (Mathf.Approximately(Vector2.Distance(transform.position, origin), 0))
        {
            movingBack = false;
        }
    }
}
