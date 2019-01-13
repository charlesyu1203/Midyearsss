using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTrack : MazeAlgorithm {

    private int r = 0;
    private int c = 0;
    //gives you current position in maze/2D array

    private bool completed = false;
    //tells you if the maze has been completely made
    //aka if every cell has been visited

    public BackTrack(MazeCell[,] mazeCells) : base(mazeCells)
    {
    }

    public override void CreateMaze()
    {
        RunThenBack();
    }

    private void RunThenBack()
    {
        mazeCells[r, c].visited = true;
        //the beginning spot (0,0) is the starting spot, thus visited

        while (!completed)
            //while not all of the cells have been visited
        {
            Run(); // Will run until it hits a dead end.
            Back(); // Finds the next unvisited cell with an adjacent visited cell. If it can't find any, it sets courseComplete to true.
        }
    }

    private void Run()
        //this method will run until it needs to backtrack (aka until it cannot move further)
    {
        while (RouteStillAvailable(r, c))
            //checks to see if it can move, there are adjacent unvisted cells
        {
            int direction = Random.Range(1, 5);
            //get a random number between 1 and 4 inclusive
            //1 = north
            //2 = south
            //3 = east
            //4 = west
            if (direction == 1 && CellIsAvailable(r - 1, c))
            {
                // destroys two walls, the wall in the north side of current cell and the south side of the above cell
                //bec each cell has its own wall
                DestroyWallIfItExists(mazeCells[r, c].northWall);
                DestroyWallIfItExists(mazeCells[r - 1, c].southWall);
                r--;
            }
            else if (direction == 2 && CellIsAvailable(r + 1, c))
            {
                DestroyWallIfItExists(mazeCells[r, c].southWall);
                DestroyWallIfItExists(mazeCells[r + 1, c].northWall);
                r++;
            }
            else if (direction == 3 && CellIsAvailable(r, c + 1))
            {
                DestroyWallIfItExists(mazeCells[r, c].eastWall);
                DestroyWallIfItExists(mazeCells[r, c + 1].westWall);
                c++;
            }
            else if (direction == 4 && CellIsAvailable(r, c - 1))
            {
                DestroyWallIfItExists(mazeCells[r, c].westWall);
                DestroyWallIfItExists(mazeCells[r, c - 1].eastWall);
                c--;
            }

            mazeCells[r, c].visited = true;
        }
    }

    private void Back()
    {
        completed = true; // Set it to this, and see if we can prove otherwise below!

        for (int row = 0; row < mazeRows; row++)
        {
            for (int col = 0; col < mazeColumns; col++)
            {
                if (!mazeCells[row, col].visited && CellHasAnAdjacentVisitedCell(row, col))
                {
                    //once another cell that is unvisited is found, it will go there and start again
                    completed = false; 
                    r = row;
                    c = col;
                    DestroyAdjacentWall(r, c);
                    mazeCells[r, c].visited = true;
                    return; // Exit the function
                }
            }
        }
    }

    private bool RouteStillAvailable(int row, int col)
    {
        //looks towards each side to see if their ais a possible route to take
        if ( (row > 0 && !mazeCells[row - 1, col].visited) ||
            (row < mazeRows - 1 && !mazeCells[row + 1, col].visited) || 
            (col > 0 && !mazeCells[row, col - 1].visited) ||
            (col < mazeColumns - 1 && !mazeCells[row, col + 1].visited))
            return true;
        return false;
    }

    private bool CellIsAvailable(int row, int column)
    {
        //checks to see if the cell (in parameters) was visited
        if (row >= 0 && row < mazeRows && column >= 0 && column < mazeColumns && !mazeCells[row, column].visited)
            return true;
        return false;
    }

    private void DestroyWallIfItExists(GameObject wall)
    {
        if (wall != null)
        {
            GameObject.Destroy(wall);
        }
    }

    private bool CellHasAnAdjacentVisitedCell(int row, int column)
    {
        if ((row > 0 && mazeCells[row - 1, column].visited) ||
            (row < (mazeRows - 2) && mazeCells[row + 1, column].visited) ||
            (column > 0 && mazeCells[row, column - 1].visited) ||
            (column < (mazeColumns - 2) && mazeCells[row, column + 1].visited))
            return true;
        return false;
    }
    private void DestroyAdjacentWall(int row, int column)
    {
        bool wallDestroyed = false;

        while (!wallDestroyed)
        {
            int direction = Random.Range(1, 5);

            if (direction == 1 && row > 0 && mazeCells[row - 1, column].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].northWall);
                DestroyWallIfItExists(mazeCells[row - 1, column].southWall);
                wallDestroyed = true;
            }
            else if (direction == 2 && row < (mazeRows - 2) && mazeCells[row + 1, column].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].southWall);
                DestroyWallIfItExists(mazeCells[row + 1, column].northWall);
                wallDestroyed = true;
            }
            else if (direction == 3 && column > 0 && mazeCells[row, column - 1].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].westWall);
                DestroyWallIfItExists(mazeCells[row, column - 1].eastWall);
                wallDestroyed = true;
            }
            else if (direction == 4 && column < (mazeColumns - 2) && mazeCells[row, column + 1].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].eastWall);
                DestroyWallIfItExists(mazeCells[row, column + 1].westWall);
                wallDestroyed = true;
            }
        }

    }
}
