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
                    create_from_file.IsEnabled = false;
                }
            }
        }

        private void Calculate_Vertex_Guards(object sender, RoutedEventArgs e)
        {
            Triangulation t1 = new Triangulation();
            if (!p1.closed)
            {
                p1.close();
                if (p1.clockwise && (p1.area() < 0)) p1.reverse();
                else if (!p1.clockwise && (p1.area() < 0)) p1.reverse();
                DiagonalSet d1 = t1.triangulate(p1);
                p1.addDiagonals(d1);
                p1.addDiagonalsToVertices(d1);
                TriangulationColoring CSet = t1.color(d1, p1); 		// 3 color the polygon
                CSet.setGuards(p1);
                Triangulation t2 = new Triangulation();
            }
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
            create_from_file.IsEnabled = true;
        }

        private void Polygon_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void SubLayout_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
