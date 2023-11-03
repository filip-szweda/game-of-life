using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
        static readonly int DEFAULT_CELL_WIDTH = 20;
        static readonly int DEFAULT_CELL_HEIGHT = DEFAULT_CELL_WIDTH;
        readonly TimeSpan tickInterval = TimeSpan.FromMilliseconds(100);

        DispatcherTimer tickTimer;
        GameOfLife? gameOfLife;
        public GameControl()
        {
            InitializeComponent();
            // set how often the Tick event will be raised
            tickTimer = new DispatcherTimer { Interval = tickInterval };
            // subscribe <method> method to the Tick event
            tickTimer.Tick += GameTick;
        }

        void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var clickedRectangle = sender as Rectangle;

            if (clickedRectangle == null)
            {
                MessageBox.Show("[ERROR] Clicked rectangle is null.");
                return;
            }

            var clickedCell = clickedRectangle.DataContext as Cell;

            if (clickedCell == null)
            {
                MessageBox.Show("[ERROR] Clicked cell is null.");
                return;
            }

            clickedCell.IsAlive = !clickedCell.IsAlive;
            System.Diagnostics.Debug.WriteLine($"[INFO] Clicked cell, X: {clickedCell.X}, Y: {clickedCell.Y}");
        }

        void GenerateGameGrid(int width, int height)
        {
            if (gameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            GameGrid.Children.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
            GameGrid.Width = width * DEFAULT_CELL_WIDTH;
            GameGrid.Height = height * DEFAULT_CELL_HEIGHT;

            for (int i = 0; i < width; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(DEFAULT_CELL_WIDTH) });
            }

            for (int i = 0; i < height; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(DEFAULT_CELL_HEIGHT) });
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var cell = new Rectangle
                    {
                        Width = DEFAULT_CELL_WIDTH,
                        Height = DEFAULT_CELL_HEIGHT,
                        Fill = Brushes.White,
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.5,
                        DataContext = gameOfLife.cellsGrid[i, j]
                    };

                    var binding = new Binding("IsAlive")
                    {
                        Converter = new BoolToBrushConverter(),
                        ConverterParameter = this,
                        Mode = BindingMode.OneWay
                    };
                    cell.SetBinding(Rectangle.FillProperty, binding);

                    cell.MouseDown += Cell_MouseDown;

                    Grid.SetColumn(cell, i);
                    Grid.SetRow(cell, j);
                    GameGrid.Children.Add(cell);
                }
            }
        }

        void GenerateGridButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(GridWidthTextBox.Text, out int width))
            {
                if (int.TryParse(GridHeightTextBox.Text, out int height))
                {
                    gameOfLife = new GameOfLife(width, height);
                    GenerateGameGrid(width, height);
                    this.DataContext = gameOfLife;
                }
                else
                {
                    MessageBox.Show($"[ERROR] Provided grid height: {height} is invalid. Please, provide valid grid size.");
                }
            }
            else
            {
                MessageBox.Show($"[ERROR] Provided grid width: {width} is invalid. Please, provide valid grid size.");
            }
        }

        void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"[INFO] Started tickTimer");
            tickTimer.Start();
        }

        void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"[INFO] Stopped tickTimer");
            tickTimer.Stop();
        }

        void GameTick(object sender, EventArgs e)
        {
            if (gameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"[INFO] Started game of life update");
            gameOfLife.Update();
        }
    }
}
