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

        private void Path_Loaded(object sender, RoutedEventArgs e)
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
    }
}
