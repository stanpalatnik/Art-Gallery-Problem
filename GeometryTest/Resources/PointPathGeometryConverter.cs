﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GeometryTest
{
    public class PointPathGeometryConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.GetType() == typeof(System.Collections.ObjectModel.ObservableCollection<ColoredPoint>))
            {
                GeometryGroup myGeometryGroup = new GeometryGroup();
                foreach (var coloredPoint in value as System.Collections.ObjectModel.ObservableCollection<ColoredPoint>)
                {
                    if (coloredPoint.IsGuard == false)
                    {
                        EllipseGeometry myEllipseGeometry = new EllipseGeometry();
                        myEllipseGeometry.Center = new Point(coloredPoint.point.X, coloredPoint.point.Y);
                        myEllipseGeometry.RadiusX = 4;
                        myEllipseGeometry.RadiusY = 4;
                        if (!myGeometryGroup.FillContains(myEllipseGeometry.Center))
                        {
                            myGeometryGroup.Children.Add(myEllipseGeometry);
                        }
                    }   
                }
                return myGeometryGroup;
            }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
