using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ClickToMoveHandler : MonoBehaviour
{
    public Camera cam;
    public Transform targetMarker; // An empty GameObject to act as the destination
    public AILerp aiLerp; // Drag the character's AILerp component here

    void Update()
    {
        // Only allow a new click if the character is NOT currently moving
        if (Input.GetMouseButtonDown(0) && !aiLerp.pathPending && aiLerp.reachedEndOfPath)
        {
            UpdateTargetPosition();
        }
    }

    void UpdateTargetPosition()
    {
        // 1. Convert mouse click to world space
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        worldPos.z = 0; // Keep it on the 2D plane

        // 2. Snap the marker to the center of the clicked grid square
        // This ensures the pathfinding targets the exact middle of a cell
        float gridSize = 1.0f;
        float x = Mathf.Floor(worldPos.x / gridSize) * gridSize + (gridSize / 2f);
        float y = Mathf.Floor(worldPos.y / gridSize) * gridSize + (gridSize / 2f);

        targetMarker.position = new Vector3(x, y, 0);

        // Note: The AIDestinationSetter on your character should be 
        // referencing this targetMarker. It will update automatically!

        // This forces the A* system to recalculate the path immediately
        aiLerp.SearchPath();
    }
}
