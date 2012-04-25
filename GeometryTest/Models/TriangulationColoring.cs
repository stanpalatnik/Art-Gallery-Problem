using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryTest.Models
{
    class TriangulationColoring
    {
        const int MAX_COLORS = 3;
        int[] vertexColors = new int[MAX_COLORS];

        internal void setupColors(Polygon p)
        {
            foreach (ColoredPoint p1 in p.vertices)
            {
                if (p1.IsDuplicate == false)
                {
                    vertexColors[(int)p1.vertexColor]++;
                }             
            }
        }

        public void add(ColoredPoint p)
        {
            vertexColors[(int)p.vertexColor]++;
        }

        public void add(TriangulationColoring CSet)
        {
            if (CSet != null)
                vertexColors = CSet.vertexColors;
        }

        /**
        * This method was created by a SmartGuide.
        * @return int
        */
        internal int getMinColorClass()
        {
            int min = 1;
            if (vertexColors[min] > vertexColors[2]) min = 2;
            if (vertexColors[min] > vertexColors[2]) min = 3;
            return min;
        }
        /**
         * This method was created by a SmartGuide.
         */
        public void setGuards(Polygon p)
        {
            int curColor = 0;
            setupColors(p);
            int minColorClass = getMinColorClass();
            for (int j = 0; j < p.vertices.Count; j++)
            {
                curColor = (int)p.vertices[j].vertexColor;
                if (curColor == minColorClass)
                {
                    p.vertices[j].IsGuard = true;
                }
            }
            return;
        }
    }
}
