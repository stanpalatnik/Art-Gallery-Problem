using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeometryTest.Models
{
    class ColorSet
    {
        Point[] CVSet = new Point[300];
        public int size = 0;
        int[] colors = new int[3];

        public ColorSet()
        {
            colors[0] = 0;
            colors[1] = 0;
            colors[2] = 0;
        }
        void add(ColorSet CSet)
        {
            if (CSet != null)
                for (int j = 0; j < CSet.size; j++)
                {
                    CVSet[size] = CSet.getPnt(j);
                    colors[CVSet[size].getColor()]++;
                    size++;
                }
        }
        void add(Point p)
        {
            CVSet[size] = p;
            colors[p.getColor()]++;
            size++;
        }
        /**
         * This method was created by a SmartGuide.
         * @return int
         */
        public int getMinColorClass()
        {
            int min = 0;
            if (colors[min] > colors[1]) min = 1;
            if (colors[min] > colors[2]) min = 2;
            return min;
        }
        Point getPnt(int i)
        {
            return CVSet[i];
        }
        /**
         * This method was created by a SmartGuide.
         */
        public void setGuards()
        {
            int curColor = 0;
            int minColorClass = getMinColorClass();
            for (int j = 0; j < size; j++)
            {
                curColor = CVSet[j].getColor();
                if (curColor == minColorClass)
                {
                    CVSet[j].guard = true;
                }
            }
            return;
        }
    }
}
