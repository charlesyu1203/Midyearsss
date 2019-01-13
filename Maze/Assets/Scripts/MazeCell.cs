using UnityEngine;

public class MazeCell {
    /*Since this maze algorithim uses backtracking, each cell is a mazecell, which contained
     if it is visited or not,
     and 4 walls.
     The walls will be knocked down as the algo go on*/
	public bool visited = false;
	public GameObject northWall, southWall, eastWall, westWall, floor;
}
