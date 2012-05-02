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
        public ObservableCollection<ColoredPoint> vertices
        {
            get;
            set;
        }
        public int[,] adjArray = new int[50, 50];
        public event PropertyChangedEventHandler PropertyChanged;
        public bool closed { get; set; }
        public bool clockwise = true;

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
            closed = false;
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
            ColoredPoint tmpPoint = new ColoredPoint(vertex);
            if (!this.vertices.Contains(vertex))
            {
                if (tmpPoint.index == -1)
                {
                    tmpPoint.index = this.vertices.Count;
                }        
                this.vertices.Add(tmpPoint);
                OnPropertyChanged("vertices");
            }
            
        }

        public bool AddVertexFromMouseClick(ColoredPoint vertex)
        {
            Triangulation triangulation = new Triangulation();
            if (this.closed == true) return false;
            if (!containsWithinRange(vertex))
            {

                if (this.vertices.Count == 0 || 
                    triangulation.noIntersection(this.vertices[0].point.X, this.vertices[0].point.Y, vertex.point.X, vertex.point.Y, this))
                {
                    vertex.index = this.vertices.Count;
                    this.vertices.Add(vertex);
                    OnPropertyChanged("vertices");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //if we click on the first point, then we are closing the polygon
                if (vertex.withinRoot(this.vertices[0]))
                {
                    this.closed = true;
                    OnPropertyChanged("vertices");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal void readCoordinateFile(string inputFile)
        {
            Triangulation triangulation = new Triangulation();
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

                        if (this.vertices.Count == 0 || 
                            triangulation.noIntersection(this.vertices[0].point.X, this.vertices[0].point.Y, c1.point.X, c1.point.Y, this))
                        {
                            //this.vertices.Add(c1);
                            this.AddVertex(c1);
                        }
                        else
                        {
                            //error, we have an intersection
                        }
                    }
                    if (this.vertices.Count > 3)
                    {
                        this.close();
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
                    input.Append("X: ").Append(vertex.point.X).Append(" Y: ").Append(vertex.point.Y).Append(Environment.NewLine);
                }
            }

            return input.ToString();
        }

        public String getCalculatedResults()
        {
            StringBuilder rtnStr = new StringBuilder();
            foreach (ColoredPoint coloredPoint in this.vertices)
            {
                if (!coloredPoint.IsDuplicate)
                {
                    rtnStr.Append("X: ").Append(coloredPoint.point.X)
                        .Append(" Y: ").Append(coloredPoint.point.Y)
                        .Append(" Is Guard: ").Append(coloredPoint.IsGuard)
                        .Append(" Vertex Color: ").Append(coloredPoint.vertexColor)
                        .Append(Environment.NewLine);
                }
            }
            return rtnStr.ToString();
        }

        internal void flushData()
        {
            closed = false;
            clockwise = true;
            adjArray = new int[50, 50];
            vertices.Clear();
            OnPropertyChanged("vertices");
        }

        public void addDiagonals(GeometryTest.Models.DiagonalSet d)
        {
            Edge tmp;
            for (int i = 0; i < d.diagonalSet.Count; i++)
            {
                tmp = d.getDiagonal(i);
                link(tmp.Start.index, tmp.End.index);
            }
            return;
        }

        public bool containsWithinRange(ColoredPoint point)
        {
            foreach (ColoredPoint coloredPoint in this.vertices)
            {
                if(point.withinRoot(coloredPoint))
                {
                    return true;
                }
            }

            return false;
        }

        public void addPnt(Point p)
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
                c1.index = vertices.Count;
            }

            else
            {
                for (int i = vertices.Count; i > 0; i--)
                {
                    vertices[i].index = i;
                }
                vertices.Insert(0, c1);
                link(vertices.Count, vertices.Count - 1);
                link(vertices.Count, 0);
                unlink(vertices.Count - 1, 0);
                c1.index = 0;
            }
            OnPropertyChanged("vertices");
            vertices.Add(c1);
        }
        /**
         * @return int
         */
        public double area()
        {
            int i;
            double currentSum = 0;
            for (i = 0; i < vertices.Count - 1; i++)
            {
                currentSum += (vertices[i].point.X * vertices[i + 1].point.Y) - (vertices[i].point.Y * vertices[i + 1].point.X);
            }
            currentSum += (vertices[vertices.Count - 1].point.X * vertices[0].point.Y) - (vertices[vertices.Count - 1].point.Y * vertices[0].point.X);
            return currentSum;
        }

        /**
         * @return boolean
         * @param v int
         */
        public bool areNeighbors(int v1, int v2)
        {
            return (adjArray[v1, v2] == 1);
        }

        /**
         */
        public void close()
        {
            closed = true;
        }
        public ColoredPoint getColoredPoint(int i)
        {
            return vertices[i];
        }
        /**
         * @param a int
         * @param b int
         */
        public void link(int a, int b)
        {
            adjArray[a, b] = 1;
            adjArray[b, a] = 1;
            return;
        }
        /**
         * This method was created by a SmartGuide.
         * @param pnt Point
         */
        public void remove(int index, DiagonalSet d)
        {
            Edge diag;
            int j;

            if (!clockwise)
            {
                vertices.RemoveAt(index);
                for (int i = vertices.Count; i > 0; i--)
                {
                    vertices[i].index = i;
                }

            }
            // remove diagonals
            if (closed)
            {
                for (j = d.diagonalSet.Count - 1; j >= 0; j--)
                {
                    diag = d.getDiagonal(j);
                    unlink(diag.Start.index, diag.End.index);
                }
                // reset the colors and remove guards

                for (j = 0; j < vertices.Count; j++)
                {
                    //vertices[j].vertexColor = ColoredPoint.color.None;
                    vertices[j].IsGuard = false;
                }
            }

            link(vertices.Count - 1, 0);
            unlink(0, vertices.Count);

            closed = false; // open the polygon
            return;
        }
        public void removeVertex(int i)
        {
            vertices.RemoveAt(i);
            OnPropertyChanged("vertices");
        }

        public void reverseCollection()
        {
            // 
            if (vertices.Count == 1) return;
            else
            {
                vertices.Reverse();
                for (int i = 0; i < vertices.Count; i++)
                {
                    vertices[i].index = i;
                }
                OnPropertyChanged("vertices");
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
            adjArray[a, b] = 0;
            adjArray[b, a] = 0;
            return;
        }

        internal void addDiagonalsToVertices(DiagonalSet diagonalSet)
        {
            for (int i = 0; i < diagonalSet.diagonalSet.Count; ++i)
            {
                this.AddVertex(diagonalSet.diagonalSet[i].Start);
                this.AddVertex(diagonalSet.diagonalSet[i].End);
                this.AddVertex(diagonalSet.diagonalSet[i].Cutoff);
            }
            
        }
    }
}
