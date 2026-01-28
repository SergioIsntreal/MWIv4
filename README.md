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

## DL2 (29/01/26)
