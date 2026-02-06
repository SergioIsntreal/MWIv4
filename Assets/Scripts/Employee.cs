using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Employee : MonoBehaviour
{
    [Header("Hierarchy Details")]
    public string characterName;
    public int moveSpeedStat; //Higher = Faster
    public int priorityOrder; // Tie-breaker (Lower number moves First)

    //This variable tracks turns
    [HideInInspector] public bool hasMovedThisRound = false;

    private AILerp aiLerp;
    public string[] menu = { "Soup", "Burger", "Salad", "Ice Cream" };
    private AIDestinationSetter destSetter;
    private GameObject myInternalTarget; // Individual to each employee

    // For interactable objects
    private InteractableObject currentTaskObject;
    private float taskTimer = 0;
    private bool isWorking = false;

    // For timing out the employee if they get stuck
    public float movementTimeout = 10.0f; // Max time allowed to reach destination
    private float movementTimer = 0f;
    private bool isMoving = false;

    // For flipping the sprite when they move
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        aiLerp = GetComponent<AILerp>();
        destSetter = GetComponent<AIDestinationSetter>();

        // Create a private target just for this character
        myInternalTarget = new GameObject(gameObject.name + "_Target");
        destSetter.target = myInternalTarget.transform;

        // Start at current position so they don't run away at start
        myInternalTarget.transform.position = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void GoTo(Vector3 position, InteractableObject obj = null)
    {
        // Wake up the AI
        if (aiLerp != null)
        {
            aiLerp.canMove = true;
            aiLerp.canSearch = true;
        }
        if (destSetter != null) destSetter.enabled = true;

        isMoving = true;
        movementTimer = 0f; // Reset timer when a new command starts

        aiLerp.canMove = true;

        currentTaskObject = obj; // This stores the table/station info
        myInternalTarget.transform.position = position;
        aiLerp.SearchPath();
        
        isWorking = false;
    }

    public void GoToTable(TableStation table)
    {
        StopAllCoroutines(); // Cancel previous tasks
        StartCoroutine(TakeOrderRoutine(table));
    }

    private IEnumerator TakeOrderRoutine(TableStation table)
    {
        // 1. Move to the table
        myInternalTarget.transform.position = table.transform.position;

        // 2. Wait until we are close enough
        while (Vector3.Distance(transform.position, table.transform.position) > 0.6f)
        {
            yield return null;
        }

        // 3. Take the Order
        if (table.needsOrder)
        {
            string choice = menu[Random.Range(0, menu.Length)];
            table.currentOrder = choice;
            table.needsOrder = false;

            Debug.Log($"Employee took order: {choice}");

            // Tell the customer to change state to 'WaitingForFood'
            table.currentCustomer.currentState = Customer.CustomerState.Eating; // Or a new 'WaitingForFood' state

            // 4. Update the Customer's bubble to show the FOOD they want
            // You'll need a way to map the string 'choice' to a sprite
            table.currentCustomer.GetComponent<CustomerPatience>().SetOrderVisual(choice);
        }
    }

    void Update()
    {
        HandleFlipping();

        // Check if arrived at the task
        if (currentTaskObject != null && !isWorking && aiLerp.reachedEndOfPath)
        {
            StartWorking();
        }

        if (isMoving)
        {
            movementTimer += Time.deltaTime;

            // If they take too long, force stop
            if (movementTimer >= movementTimeout)
            {
                Debug.LogWarning(gameObject.name + " got stuck! Timing out.");
                StopMoving();
                return;
            }
        }

        if (isWorking)
        {
            taskTimer += Time.deltaTime;
            if (taskTimer >= currentTaskObject.timeToComplete)
            {
                FinishWorking();
            }
        }
    }

    public void StopMoving()
    {
        isMoving = false;
        isWorking = false; // Ensure they aren't stuck in a work state
        movementTimer = 0f;
        hasMovedThisRound = true;

        // 1. Completely disable the A* components to stop all background math
        if (aiLerp != null)
        {
            aiLerp.canMove = false;
            aiLerp.canSearch = false; // Stop it from looking for paths
            aiLerp.SetPath(null);     // Clear the current path
        }

        if (destSetter != null) destSetter.enabled = false;

        // 2. Snap to Grid
        float gridSize = 1.0f;
        float x = Mathf.Floor(transform.position.x / gridSize) * gridSize + (gridSize / 2f);
        float y = Mathf.Floor(transform.position.y / gridSize) * gridSize + (gridSize / 2f);
        Vector3 snappedPos = new Vector3(x, y, 0);

        transform.position = snappedPos;
        myInternalTarget.transform.position = snappedPos;
    }

    void StartWorking()
    {
        isWorking = true;
        taskTimer = 0;
        Debug.Log(characterName + " is now working...");
    }

    void FinishWorking()
    {
        isWorking = false;
        isMoving = false;
        currentTaskObject = null;
        Debug.Log(characterName + " finished the task!");

        // They should be made to stand still or return to idle once completed
    }

    public bool IsBusy()
    {
        // If we are currently walking OR currently doing a task (like cooking/cleaning)
        return isMoving || isWorking;
    }

    void HandleFlipping()
    {
        // Only flip if we are actually moving
        if (IsBusy())
        {
            // If the target is to the left of the character
            if (myInternalTarget.transform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true; // Faces left
            }
            else if (myInternalTarget.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false; // Faces right
            }
        }
    }
}
