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
        public List<Edge> diagonalSet = new List<Edge>();
        public DiagonalSet() { }
        public void addDiagonal(ColoredPoint i, ColoredPoint j, ColoredPoint cutOff)
        {
            diagonalSet.Add(new Edge(i, j, cutOff));
        }
        public Edge getDiagonal(int i)
        {
            return diagonalSet[i];
        }

        public int isInDiagSet(ColoredPoint a, ColoredPoint b)
        {
            for (int i = 0; i < diagonalSet.Count; i++)
            {
                if (((diagonalSet[i].Start.point.X == a.point.X) &&
                  (diagonalSet[i].Start.point.Y == a.point.Y) &&
                  (diagonalSet[i].End.point.X == b.point.X) &&
                  (diagonalSet[i].End.point.Y == b.point.Y))
                  ||
                  ((diagonalSet[i].End.point.X == a.point.X) &&
                  (diagonalSet[i].End.point.Y == a.point.Y) &&
                  (diagonalSet[i].Start.point.X == b.point.X) &&
                  (diagonalSet[i].Start.point.Y == b.point.Y)))
                    return i;          

            }
            return -1;
        }

        public int isInDiagSet2(ColoredPoint a, ColoredPoint b)
        {
            for (int i = 1; i < diagonalSet.Count; i++)
            {
                if (((diagonalSet[i].Start.point.X == a.point.X) &&
                  (diagonalSet[i].Start.point.Y == a.point.Y) &&
                  (diagonalSet[i].End.point.X == b.point.X) &&
                  (diagonalSet[i].End.point.Y == b.point.Y))
                  ||
                  ((diagonalSet[i].End.point.X == a.point.X) &&
                  (diagonalSet[i].End.point.Y == a.point.Y) &&
                  (diagonalSet[i].Start.point.X == b.point.X) &&
                  (diagonalSet[i].Start.point.Y == b.point.Y)))
                    return i;
            }
            return -1;
        }
        public DiagonalSet merge(DiagonalSet d2)
        {
            int d2size = d2.diagonalSet.Count;

            for (int j = 0; j < d2size; j++)
            {
                diagonalSet.Add(d2.getDiagonal(j));
            }

            return this;
        }
    }
}
