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
        Point strt;
        Point nd;
        Point cutoff;

        public Edge(Point p1, Point p2)
        {
            strt = p1;
            nd = p2;
        }
        public Edge(Point p1, Point p2, Point cut)
        {
            strt = p1;
            nd = p2;
            cutoff = cut;
        }
        public Point getCutPnt()
        {
            return cutoff;
        }
        public Point getEnd()
        {
            return nd;
        }
        public Point getStart()
        {
            return strt;
        }
    }
}
