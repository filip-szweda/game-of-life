using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace game_of_life
{
    public class Cell: INotifyPropertyChanged
    {
        bool isAlive;
        public Cell(int x, int y)
        {
            isAlive = false;
            X = x;
            Y = y;    
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set
            {
                if (isAlive != value)
                {
                    isAlive = value;
                    OnPropertyChanged(nameof(IsAlive));
                }
            }
        }

        public int X { get; set; }
        public int Y { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Brushes.Black : Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Brush)value == Brushes.Black;
        }
    }

    public class Utils
    {
        public static bool[,] Cell2DArrayToBool2DArray(Cell[,] cell2DArray)
        {
            bool[,] bool2DArray = new bool[cell2DArray.GetLength(0), cell2DArray.GetLength(1)];
            for(int x = 0; x < cell2DArray.GetLength(0); x++)
            {
                for(int y = 0; y < cell2DArray.GetLength(1); y++)
                {
                    bool2DArray[x, y] = cell2DArray[x, y].IsAlive;
                }
            }
            return bool2DArray;
        }
    }
}
