using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;
    public float gridSize = 1f;

    [Header("Detection")]
    public LayerMask obstacleLayer; // Set this to "Obstacle" in the Inspector

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        // Snap the character to the grid immediately on start
        targetPosition = SnapToGrid(transform.position);
        transform.position = targetPosition;
    }

    void Update()
    {
        // 1. Listen for Mouse Click
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }

        // 2. Move towards the target
        MoveCharacter();
    }

    void SetTargetPosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3 snappedPos = SnapToGrid(mouseWorldPos);

        // Check if the clicked square is blocked
        // We use a small radius (e.g., 0.4f for a 1.0f grid) to check the center of the square
        Collider2D hit = Physics2D.OverlapCircle(snappedPos, gridSize * 0.4f, obstacleLayer);

        if (hit == null)
        {
            // Path is clear!
            targetPosition = snappedPos;
            isMoving = true;
        }
        else
        {
            Debug.Log("That spot is blocked by: " + hit.name);
        }
    }

    Vector3 SnapToGrid(Vector3 position)
    {
        // Calculate the grid coordinates
        float x = Mathf.Floor(position.x / gridSize) * gridSize;
        float y = Mathf.Floor(position.y / gridSize) * gridSize;

        // Offset by half gridSize to move to the center of the square, not the corner
        return new Vector3(x + (gridSize / 2f), y + (gridSize / 2f), 0);
    }

    void MoveCharacter()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop moving once we are close enough to the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }


}
