using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GeometryTest.Models;

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
        int[,] adjArray = new int[50,50];
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

        public void addDiagonals(GeometryTest.Models.DiagonalSet d)
        {
            Edge tmp;
            for (int i = 0; i < d.size; i++)
            {
                tmp = d.getDiagonal(i);
                link(tmp.getStart().getIndex(), tmp.getEnd().getIndex());
            }
            return;
        }

        void addPnt(Point p)
        {
            ColoredPoint c1 = new ColoredPoint(Convert.ToDouble(p.X), Convert.ToDouble(p.Y));
            if (clockwise)
            {
                if (vertices.Count != 0)
                {
                    link(vertices.Count, vertices.Count - 1);
                    if (vertices.Count != 2) unlink(vertices.Count - 1, 0);
                    link(vertices.Count, 0);
                }
                p.setIndex(vertices.Count);
                vertices.Add(c1);
            }

            else
            {
               // for (int i = vertices.Count; i > 0; i--)
               // {
               //     VSet[i] = VSet[i - 1];
               //     VSet[i].index = i;
               // }
                vertices.Insert(0, c1);
                link(vertices.Count, vertices.Count - 1);
                link(vertices.Count, 0);
                unlink(vertices.Count - 1, 0);
                VSet[0].index = 0;
            }
        }
        /**
         * @return int
         */
        public int area()
        {
            int i;
            int currentSum = 0;
            for (i = 0; i < vertices.Count - 1; i++)
            {
                currentSum += (VSet[i].x * VSet[i + 1].y) - (VSet[i].y * VSet[i + 1].x);
            }
            currentSum += (VSet[size - 1].x * VSet[0].y) - (VSet[size - 1].y * VSet[0].x);
            return currentSum;
        }
        /**
         * @return boolean
         * @param v int
         */
        public bool areNeighbors(int v1, int v2)
        {

            return (adjArray[v1,v2] == 1);
        }

        /**
         */
        public void close()
        {
            closed = true;
            return;
        }
        Point getPnt(int i)
        {
            return VSet[i];
        }
        /**
         * @param a int
         * @param b int
         */
        public void link(int a, int b)
        {
            adjArray[a,b] = 1;
            adjArray[b,a] = 1;
            return;
        }
        /**
         * This method was created by a SmartGuide.
         * @param pnt Point
         */
        public void remove(int index, DiagonalSet d)
        {
            Edge diag; ;
            int j;

            if (!clockwise)
            {
                for (j = 0; j < vertices.Count - 1; j++)
                {
                    VSet[j] = VSet[j + 1];
                    VSet[j].index = j;
                }

            }
            // remove diagonals
            if (closed)
            {
                for (j = d.size - 1; j >= 0; j--)
                {
                    diag = d.getDiagonal(j);
                    unlink(diag.getStart().getIndex(), diag.getEnd().getIndex());
                }
                // reset the colors and remove guards

                for (j = 0; j < size; j++)
                {
                    VSet[j].setColor(-1);
                    VSet[j].guard = false;
                }
                d.size = 0;
            }

            link(vertices.Count - 1, 0);
            unlink(0, vertices.Count);

            closed = false; // open the polygon
            return;
        }
        void removeVertex(int i)
        {
            int k;

            for (k = i; k < vertices.Count - 1; k++)
            {
                VSet[k] = VSet[k + 1];
            }
        }

        public void reverse()
        {
            // 
            int i, j;
            Point tmp;
            if (vertices.Count == 1) return;
            else
                for (i = 0, j = vertices.Count - 1; i < j; i++, j--)
                {
                    tmp = VSet[i];
                    VSet[i] = VSet[j];
                    VSet[j] = tmp;
                    VSet[i].index = i;
                    VSet[j].index = j;
                }
            clockwise = !clockwise;
            return;
        }
        /**
         * @param a int
         * @param b int
         */
        public void unlink(int a, int b)
        {
            adjArray[a,b] = 0;
            adjArray[b,a] = 0;
            return;
        }
    }
}
