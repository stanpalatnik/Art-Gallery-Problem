using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeometryTest.Models
{
    class Edge
    {
        ColoredPoint start;
        ColoredPoint end;
        ColoredPoint cutoff;

        public Edge(ColoredPoint p1, ColoredPoint p2)
        {
            start = p1;
            end = p2;
        }
        public Edge(ColoredPoint p1, ColoredPoint p2, ColoredPoint cut)
        {
            start = p1;
            end = p2;
            cutoff = cut;
        }
        public ColoredPoint getCutPnt()
        {
            return cutoff;
        }
        public ColoredPoint getEnd()
        {
            return end;
        }
        public ColoredPoint getStart()
        {
            return start;
        }
    }
}
