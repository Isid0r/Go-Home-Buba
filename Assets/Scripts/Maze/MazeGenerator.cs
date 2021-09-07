using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MazeGeneratorCell
{
    public int x;
    public int y;

    public int id; // для Краскала
    public bool wallLeftChecked = false;
    public bool wallBottomChecked = false;

    public bool wallLeft = true;
    public bool wallBottom = true;

}
public class MazeGenerator
{
    public int width = 10;
    public int height = 10;
    private Vector2 mazeExit = new Vector2(0, 0);
    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[width,height];
        int counter = 0;
        for (int i = 0; i < maze.GetLength(0); i++) 
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = new MazeGeneratorCell { x = i, y = j , id = counter};
                counter++;
            }
        }
        for (int i = 0; i < width; i++)
        {
            maze[i, height - 1].wallLeft = false;
        }
        for (int i = 0; i < height; i++)
        {
            maze[width - 1, i].wallBottom = false;
        }
        DoKruskal(maze);
        return maze;
    }

    private void DoKruskal(MazeGeneratorCell[,] maze)
    {
        //Убираем из дальнейшей обработки границы
        for (int i = 0; i < width; i++)
        {
            maze[i, height - 1].wallLeftChecked = true;
            maze[i, height - 1].wallBottomChecked = true;
        }
        for (int i = 0; i < width; i++)
        {
            maze[i, 0].wallBottomChecked = true;
        }
        for (int i = 0; i < height; i++)
        {
            maze[width - 1, i].wallBottomChecked = true;
            maze[width - 1, i].wallLeftChecked = true;
        }
        for (int i = 0; i < height; i++)
        {
            maze[0, i].wallLeftChecked = true;
        }

        // Сам алгоритм
        int countCells = width * height;
        List<int> temp = new List<int>();
        int counter = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                temp.Add(counter);
                counter++;
            }
        }

        while (temp.Count != 0)
        {
            int num = temp[Random.Range(0, temp.Count)];
            int x = num / width; // здесь возможно ошибка
            int y = num % width;
            MazeGeneratorCell selected = maze[x,y];

            if (!selected.wallBottomChecked && !selected.wallLeftChecked)
            {
                int tempRand = Random.Range(0, 2);
                if (tempRand == 0)
                {
                    if (selected.id != maze[x - 1, y].id)
                    {
                        RemoveWall(maze[x - 1, y], selected);
                        ChangeID(maze, maze[x - 1, y].id, selected.id);
                        // присваиваем дочерним элементам тоже
                    }
                    selected.wallLeftChecked = true;
                }
            if (tempRand == 1)
            {
                if (selected.id != maze[x, y - 1].id)
                {
                    RemoveWall(maze[x, y - 1], selected);
                    ChangeID(maze, maze[x, y - 1].id, selected.id);
                }
                selected.wallBottomChecked = true;
            }
        }
            else
            {
                if (!selected.wallLeftChecked)
                {
                    if (selected.id != maze[x - 1, y].id)
                    {
                        RemoveWall(maze[x - 1, y], selected);
                        ChangeID(maze, maze[x - 1, y].id, selected.id);
                    }
                    selected.wallLeftChecked = true;
                    // вызываем removewall для левой стены
                }
                if (!selected.wallBottomChecked)
                {
                    if (selected.id != maze[x, y - 1].id)
                    {
                        RemoveWall(maze[x, y - 1], selected);
                        ChangeID(maze, maze[x, y - 1].id, selected.id);
                    }
                    selected.wallBottomChecked = true;
                    //вызываем removewall для нижней стены
                }
                temp.Remove(num);
            }
            
        }
    }

    private void ChangeID(MazeGeneratorCell[,] maze,int idFrom, int idTo)
    {
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (maze[i, j].id == idFrom) maze[i, j].id = idTo;
            }
        }
    }
    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.x != b.x)
        {
            if (a.x > b.x)
            {
                a.wallLeft = false;
            }
            else
            {
                b.wallLeft = false;
            }
        }
        else
        {
            if (a.y > b.y)
            {
                a.wallBottom = false;
            }
            else
            {
                b.wallBottom = false;
            }
        }
    }
}
