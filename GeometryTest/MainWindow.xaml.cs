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
            Polygon p1 = new Polygon();
            if (!File.Exists("input.txt"))
            {
                Console.WriteLine("input.txt does not exist.");
                return;
            }
            using (StreamReader sr = File.OpenText("input.txt"))
            {
                String input;
                while ((input = sr.ReadLine()) != null)
                {
                    Console.WriteLine(input);
                }
                Console.WriteLine("The end of the stream has been reached.");
            }
        }

        private void Calculate_Vertex_Guards(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Calculated_Data_To_Output(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Data(object sender, RoutedEventArgs e)
        {

        }
    }
}
