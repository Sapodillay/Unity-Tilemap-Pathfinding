# Unity-Tilemap-Pathfinding
A simple A* pathfinding script using unity's TileMap components

#Using

Attach the Grid2D and Pathfinding Script to your collision tilemap and set the Grid attribute in the pathfinding script to your collision tilemap

Then get a reference to the Pathfinding script in the script you want to use it in and call Pathfinding.FindPath(Vector2 A, Vector2 B) to find the path.
