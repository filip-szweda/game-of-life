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
        readonly TimeSpan TickInterval = TimeSpan.FromMilliseconds(100);

        DispatcherTimer TickTimer;
        GameOfLife? GameOfLife;
        public GameControl()
        {
            InitializeComponent();
            // set how often the Tick event will be raised
            TickTimer = new DispatcherTimer { Interval = TickInterval };
            // subscribe GameTick method to the Tick event
            TickTimer.Tick += GameTick;
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
            if (GameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            GameGrid.Children.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
            GameGrid.Width = width * DEFAULT_CELL_WIDTH;
            GameGrid.Height = height * DEFAULT_CELL_HEIGHT;

            for (int x = 0; x < width; x++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(DEFAULT_CELL_WIDTH) });
            }

            for (int y = 0; y < height; y++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(DEFAULT_CELL_HEIGHT) });
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var cell = new Rectangle
                    {
                        Width = DEFAULT_CELL_WIDTH,
                        Height = DEFAULT_CELL_HEIGHT,
                        Fill = Brushes.White,
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.5,
                        DataContext = GameOfLife.CurrentGeneration[x, y]
                    };

                    var binding = new Binding("IsAlive")
                    {
                        Converter = new BoolToBrushConverter(),
                        ConverterParameter = this,
                        Mode = BindingMode.OneWay
                    };
                    cell.SetBinding(Rectangle.FillProperty, binding);

                    cell.MouseDown += Cell_MouseDown;

                    Grid.SetColumn(cell, x);
                    Grid.SetRow(cell, y);
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
                    GameOfLife = new GameOfLife(width, height);
                    GenerateGameGrid(width, height);
                    this.DataContext = GameOfLife;
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

        void StartAnimationButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"[INFO] Started TickTimer");
            TickTimer.Start();
        }

        void StopAnimationButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"[INFO] Stopped TickTimer");
            TickTimer.Stop();
        }

        void OneFrameForwardsButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            GameOfLife.Update();
        }

        void OneFrameBackwardsButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            GameOfLife.Revert();
        }

        void ExportState_Click(object sender, RoutedEventArgs e)
        {
            if (GameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            bool[,] generationToExport = new bool[GameOfLife.Width, GameOfLife.Height];
            for (int x = 0; x < GameOfLife.Width; x++)
            {
                for (int y = 0; y < GameOfLife.Height; y++)
                {
                    generationToExport[x, y] = GameOfLife.CurrentGeneration[x, y].IsAlive;
                }
            }

            var fileName = "state.json";
            StateHandler.SaveStateToJson(fileName,
                generationToExport,
                GameOfLife.CellsDied,
                GameOfLife.CellsBorn,
                GameOfLife.Generations);
            MessageBox.Show($"[INFO] Exported state to {fileName}");
        }

        public void ImportState(State? state)
        {
            if (state == null)
            {
                return;
            }

            bool[,] importedGeneration = state.Generation;
            for (int x = 0; x < GameOfLife.Width; x++)
            {
                for (int y = 0; y < GameOfLife.Height; y++)
                {
                    // if we are importing a smaller grid, we need to set the rest of the cells to false
                    if (x < importedGeneration.GetLength(0) && y < importedGeneration.GetLength(1))
                    {
                        GameOfLife.CurrentGeneration[x, y].IsAlive = importedGeneration[x, y];
                    }
                    else
                    {
                        GameOfLife.CurrentGeneration[x, y].IsAlive = false;
                    }
                }
            }

            GameOfLife.CellsDied = state.CellsDied;
            GameOfLife.CellsBorn = state.CellsBorn;
            GameOfLife.Generations = state.Generations;
        }

        void ImportState_Click(object sender, RoutedEventArgs e)
        {
            if (GameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            var state = StateHandler.LoadStateFromJson("state.json");
            ImportState(state);
        }

        private void ChooseShape_Click(object sender, RoutedEventArgs e)
        {
            if (GameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.SwitchToShapeLibraryControl();
        }

        void GameTick(object sender, EventArgs e)
        {
            if (GameOfLife == null)
            {
                MessageBox.Show("[ERROR] Game grid has not been created yet.");
                return;
            }

            GameOfLife.Update();
        }
    }
}
