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
        private ColoredPoint start;

        public ColoredPoint Start
        {
            get { return start; }
            set { start = value; }
        }
        private ColoredPoint end;

        public ColoredPoint End
        {
            get { return end; }
            set { end = value; }
        }
        private ColoredPoint cutoff;

        public ColoredPoint Cutoff
        {
            get { return cutoff; }
            set { cutoff = value; }
        }

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
    }
}
