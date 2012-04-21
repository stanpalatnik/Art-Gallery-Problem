using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeometryTest.Models
{
    class DiagonalSet
    {
        Edge[] dSet = new Edge[200];
        public int size = 0;

        public DiagonalSet() { }
        public void addDiagonal(ColoredPoint i, ColoredPoint j, ColoredPoint cutOff)
        {
            dSet[size] = new Edge(i, j, cutOff);
            size++;
        }
        public Edge getDiagonal(int i)
        {
            return dSet[i];
        }
        public int getSize()
        {
            return size;
        }
        public int isInDiagSet(ColoredPoint a, ColoredPoint b)
        {
            for (int i = 0; i < size; i++)
            {
                if (((dSet[i].getStart().point.X == a.point.X) &&
                  (dSet[i].getStart().point.Y == a.point.Y) &&
                  (dSet[i].getEnd().point.X == b.point.X) &&
                  (dSet[i].getEnd().point.Y == b.point.Y))
                  ||
                  ((dSet[i].getEnd().point.X == a.point.X) &&
                  (dSet[i].getEnd().point.Y == a.point.Y) &&
                  (dSet[i].getStart().point.X == b.point.X) &&
                  (dSet[i].getStart().point.Y == b.point.Y)))
                    return i;
            }
            return -1;
        }
        public DiagonalSet merge(DiagonalSet d2)
        {
            int d2size = d2.getSize();
            int i = size;

            for (int j = 0; j < d2size; j++, i++, size++)
            {
                dSet[size] = d2.getDiagonal(j);
            }

            return this;
        }
    }
}
