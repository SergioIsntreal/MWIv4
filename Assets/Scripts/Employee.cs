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
    private AIDestinationSetter destSetter;
    private GameObject myInternalTarget; // Individual to each employee

    // For interactable objects
    private InteractableObject currentTaskObject;
    private float taskTimer = 0;
    private bool isWorking = false;


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
        currentTaskObject = obj; // This stores the table/station info
        myInternalTarget.transform.position = position;
        aiLerp.SearchPath();
        isWorking = false;
    }

    void Update()
    {
        HandleFlipping();

        // Check if arrived at the task
        if (currentTaskObject != null && !isWorking && aiLerp.reachedEndOfPath)
        {
            StartWorking();
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

    void StartWorking()
    {
        isWorking = true;
        taskTimer = 0;
        Debug.Log(characterName + " is now working...");
    }

    void FinishWorking()
    {
        isWorking = false;
        currentTaskObject = null;
        Debug.Log(characterName + " finished the task!");

        // They should be made to stand still or return to idle once completed
    }

    public bool IsBusy()
    {
        // Character is busy if they haven't reached their personal target
        return !aiLerp.reachedEndOfPath || aiLerp.pathPending;
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
