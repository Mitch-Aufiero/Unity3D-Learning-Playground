using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour
{

    public GameObject wall;
    public GameObject floor;
    public int mazeWidth;
    public int mazeHeight;
    public int seed;

    public GameObject[] DeadEndSpawns;
    public GameObject[] CoridorSpawns;
    public GameObject[] HallSpawns;

    public GameObject PlayerCharacter;
    public NavMeshGenerator navGenerator;
    public WorldBounds bounds;



    struct Cell // (dead end 3 walls, hall 2 walls, coridor 1 wall
    {
        public GameObject Wall;
        public GameObject RotWall;
        public GameObject Floor;

        public Cell(GameObject wall, GameObject rotwall, GameObject floor)
        {
            Wall = wall;
            RotWall = rotwall;
            Floor = floor;
        }
    }

    Cell[,] Grid;
    GameObject MazeGrid;
    bool mazeMapped;

    IEnumerator Start()
    {

        float mazeOffset = wall.transform.localScale.x;

        bounds.min.position = new Vector3(0, 1, 0);
        bounds.max.position = new Vector3(mazeWidth *mazeOffset, 1, mazeHeight * mazeOffset);
    

        navGenerator = GetComponent<NavMeshGenerator>();
        Random.InitState(seed);
        MazeGrid = this.gameObject;
        CreateGrid();
        CarveMaze();
        

        yield return new WaitForFixedUpdate();
        MapMaze();


        //navGenerator.Generate();

        PlayerCharacter.SetActive(true);
    }



    void CreateGrid()
    {
        float mazeOffset = wall.transform.localScale.x;

        
        Grid = new Cell[mazeWidth + 1, mazeHeight + 1];

        GameObject Wall = null;
        GameObject RotWall = null;
        GameObject Floor = null;
       

        for (int i = 0; i <= mazeWidth; i++)
        {

            for (int j = 0; j <= mazeHeight; j++)
            {
                GameObject MazeCell = new GameObject("MazeCell");
                if (i >0)
                {
                    Wall = Instantiate(wall, new Vector3(0, 0, 1), Quaternion.identity);
                    Wall.transform.localPosition = new Vector3((i * mazeOffset) - (mazeOffset/2), 3, (j * mazeOffset));

                    Wall.transform.SetParent(MazeCell.transform);
                    Wall.name = "Wall";

                }
                if (j > 0)
                {
                    RotWall = Instantiate(wall, new Vector3(0, 0, 1), Quaternion.identity);
                    RotWall.transform.localRotation = Quaternion.Euler(0, 90, 0);
                    RotWall.transform.localPosition = new Vector3((i * mazeOffset), 3, (j * mazeOffset) - (mazeOffset / 2));

                    RotWall.transform.SetParent(MazeCell.transform);
                    RotWall.name = "Wall";
                }

                if(j> 0 && i > 0)
                {
                    Floor = Instantiate(floor, new Vector3(0, 0, 1), Quaternion.identity);
                    Floor.transform.localPosition = new Vector3((i * mazeOffset) - (mazeOffset / 2), -2, (j * mazeOffset) - (mazeOffset / 2));

                    Floor.transform.SetParent(MazeCell.transform);
                    Floor.name = "Floor";
                    //navGenerator.surfaces.Add(Floor.GetComponent<NavMeshSurface>());
                }

                Grid[i, j] = new Cell(Wall, RotWall, Floor);
                MazeCell.transform.SetParent(MazeGrid.transform);

            }
        }
    }

    void CarveMaze()
    {
        Stack<Vector2> Carver = new Stack<Vector2>();
        Carver.Push(new Vector2(Random.Range(1, mazeWidth), Random.Range(1, mazeWidth)));
        
        List<Vector2> neighbors = new List<Vector2>();
        Vector2 neighborLocation = new Vector2(0, 0);
        
        do
        {
            Vector2 currentCell = Carver.Peek();

            for (int neighborPos = -1; neighborPos < 2; neighborPos++)// 4 positions: up, down, left right (make enum)
            {
                if (!(neighborPos == 0))
                {
                    if (!(currentCell.x + neighborPos > mazeWidth) && !(currentCell.x + neighborPos < 1))
                        if (CheckCellUnvisited((int)currentCell.x + neighborPos, (int)currentCell.y))
                            neighbors.Add(new Vector2(currentCell.x + neighborPos, currentCell.y));

                    if (!(currentCell.y + neighborPos > mazeHeight) && !(currentCell.y + neighborPos < 1))
                        if (CheckCellUnvisited((int)currentCell.x, (int)currentCell.y + neighborPos))
                            neighbors.Add(new Vector2(currentCell.x, currentCell.y + neighborPos));


                }
            }


            if (neighbors.Count == 0)
            {//if all neighbors have been visited then pop current cell
                Carver.Pop();

            }
            else
            {//if at least one neighbor has been visited choose neighbor and destroy wall

                
                neighborLocation = neighbors[Random.Range(0, neighbors.Count)];
                Carver.Push(neighborLocation);//puts random unvisited neighbor on the stack
                neighbors.Clear();

                

                
                if ((int)neighborLocation.x == (int)currentCell.x && (int)neighborLocation.y == (int)currentCell.y - 1)
                    destroyWall((int)neighborLocation.x, (int)neighborLocation.y, true);// destroy new cell EastWall
                else if ((int)neighborLocation.x == (int)currentCell.x - 1 && (int)neighborLocation.y == (int)currentCell.y)
                    destroyWall((int)neighborLocation.x, (int)neighborLocation.y, false);// destroy new cell NorthWall	
                else if ((int)neighborLocation.x == (int)currentCell.x && (int)neighborLocation.y == (int)currentCell.y + 1)
                    destroyWall((int)currentCell.x, (int)currentCell.y, true);// destroy old cell East Wall	
                else if ((int)neighborLocation.x == (int)currentCell.x + 1 && (int)neighborLocation.y == (int)currentCell.y)
                    destroyWall((int)currentCell.x, (int)currentCell.y, false);// destroy old cell North Wall


            }
        } while ((Carver.Count > 0));
    }

    void ChoosePlayerSpawnCell(Transform cell, Vector3 orientation)// fix orientation, camera is getting things off since character is set based on camera
    {

        PlayerCharacter.transform.position = cell.position;
        PlayerCharacter.transform.localRotation = Quaternion.Euler(orientation);


    }
    


    void MapMaze()// its hard to stay clean when its procedural
    {
        bool playerSpawnChosen = false;

        foreach (Transform mazeCell in MazeGrid.transform)
        {
            foreach (Transform rayOriginator in mazeCell.transform)
            {
                if(rayOriginator.name == "Floor")// which ray didn't hit a wall? face container or mob the same direction
                {
                    rayOriginator.transform.position = new Vector3(rayOriginator.transform.position.x, rayOriginator.transform.position.y + 1, rayOriginator.transform.position.z);

                    int wallCount = 0;
                    List<Vector3> remainingWallsDirections = new List<Vector3>();
                    List<Vector3> missingWallsDirections = new List<Vector3>();

                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(rayOriginator.transform.position, transform.TransformDirection(Vector3.forward), 5f))
                    {
                        wallCount++;
                        remainingWallsDirections.Add(new Vector3(0, 90, 0));
                    }
                    else
                    {
                        missingWallsDirections.Add(new Vector3(0, 90, 0));
                    }
                    if (Physics.Raycast(rayOriginator.transform.position, transform.TransformDirection(Vector3.back), 5f))
                    {
                        wallCount++;
                        remainingWallsDirections.Add(new Vector3(0, -90, 0));
                    }
                    else
                    {
                        missingWallsDirections.Add(new Vector3(0, -90, 0));
                    }
                    if (Physics.Raycast(rayOriginator.transform.position, transform.TransformDirection(Vector3.left), 5f))
                    {
                        wallCount++;
                        remainingWallsDirections.Add(new Vector3(0, 0, 0));
                    }
                    else
                    {
                        missingWallsDirections.Add(new Vector3(0, 0, 0));
                    }
                    if (Physics.Raycast(rayOriginator.transform.position, transform.TransformDirection(Vector3.right), 5f))
                    {
                        wallCount++;
                        remainingWallsDirections.Add(new Vector3(0, 180, 0));
                    }
                    else
                    {
                        missingWallsDirections.Add(new Vector3(0, 180, 0));
                    }

                    string cellType = markCellType(wallCount);
                    mazeCell.gameObject.name = cellType;
                    if(cellType == "DeadEnd")
                    {
                        int index = Random.Range(0, missingWallsDirections.Count);

                        if (!playerSpawnChosen)// if there's not a dead end what happens? lol
                        {
                            playerSpawnChosen = true;

                            ChoosePlayerSpawnCell(rayOriginator, new Vector3(missingWallsDirections[index].x, missingWallsDirections[index].y, missingWallsDirections[index].z));
                        }
                        else
                        {
                           
                            SpawnRandomObject(DeadEndSpawns, rayOriginator.gameObject, new Vector3(0, .5f, 0),
                            new Vector3(missingWallsDirections[index].x, missingWallsDirections[index].y - 180, missingWallsDirections[index].z));

                        }

                    }
                    if(cellType == "Coridor")
                    {
                        int index = Random.Range(0, missingWallsDirections.Count);
                        SpawnRandomObject(CoridorSpawns, rayOriginator.gameObject, new Vector3(0, .5f, 0), missingWallsDirections[index]) ;
                    }
                       
                    if (cellType == "Hall")
                    {
                        int index = Random.Range(0,remainingWallsDirections.Count);

                        SpawnRandomObject(HallSpawns, rayOriginator.gameObject, new Vector3(0, .5f, 0), remainingWallsDirections[index]);

                    }
                        


                }
            }
        }
    }

    bool CheckCellUnvisited(int i, int j)//true = unvisited (grid 10/5 throws index out of bounds)
    {
        bool CellUnvisited = true;

        if (Grid[i, j].RotWall == null)
            CellUnvisited = false;
        if (Grid[i, j].Wall == null)
            CellUnvisited = false;
        if (Grid[i, j - 1].Wall == null)
            CellUnvisited = false;
        if (Grid[i - 1, j].RotWall == null)
            CellUnvisited = false;


        return CellUnvisited;
    }

    void destroyWall(int i, int j, bool wall)// true for wall; false for rotated wall
    {
        if (wall)
        {
            Destroy(Grid[i, j].Wall);
            Grid[i, j].Wall = null;

        }
        else
        {
            Destroy(Grid[i, j].RotWall);
            Grid[i, j].RotWall = null;
        }


        
    }

    string markCellType(int floorType)//3 deadend, 2 hallway, 1 coridor
    {
        string cellType = "";
        switch (floorType)
        {
            case 3:
                cellType = "DeadEnd";
                break;

            case 2:
                cellType = "Hall";
                break;

            case 1:
                cellType = "Coridor";
                break;

            case -1:
                cellType = "Border";
                break;
            default:
                break;
        }

        return cellType;


    }

    void SpawnRandomObject(GameObject[] gameObjects, GameObject spawnCell, Vector3 offset, Vector3 orientation)
    {
        GameObject gameObject = gameObjects[Random.Range(0, gameObjects.Length)];
        
        gameObject = Instantiate(gameObject, new Vector3(0, 0, 1), Quaternion.identity);
        gameObject.transform.localRotation = Quaternion.Euler(orientation);
        gameObject.transform.localPosition = new Vector3(spawnCell.transform.position.x + offset.x , spawnCell.transform.position.y + offset.y, spawnCell.transform.position.z + offset.z);
        
        gameObject.transform.position +=
            (gameObject.transform.right * -gameObject.GetComponent<spawnableData>().RelativeObjectSize.x)
            + (gameObject.transform.forward * -gameObject.GetComponent<spawnableData>().RelativeObjectSize.z)
            + (gameObject.transform.up * -gameObject.GetComponent<spawnableData>().RelativeObjectSize.y);
        gameObject.transform.SetParent(spawnCell.transform);

    }
}
