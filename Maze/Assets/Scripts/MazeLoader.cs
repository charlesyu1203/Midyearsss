using UnityEngine;
using UnityEngine.AI;

public class MazeLoader : MonoBehaviour {
	public int rows, cols;
	public GameObject wall; // from prefabs. wall object
    public GameObject floor;
    public GameObject coin;
    public GameObject enemy;
    public GameObject player;
    public float size = 2f; //determines the space inside the wall
    public NavMeshSurface surface;

	private MazeCell[,] mazeCells;
    //holds an array of mazecells
    //2D array

	// Use this for initialization
	void Start () {
		InitializeMaze ();

		MazeAlgorithm maze = new BackTrack (mazeCells);
		maze.CreateMaze ();

        surface.BuildNavMesh();
        generateCoins();
        randomlyPlace(enemy);
        randomlyPlace(player);
    }
	
	// Update is called once per frame
	void Update () {
	}

	private void InitializeMaze() {

		mazeCells = new MazeCell[rows,cols];
        //initializes the 2D array of mazecells based on the rows and cols given

		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < cols; c++) {
				mazeCells [r, c] = new MazeCell ();
                //each mazecell in the array is given an index (row and column)

                //since each mazecell has 5 game objects, (1 floor and four walls), they will be instantiated in the loops
                //floor is always instantiated
                //Instantiate method takes the gameobject to be instantiated, the position, and the rotation
                //The name initialization is done to make sure that it is clear where it is. once instanitated, it will appear on the component tree as gameobj r,c
				mazeCells [r, c] .floor = Instantiate (floor, new Vector3 (r*size, -(size/2f), c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

				if (c == 0) {
                    //if it is on the leftmost side of the maze, it will make a wall on the left
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
				}

                //there will always be an east wall
				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;

				if (r == 0) {
                    //if it is on the top row, north wall is instantiated
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
				}

                //there will always be a south wall
				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
			}
		}
	}

    private void generateCoins() {
        for (int i = 0; i < rows; i++) {
            randomlyPlace(coin);
        }
    }
    private void randomlyPlace(GameObject gameObject) {
        int x = Random.Range(0, rows);
        int y = Random.Range(0, cols);
        Vector3 v = GameObject.Find("Floor " + x + "," + y).transform.position;
        v.y++;

        Instantiate(gameObject, v, Quaternion.identity);
    }
}
