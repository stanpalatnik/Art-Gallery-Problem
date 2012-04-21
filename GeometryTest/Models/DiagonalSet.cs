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
        List<Edge> diagonalSet = new List<Edge>();
        public DiagonalSet() { }
        public void addDiagonal(ColoredPoint i, ColoredPoint j, ColoredPoint cutOff)
        {
            diagonalSet.Add(new Edge(i, j, cutOff));
        }
        public Edge getDiagonal(int i)
        {
            return diagonalSet[i];
        }
        public int getSize()
        {
            return diagonalSet.Count;
        }
        public int isInDiagSet(ColoredPoint a, ColoredPoint b)
        {
            for (int i = 0; i < diagonalSet.Count; i++)
            {
                if (((diagonalSet[i].getStart().point.X == a.point.X) &&
                  (diagonalSet[i].getStart().point.Y == a.point.Y) &&
                  (diagonalSet[i].getEnd().point.X == b.point.X) &&
                  (diagonalSet[i].getEnd().point.Y == b.point.Y))
                  ||
                  ((diagonalSet[i].getEnd().point.X == a.point.X) &&
                  (diagonalSet[i].getEnd().point.Y == a.point.Y) &&
                  (diagonalSet[i].getStart().point.X == b.point.X) &&
                  (diagonalSet[i].getStart().point.Y == b.point.Y)))
                    return i;
            }
            return -1;
        }
        public DiagonalSet merge(DiagonalSet d2)
        {
            int d2size = d2.getSize();

            for (int j = 0; j < d2size; j++)
            {
                diagonalSet.Add(d2.getDiagonal(j));
            }

            return this;
        }
    }
}
