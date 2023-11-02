using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace game_of_life
{
    public class GameOfLife
    {
        public Cell[,] cellsGrid;
        public GameOfLife(int width, int height)
        {
            cellsGrid = new Cell[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cellsGrid[i, j] = new Cell(i, j);
                }
            }
        }

        public void Update()
        {

        }
    }
}
