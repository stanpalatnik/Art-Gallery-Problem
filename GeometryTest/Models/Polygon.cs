using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeometryTest
{
    class Polygon
    {

        private static readonly Polygon instance = new Polygon();

        public List<CoordinatePoint> vertices {get; set;}

        private Polygon()
        {
            vertices = new List<CoordinatePoint>();
        }

        public static Polygon Instance
        {
            get
            {
                return instance;
            }
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
                        string[] words = input.Split(' ');
                        if (words.Length > 2)
                        {
                            throw new FormatException();
                        }
                        CoordinatePoint c1 = new CoordinatePoint(Convert.ToInt16(words[0]), Convert.ToInt16(words[1]));
                        this.vertices.Add(c1);
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
                foreach (CoordinatePoint vertex in vertices)
                {
                    input.Append("X: ").Append(vertex.Xcoord).Append(" Y: ").Append(vertex.Ycoord);
                }
            }

            return input.ToString();
        }
    }
}
