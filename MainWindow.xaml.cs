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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace game_of_life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameControl _gameControl;
        private ShapeLibraryControl _shapeLibraryControl;
        public MainWindow()
        {
            InitializeComponent();
            _gameControl = new GameControl();
            _shapeLibraryControl = new ShapeLibraryControl();
            SwitchToGameControl();
        }

        public void SwitchToGameControl()
        {
            MainContentControl.Content = _gameControl;
        }

        public void SwitchToShapeLibraryControl()
        {
            MainContentControl.Content = _shapeLibraryControl;
        }
    }
}
