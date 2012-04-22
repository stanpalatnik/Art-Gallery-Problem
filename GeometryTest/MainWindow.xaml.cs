using System;
using System.Collections.Generic;
using System.IO;
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
using GeometryTest.Models;

namespace GeometryTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Polygon p1 = Polygon.Instance;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Create_Polygon_From_File(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                p1.readCoordinateFile(dlg.FileName);
                if (p1.vertices.Count != 0)
                {
                    print_input_file.IsEnabled = true;
                    calculate_guards.IsEnabled = true;
                    save_to_output.IsEnabled = true;
                }
            }
        }

        private void Calculate_Vertex_Guards(object sender, RoutedEventArgs e)
        {
            if (!p1.closed)
            {
                if (prevY != -1) // not the first point
                {
                    if (!((Math.abs(first.getX() - evt.x) <= 3 && (Math.abs(first.getY() - evt.y) <= 3))) && p.size < 100)
                    {
                        if (noIntersection(evt.x, evt.y, prevX, prevY))
                        {
                            drawEdge(evt.x, evt.y, prevX, prevY);
                            p.addPnt(new ArtGallery.Point(evt.x, evt.y));
                            drawpoint(evt.x, evt.y);
                            intersected = false;
                        }
                        else
                        {
                            intersected = true;
                        }
                    }

                    else
                    {
                        if (noIntersection2(first.x, first.y, prevX, prevY))
                        {
                            drawEdge(first.x, first.y, prevX, prevY);
                            p.close();
                            if (p.clockwise && (p.area() < 0)) p.reverse();
                            else if (!p.clockwise && (p.area() < 0)) p.reverse();
                            Polygon pTmp = new Polygon(p);
                            d = t.triangulate(pTmp, getGraphics());
                            p.addDiagonals(d);
                            ColorSet CSet = t.color(d, p); 		// 3 color the polygon
                            CSet.setGuards();
                            repaint();
                        }
                        else intersected = true;
                    }
                }
                else
                {
                    first = new ArtGallery.Point(evt.x, evt.y);
                    p.addPnt(first);
                    drawpoint(evt.x, evt.y);
                }
                if (!intersected)
                {
                    prevX = evt.x;
                    prevY = evt.y;
                }
                return true;
            }
            //
            Triangulation t1 = new Triangulation();
            DiagonalSet d1 = t1.triangulate(p1);
            Triangulation t2 = new Triangulation();
        }

        private void Save_Calculated_Data_To_Output(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            if (dlg.ShowDialog() == true)
            {
                //save input
                string filename = dlg.FileName;
                using (StreamWriter sw = new StreamWriter(filename))
                {

                    sw.WriteLine(p1.getInputCoordinates());
                }
            }
        }

        private void Remove_Data(object sender, RoutedEventArgs e)
        {
            p1.flushData();
            //reset button states
            print_input_file.IsEnabled = false;
            calculate_guards.IsEnabled = false;
        }

        private void Polygon_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void SubLayout_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public bool noIntersection(int x1, int y1, int x2, int y2)
        {
            ColoredPoint p2, p3;
            p2 = new ColoredPoint(x1, y1);
            p3 = new ColoredPoint(x2, y2);
            if (p1.vertices.Count <= 1) return true;
            if (p1.clockwise)
                for (int i = 0; i < p1.vertices.Count - 2; i++)
                {
                    if (t1.intersect(p1, p2, p1.vertices[i], p1.vertices[i + 1]))
                    {
                        return false;
                    }
                }
            else
                for (int i = p1.vertices.Count - 1; i > 1; i--)
                {
                    if (t.intersect(p1, p2, p1.vertices[i - 1], p1.vertices[i]))
                    {
                        return false;
                    }

                }


            return true;
        }

        public bool noIntersection2(int x1, int y1, int x2, int y2)
        {
            ColoredPoint p2, p3;
            p2 = new ColoredPoint(x1, y1);
            p3 = new ColoredPoint(x2, y2);
            if (p1.vertices.Count <= 1) return true;
            if (p1.clockwise)
                for (int i = 1; i < p1.vertices.Count - 2; i++)
                {
                    if (t.intersect(p1, p2, p1.vertices[i], p1.vertices[i + 1]))
                    {
                        return false;
                    }
                }
            else
                for (int i = p1.vertices.Count - 2; i > 1; i--)
                {
                    if (t.intersect(p1, p2, p1.vertices[i - 1], p1.vertices[i]))
                    {
                        return false;
                    }

                }


            return true;
        }
    }
}
