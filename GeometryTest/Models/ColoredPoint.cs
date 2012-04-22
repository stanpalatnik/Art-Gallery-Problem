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
        private bool isGuard;
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
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

        public ColoredPoint(double x, double y)
        {
            point = new Point(x, y);
            index = 0;
            isGuard = false;
        }
    }
}
