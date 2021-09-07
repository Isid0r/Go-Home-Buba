using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject exitPrefab;
    public Transform cam;
    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                Cell c = Instantiate(cellPrefab,new Vector2(i,j),Quaternion.identity,gameObject.transform).GetComponent<Cell>();
                c.wallLeft.SetActive(maze[i,j].wallLeft);
                c.wallBottom.SetActive(maze[i,j].wallBottom);        
            }
        }
        Instantiate(exitPrefab, new Vector2(maze.GetLength(0) - 1.5f, maze.GetLength(1) - 1.5f), Quaternion.identity,gameObject.transform);
        cam.position = new Vector3(((maze.GetLength(0) - 1) / 2)+0.5f, ((maze.GetLength(1) - 1) / 2)+0.5f, -10);
    }

}
