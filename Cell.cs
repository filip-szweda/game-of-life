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
}
