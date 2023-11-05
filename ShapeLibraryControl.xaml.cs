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
    /// Interaction logic for ShapeLibraryControl.xaml
    /// </summary>
    public partial class ShapeLibraryControl : UserControl
    {
        public ShapeLibraryControl()
        {
            InitializeComponent();
        }

        private void SwitchToGameControlWithShape(State? shape)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.SwitchToGameControl(shape);
        }

        private void CreateGliderButton_Click(object sender, RoutedEventArgs e)
        {
            var glider = StateHandler.LoadStateFromJson("shapes/glider.json");
            SwitchToGameControlWithShape(glider);
        }

        private void CreateToadButton_Click(object sender, RoutedEventArgs e)
        {
            var toad = StateHandler.LoadStateFromJson("toad.json");
            SwitchToGameControlWithShape(toad);
        }

        private void CreatePulsarButton_Click(object sender, RoutedEventArgs e)
        {
            var pulsar = StateHandler.LoadStateFromJson("pulsar.json");
            SwitchToGameControlWithShape(pulsar);
        }

        private void CreatePentadecathlonButton_Click(object sender, RoutedEventArgs e)
        {
            var pentadecathlon = StateHandler.LoadStateFromJson("pentadecathlon.json");
            SwitchToGameControlWithShape(pentadecathlon);
        }

        private void CreateHeavyweightSpaceshipButton_Click(object sender, RoutedEventArgs e)
        {
            var heavyweightSpaceship = StateHandler.LoadStateFromJson("heavyweightSpaceship.json");
            SwitchToGameControlWithShape(heavyweightSpaceship);
        }

        private void CreateCloverleafButton_Click(object sender, RoutedEventArgs e)
        {
            var cloverleaf = StateHandler.LoadStateFromJson("cloverleaf.json");
            SwitchToGameControlWithShape(cloverleaf);
        }
    }
}
