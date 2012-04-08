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
        Point start;
        Point end;
        Point cutoff;

        public Edge(Point p1, Point p2)
        {
            start = p1;
            end = p2;
        }
        public Edge(Point p1, Point p2, Point cut)
        {
            start = p1;
            end = p2;
            cutoff = cut;
        }
        public Point getCutPnt()
        {
            return cutoff;
        }
        public Point getEnd()
        {
            return end;
        }
        public Point getStart()
        {
            return start;
        }
    }
}
