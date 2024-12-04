README
NOTES: Tiles that are black are not walkable for the ghost, so they avoid them. Move the player by using the arrow keys.
Sometimes a wall spawns on top of a ghost or the player. This prevents the algorithm from working so when it happens just rerun the scene and should fix it.

Steps to Open the project

1. Download Unity Hub and make an account (it's free)

2. Once it's finished downloading, Unity Hub will have an Add Project button click it and select Add Project from Disk

3. Find where the project was downloaded to, and then click Add Project

4. it may download the most recent version of the Unity Game Engine and the project won't launch. 

5. Go to installs in Unity Hub Select Install editor in the top right

6. click the archive tab and the follow to the link to the archive

7. Select the 2022 year and the version we used is the 2022.3.18f1 or you can just look for date Jan 20, 2024 and install that version

8. once it's downloaded just click the project in the project tabs. It will take a long time to fully open (it always does)




Steps to run the project

1. In the project Tab, there is a folder called assets. Click it and there will another folder called Scenes. There should be an something called sample scene in it, click it.

2. There should now be all the characters in the that run their algorithms

3. To run it, there is a play button at the top of the window click it and then both of the windows in the center will run the programs.




To See the code

1. In the same assets folder as before, there is another folder called Script click it.

2. Double click the code you want to see. 
	For Breadth First Search the scripts are BreadthFirstSearch and Inky
	For Depth First Search the scripts is Depth First Search
	For A* the script is AStar
	For Linear the script is LinearSearch
	The scripts GridManager and Tile are used to create the grid and referenced by some code
	All scripts with Timer are just to display how long it takes to run

If you don't want to use Unity to see the code, you can just find the project in your files and navigate to the scripts folder to open them. The only scripts that pertain to how the algorithms run are the ones mentioned above


To Increase the Grid Size

1. In the hierarchy on the right side of the menu there a bunch of cube icons. One of them has Grid Manager by it click it.

2. It should open in the inspector and there is Grid Manager (Script). There should be width 15 and length 10. you can just increase those values and the grid will scale accordingly

This will give a better view of how long each of the algorithms take as the current size is just small enough that the algorithms can swiftly process it



To limit which algorithm is running

1. In the hierarchy there is another little cube icon with Ghost next to it click it and it will expand to show all the ghost.

2. The algorithms attached to each are as follows:
		Inky is Breadth First Search
		Blinky is A*
		Pinkie is Depth Fist Search
		Clive is Linear Search

3. click the ghost that represents the algorithm you DO NOT want to see.

4. It will pop up in the inspector and at the top by the ghost's name there will a box with a checkmark. click it to uncheck it and that algorithm won't run the next time.