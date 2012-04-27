using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeometryTest
{
    //This class adds a color proprety to a coordinate point using the behavior design
    class ColoredPoint :  INotifyPropertyChanged
    {
        public Point point {get; set;}
        public int index { get; set; }
        public enum color { None, Blue, Red, Yellow };
        public color vertexColor { get; set; }
        private bool isFromMouse;
        private bool isGuard;
        private bool isDuplicate;

        public event PropertyChangedEventHandler PropertyChanged;
        private ColoredPoint vertex;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public ColoredPoint(ColoredPoint point)
        {
            this.vertex = point.vertex;
            this.point = point.point;
            this.index = point.index;
            this.vertexColor = point.vertexColor;
            this.isGuard = point.IsGuard;
            this.isGuard = point.IsDuplicate;
            this.IsFromMouse = point.IsFromMouse;
        }

        public bool IsGuard
        {
            get
            {
                return this.isGuard;
            }

            set
            {
                if (value != this.isGuard)
                {
                    this.isGuard = value;
                    NotifyPropertyChanged("IsGuard");
                }
            }
        }

        public bool IsDuplicate
        {
            get { return isDuplicate; }
            set { isDuplicate = value; }
        }

        public bool IsFromMouse
        {
            get { return isFromMouse; }
            set { isFromMouse = value; }
        }

        public ColoredPoint(double x, double y, bool fromMouse = false)
        {
            point = new Point(x, y);
            index = -1;
            isGuard = false;
            isDuplicate = false;
            isFromMouse = fromMouse;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            ColoredPoint p = obj as ColoredPoint;
            if ((System.Object)p == null)
            {
                return false;
            }

            return (this.point.X == p.point.X) && (this.point.Y == p.point.Y);
        }

        public bool Equals(ColoredPoint p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            return (this.point.X == p.point.X) && (this.point.Y == p.point.Y);
        }

        public bool withinRoot(ColoredPoint p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }
            return ((Math.Abs(this.point.X - p.point.X) < 4)) && (Math.Abs(this.point.Y - p.point.Y) < 4);
        }
    }
}
