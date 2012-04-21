using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeometryTest
{
    //This class adds a color proprety to a coordinate point using the behavior design
    class ColoredPoint
    {
        public Point point;
        public int index { get; set; }
        public enum color { None, Blue, Red, Yellow };
        public color vertexColor { get; set; }
        public bool isGuard;

        public ColoredPoint(double x, double y)
        {
            point = new Point(x, y);
            index = 0;
            isGuard = false;
        }
    }
}
