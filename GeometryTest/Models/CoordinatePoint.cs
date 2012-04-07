using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryTest
{
    class CoordinatePoint
    {
        public short Xcoord { get; set; }
        public short Ycoord { get; set; }
        public enum color { Blue, Red, Yellow };
        public color vertexColor { get; set; }

        public CoordinatePoint(short x, short y)
        {
            this.Xcoord = x;
            this.Ycoord = y;
        }
    }
}
