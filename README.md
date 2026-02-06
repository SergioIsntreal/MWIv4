# MWIv4
This time, with A*Grid

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
- 
## DL3 (31/01/26) Continuing From Yesterday

**Notes:**
- [FIXED] Combination Stations **require** a BoxCollider2D to register as an Obstacle
- Added a PiggyBank and Money Counter (no functionality just yet)
