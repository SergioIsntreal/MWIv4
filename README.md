## Script Tracker
### "Bistro Manager"
- Selects the'fastest available Employee' and sends them to the clicked area
- Determines the movement order
- A line in Update prevents movement them holding down instead of clicking LMB

### "ClickToMoveHandler"
- Creates an empty GameObject that will appear where you click
- This invisible GameObject will be what characters move towards
- When you click it determines which tile and sends the character to the centre of that tile

### "CursorManager"
- Sets the appearance of your texture depending on what action you're performing (eg. clicking, hovering, grabbing)

### "Customer"
- `void Awake()` creates 'myTarget' GameObject for the Customers, so they will move to a location based on their current state
- `void Awake()` creates a 'transform.position' reference for the door (so that they can leave and despawn)
- `void Start()` calls "MoveToWaitingArea()"
- `void Update()` checks that if the Customer's state is 'Entering' that they move to the waiting area, only stopping if they're within a certain distance of the target
- `MoveToWaitingArea()` communicates with "WaitingAreaManager" to find the closest empty chair to walk to; if no chairs are available, they will leave
- `SeatAtTable(TableStation table)` snaps the Customer to the TableSeat waypoint, changes the state of the Table to 'Occupied' and the state of the Customer to 'Seated'
- There's a line of code changing the gameObject layer, but not sure what this is for
- CustomerPatience is reset
- Tells the "TableStation" script to 'MarkForOrder()'
- `LeaveBistro()` sets the Chair or Table back to 'Empty' if a leaving Customer had been occupying it
- The AI Lerp is re-enabled, the door position is called and `StartCoroutine(DestroyAtDoor())` is called
- `DestroyAtDoor()`removes the Customer and their invisible target from the Scene
- 'Drag and Drop Logic' changes the visuals of the cursor, enables dragging/disables movement when they're 'Waiting' or 'Entering', and controls what occurs when the CUstomer is being dragged (self explanatory, I know)
- `snapBackToWaitingSeat` teleports the Customer back to their seat if not making contact with an available table
> [!NOTE]
> The Customer's AI is re-enabled, I want to test if this is necessary OR causing them to walk when they're not supposed to

### "CustomerPatience"
- `void Start()` sets Customer's patience to max, starts turning them red when 1/3 remains, and they turn fully red when it's at 1/6, also the bubble is deactivated
- `void Update()` controls the food bubble visibility, processes the patience meter when the Customer is 'Waiting' or 'Seated', and resets the Customer back to white if they're 'Eating'
- `UpdateVisuals()` controls the screen shake and the Customer's colour, depending on the patience and if they have been moved
- `UpdateBubbleVisibility()` only enables the food bubble when the Customer is 'Seated' (at a table)
- `SetOrderVisuals(string foodName)` has a switch for the food displayed
> ^ UNFINISHED
- `ResetPatience()` does exactly what is says on the tin
- `UpdateOriginalPosition` I thiiiink is a reference for when their position changes the drag&drop logic from `Customer`
> ^ NEEDS CONFIRMATION

### "CustomerSpawner"
- `void Start()` makes the first Customer always spawns within the first 3 seconds
- `void Update()` only runs if the `timeManager` is working and the Bistro is Open. The timer counts upwards, so if it exceeds 'nextSpawnTime', spawn a random customer, randomise the spawn timer and reset spawn time

### "DragAndDrop"
- `DragAndDrop` logic, to be assigned to the Food. Customers have a similar logic
> TESTING REQUIRED

### "Employee"
- The script communicates with the `InteractableObjects` and `TableStation` scripts
- `void Awake()` contains the 'AI Lerp' and 'destSetter'. A private target is created for each individual Employee and will be set at their current position when the Level starts
- `void GoTo(Vector3 position, InteractableObject obj = null)` controls when the AI Lerp is active, whether 'isMoving' is true and where they are going
- `GoToTable(TableStation table)` stops all coroutines and starts coroutine `TakeOrderRoutine(table)`
> [!Warning]
> This code is NOT being called! Needs an IF statement that registers when a player clicks the table
- `IEnumerate TakeOrderRoutine(table)` moves the Employee to the assigned waypoint, initiates taking order if the table requires one, and updates the bubble to randomise and show the order generated
- `void Update()` calls `HandleFlipping()` (which changes the direction they're facing when moving), calls `StartWorking()` if they arrive at an interactable object + `StopMoving()` and starts the progress bar for interactions
- `IsBusy()` prevents the Employee from moving if they are engaged in a task or travelling to their destination

### "FoodStation"
- Spawns the food when the timer from the `InteractableObject` is complete

### "InteractableObject"
- To be given to the Tables, Food Stations and Till
- `ShouldIStartInteraction()` checks if an Employee has arrived at a table or foodstation, and if it requires them to interact, which tells the `Employee` script to initiate `StartWorking()`
> [!NOTE]
> Requires an IF statement to check if the Table needs their order taken (I think?)
- Structured so that the interaction only triggers when the assigned employee reaches the waypoint
- When the interaction is complete, tells FoodStation to `FinishCooking()`

### "TableStation"
- Controls and monitors whether the table is occupied or needs their order taken
- Also registers if the Employee has arrived
- `MarkForOrder` sets 'needsOrder' to true
- uses `OnTriggerEnter/Exit2D` to register if the Employee has arrived
> [!Warning]
> The OnTriggers aren't called and remains untested!

### "TimeManager"
- Linked to the 'TimeText' GameObject
- Handles how fast the day progresses, has statuses that determine if the `CustomerSpawner` will be active or not and changes the text above the timer to 'Closed' at the end of the shift

### "WaitingAreaManager"
- Tracks which chairs are available, which chairs are taken, finds the closest available chair to the door, and releases the chair when a Customer storms off or is dragged to a Table

# MWIv4 Entries

## DL1 (28/01/26) A* Grid & Employee Movement
Unfortunately, my chat history with Gemini was erased when I logged off, so I'm gonna have to try and make sense of what I have - not that it'll be very hard, it's got a lot of annotations to clue me in. Worth noting, the main reason this attempt at gridded movement was successful was because Gemini directed me to where to download the script. Very helpful.

I have a total of 5 scripts thus far: **BistroManager** (Manages the movement queue via List, also sends the employees to the waypoints of selected ovjects (otherwise the tile clicked)), **ClickToMove** (Updates the target position to where the player clicks), **Employee** (currently holds stats that determine their movement order, the beginnings of an interaction script and a few lines that allow the sprite to face whichever direction it;s travelling), **GridMovement** (Helps to detect obstacles and manuevre characters around the grid), and **InteractableObject** (Still being developed; it has an enum with every possible interaction for the employees).

Vague Notes:
- Gemini claims I can use one master script for all interactions, rather than having one script per interaction.
- I also have a movement circle implemented, that sits below the character's feet if they're the next one who'll move
- The grid itself is 13 by 7, with each node being 1.1
- I'll need to port over that DragAndDrop script from my OG project

Next On The Agenda:
- Create the Level Timer & UI
- Create the customer randomiser, spawner and movement script
- Get the food station interaction to spawn food

## DL2 (30/01/26) Immediate Issues
I'll probably only document game-breaking bugs from here on out.

**Current Bugs That Require Immediate Attention Before Proceeding:**
- All Characters begin jittering the moment you click anything
- When an Employee times out, they run to the most recent click position (This breaks the "Round" (Order of Employee Movement))
- Clicking outside the Grid causes the Employees to breach containment and travel outside their designated area (Do I need to add the UI as Collision Layers?)
- When a Customer is dragged to a table, they Timeout and follow the click positions, similar to the Employees (Is this because they share movement code?)
- Waypoints for the FoodStations and Till are broken; then proceed to break the Table waypoints (originally work fine)
- When 1 Customer is bugged, any Customer you try to seat after will move towards the recent click position (until their patience runs out and they leave)
- There is a delay when the MovementCircle switches targets (likely due to a delay with the Round Reset)
- CombinationStations need Waypoints

**Attempted Fixes:**
- Removed the BoxCollider2D from all CombinationStations (scrapping functionality)
- Reworking the GridMovement script to be a slave script, that obeys the BistroManager and Customer scripts
- BistroManager and Customer scripts updated to accomodate GridMovement
- Gemini theorizes that the jittering is due to the GridMovement and AILerp fighting for control; **Deleting GridMovement script.**

[NOTE: HOLY SHIT THE JITTERING IS GONE...]

- Jittering fixed, the Customers and Employees do not Timeout nearly as much as they were before. Need to reinstate the code that disables the Employees from running to your click position when dragging & dropping a Customer
- FoodStation Waypoints are still not working as intended, though the Employees are not Timing Out
- Clicking a CombinationStation causes the Obstacle Detection to break; it becomes a passive object (unsure what's causing that tbh)

**Bugs To Return To Later:**
- The Employees will still try to follow your click if it falls outside the grid boundary
- The delay between the Round Reset is noticable
- CombinationStations lose their 'Obstacle' status when clicked on
- FoodStation Waypoints aren't working; Employees still tru to travel to the centre of the Collider (may need to move that actually)
  
## DL3 (31/01/26) Continuing From Yesterday

**Notes:**
- [FIXED] Combination Stations **require** a BoxCollider2D to register as an Obstacle
- Added a PiggyBank and Money Counter (no functionality just yet)

## DL4 (06/02/26) Troubleshooting

**Current Bugs I want to fix before proceeding:**
- I want to disable the bubble that appears above their head when they're waiting, and instead have a clock icon that appears next to them when they have 1/3 patience left, the icon pulsing slightly
- Employees can walk off the grid/designated area

**Progress Notes:**
- Customers now snap to the centre of the table and teleport back to the chair if they aren't dragged to an empty table
- Rounds have been swapped with a Queue, which makes the transition a bit more seamless with the employee movement turn order
- The Customers now turn red and vibrate when their patience is depleting (which has spawned its own bugs)

I'm going to call it quits for now; I don't have the braincells to understand the code, and I've stopped trying to. AI can only understand so much, even with constant snapshots of the code. In all honesty, I wish I didn't have to bother with this. I don't want to ask for help and I'm damn near tempted to let it all slip between my fingers like sand. Doesn't want to do anything artistic, doesn't want to do any problem solving, what am I honestly good for?

## DL5 (07/02/26) Troubleshooting

Let's try this again.

**Exactly 4 hours later**

Nah, I'm still done with this shit. I'll assume I'm inputting something incorrectly in the Inspector rather than the code, but at this point? I'm too tired for this shit. No, I don't understand what I'm copying and pasting, and yes, I miss when everything was working. It seems the more I add, the more it overcomplicates itself.
