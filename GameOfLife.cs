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
        public Cell[,] CurrentGeneration;
        public List<bool[,]> PreviousGenerations;

        public GameOfLife(int width, int height)
        {
            Width = width;
            Height = height;

            CurrentGeneration = new Cell[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    CurrentGeneration[x, y] = new Cell(x, y);
                }
            }

            PreviousGenerations = new List<bool[,]>();
        }

        private int countNeighbourLiveCells(bool [,] previousGeneration, int x, int y)
        {
            int count = 0;
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if ((i == x && j == y) || (i < 0 || j < 0) || (i >= Width || j >= Height))
                    {
                        continue;
                    }

                    if (previousGeneration[i, j])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private void CreateNewGeneration()
        {
            bool[,] previousGeneration = PreviousGenerations[PreviousGenerations.Count - 1];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var neighbourLiveCell = countNeighbourLiveCells(previousGeneration, x, y);
                    if (previousGeneration[x, y])
                    {
                        if (neighbourLiveCell < 2 || neighbourLiveCell > 3)
                        {
                            CurrentGeneration[x, y].IsAlive = false;
                        }
                    }
                    else
                    {
                        if (neighbourLiveCell == 3)
                        {
                            CurrentGeneration[x, y].IsAlive = true;
                        }
                    }
                }
            }
        }

        private void SavePreviousGeneration()
        {
            bool[,] previousGeneration = Utils.Cell2DArrayToBool2DArray(CurrentGeneration);
            PreviousGenerations.Add(previousGeneration);
        }

        public void Update()
        {
            SavePreviousGeneration();
            CreateNewGeneration();
        }

        private void GoBackToPreviousGeneration()
        {
            bool[,] previousGeneration = PreviousGenerations[PreviousGenerations.Count - 1];
            PreviousGenerations.RemoveAt(PreviousGenerations.Count - 1);

            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    CurrentGeneration[x, y].IsAlive = previousGeneration[x, y];
                }
            }
        }

        public void Revert()
        {
            if (PreviousGenerations.Count == 0)
            {
                return;
            }

            GoBackToPreviousGeneration();
        }
    }
}
