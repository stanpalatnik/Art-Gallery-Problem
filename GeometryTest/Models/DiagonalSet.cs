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
        void addDiagonal(Point i, Point j, Point cutOff)
        {
            dSet[size] = new Edge(i, j, cutOff);
            size++;
        }
        Edge getDiagonal(int i)
        {
            return dSet[i];
        }
        int getSize()
        {
            return size;
        }
        int isInDiagSet(Point a, Point b)
        {
            for (int i = 0; i < size; i++)
            {
                if (((dSet[i].getStart().X == a.X) &&
                  (dSet[i].getStart().Y == a.Y) &&
                  (dSet[i].getEnd().X == b.X) &&
                  (dSet[i].getEnd().Y == b.Y))
                  ||
                  ((dSet[i].getEnd().X == a.X) &&
                  (dSet[i].getEnd().Y == a.Y) &&
                  (dSet[i].getStart().X == b.X) &&
                  (dSet[i].getStart().Y == b.Y)))
                    return i;
            }
            return -1;
        }
        DiagonalSet merge(DiagonalSet d2)
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
