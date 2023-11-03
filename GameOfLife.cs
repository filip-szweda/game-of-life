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
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if ((i == x && j == y) || (i < 0 || j <0) || (i >= Width || j >= Height))
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
            bool[,] newGeneration = new bool[Width, Height];
            for(int i = 0; i < Width; i++)
            {
                for(int j = 0; j < Height; j++)
                {
                    newGeneration[i, j] = cellsGrid[i, j].IsAlive;
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
                            newGeneration[i, j] = false;
                        }
                    }
                    else
                    {
                        if (neighbourLiveCell == 3)
                        {
                            newGeneration[i, j] = true;
                        }
                    }
                }
            }

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    cellsGrid[i, j].IsAlive = newGeneration[i, j];
                }
            }
        }
    }
}
