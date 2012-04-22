using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryTest
{
    public class PointCollectionConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.GetType() == typeof(System.Collections.ObjectModel.ObservableCollection<ColoredPoint>) && targetType == typeof(System.Windows.Media.PointCollection))
            {
                var pointCollection = new System.Windows.Media.PointCollection();
                foreach (var coloredPoint in value as System.Collections.ObjectModel.ObservableCollection<ColoredPoint>)
                    pointCollection.Add(coloredPoint.point);
                return pointCollection;
            }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
