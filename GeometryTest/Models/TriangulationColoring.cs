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
        int[] vertexColors = new int[MAX_COLORS+1];
        private int size = 0;

        internal void setupColors(Polygon p)
        {
            foreach (ColoredPoint point in p.vertices)
            {
                if (point.IsDuplicate == false)
                {
                    vertexColors[(int)point.vertexColor]++;
                }             
            }
        }

        public void add(ColoredPoint p)
        {
            if (p.IsDuplicate == false)
            {
                vertexColors[(int)p.vertexColor]++;
                size++;
            }    
        }

        public void add(TriangulationColoring CSet)
        {
            if (CSet != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    vertexColors[i] += CSet.vertexColors[i];
                }
            }
        }

        /**
        * This method was created by a SmartGuide.
        * @return int
        */
        internal int getMinColorClass()
        {
            int min = (int)ColoredPoint.color.Blue;
            if (vertexColors[min] > vertexColors[1]) min = (int)ColoredPoint.color.Red;
            if (vertexColors[min] > vertexColors[2]) min = (int)ColoredPoint.color.Yellow;
            return min;
        }
        /**
         * This method was created by a SmartGuide.
         */
        public void setGuards(Polygon p)
        {
            int curColor = 0;
            setupColors(p);
            int minColorClass = getMinColorClass()+1;
            for (int j = 0; j < p.vertices.Count; j++)
            {
                curColor = (int)p.vertices[j].vertexColor;
                if (curColor == minColorClass)
                {
                    p.vertices[j].IsGuard = true;
                }
            }
            //onpropertychanged
            return;
        }
    }
}
