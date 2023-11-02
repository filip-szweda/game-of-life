using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace game_of_life
{
    public class GameOfLife
    {
        public int Width;
        public int Height;
        public Cell[,] cellsGrid;
        public GameOfLife(int width, int height)
        {
            Width = width;
            Height = height;

            cellsGrid = new Cell[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cellsGrid[i, j] = new Cell(i, j);
                }
            }
        }

        public int countNeighbourLiveCells(int x, int y)
        {
            int count = 0;
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= Width)
                {
                    continue;
                }
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (j < 0 || j >= Height)
                    {
                        continue;
                    }
                    if (i == x && j == y)
                    {
                        continue;
                    }
                    if (cellsGrid[i, j].IsAlive)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void Update()
        {
            Cell[,] newGeneration = new Cell[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    cellsGrid[i, j] = new Cell(i, j);
                }
            }

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var neighbourLiveCell = countNeighbourLiveCells(i, j);
                    if (cellsGrid[i, j].IsAlive)
                    {
                        if (neighbourLiveCell < 2 || neighbourLiveCell > 3)
                        {
                            newGeneration[i, j].IsAlive = false;
                        }
                        else
                        {
                            newGeneration[i, j].IsAlive = true;
                        }
                    }
                    else
                    {
                        if (neighbourLiveCell == 3)
                        {
                            newGeneration[i, j].IsAlive = true;
                        }
                        else
                        {
                            newGeneration[i, j].IsAlive = false;
                        }
                    }
                }
            }

            cellsGrid = newGeneration;
        }
    }
}
