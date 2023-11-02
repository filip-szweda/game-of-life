using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace game_of_life
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl
    {
        private static readonly int DEFAULT_CELL_SIZE = 20;
        private readonly TimeSpan tickInterval = TimeSpan.FromMilliseconds(100);

        private DispatcherTimer tickTimer;
        public GameControl()
        {
            InitializeComponent();
            // set how often the Tick event will be raised
            tickTimer = new DispatcherTimer { Interval = tickInterval };
            // subscribe <method> method to the Tick event
            // tickTimer.Tick += <method>;
        }

        private void GenerateGameGrid(int size)
        {
            GameGrid.Children.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
            GameGrid.Width = size * DEFAULT_CELL_SIZE;
            GameGrid.Height = size * DEFAULT_CELL_SIZE;

            for (int i = 0; i < size; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(DEFAULT_CELL_SIZE) });
            }

            for (int i = 0; i < size; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(DEFAULT_CELL_SIZE) });
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var cell = new Rectangle
                    {
                        Width = DEFAULT_CELL_SIZE,
                        Height = DEFAULT_CELL_SIZE,
                        Fill = Brushes.White,
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.5
                    };

                    Grid.SetColumn(cell, i);
                    Grid.SetRow(cell, j);
                    GameGrid.Children.Add(cell);
                }
            }
        }

        private void GenerateGridButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(GridSizeTextBox.Text, out int size) && int.TryParse(GridSizeTextBox.Text, out int height))
            {
                GenerateGameGrid(size);
            }
            else
            {
                MessageBox.Show($"[ERROR] Provided grid size: {size} is invalid. Please, provide valid grid size.");
            }
        }
    }
}
