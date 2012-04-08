using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeometryTest
{
    class Polygon : System.ComponentModel.INotifyPropertyChanged
    {

        private static readonly Polygon instance = new Polygon();
        public ObservableCollection<ColoredPoint> vertices { get;
            set
            {
                vertices = value;
                OnPropertyChanged("Vertices");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        bool closed = false;
        bool clockwise = true;

        public static Polygon Instance
        {
            get
            {
                return instance;
            }
        }
        private Polygon()
        {
            vertices = new ObservableCollection<ColoredPoint>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }     

        public void AddVertex(ColoredPoint vertex)
        {
            this.vertices.Add(vertex);
            OnPropertyChanged("Vertices");
        }

        internal void readCoordinateFile(string inputFile)
        {
            try
            {
                using (StreamReader sr = File.OpenText(inputFile))
                {
                    String input;
                    while ((input = sr.ReadLine()) != null)
                    {
                        String[] words = input.Split(',');
                        if (words.Length > 2)
                        {
                            throw new FormatException("Length: " + words.Length.ToString());
                        }
                        ColoredPoint c1 = new ColoredPoint(Convert.ToDouble(words[0]), Convert.ToDouble(words[1]));
                        this.AddVertex(c1);
                    }
                }
            }
            catch (Exception e)
            {
                // Configure the message box to be displayed
                string messageBoxText = e.Message;
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
                return;
            }
        }
        public String getInputCoordinates()
        {
            StringBuilder input = new StringBuilder();
            if (vertices.Count != 0)
            {
                foreach (ColoredPoint vertex in vertices)
                {
                    input.Append("X: ").Append(vertex.point.X).Append(" Y: ").Append(vertex.point.Y);
                }
            }

            return input.ToString();
        }

        internal void flushData()
        {
            vertices.Clear();
            //reset button states

        }

        public class PointCollectionConverter : System.Windows.Data.IValueConverter
        {
            public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value.GetType() == typeof(ObservableCollection<ColoredPoint>) && targetType == typeof(System.Windows.Media.PointCollection))
                {
                    var pointCollection = new System.Windows.Media.PointCollection();
                    foreach (var coloredPoint in value as ObservableCollection<ColoredPoint>)
                        pointCollection.Add(coloredPoint.point);
                    return pointCollection;
                }
                return null;
            }

            public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return null;
            }
        }
    }
}
