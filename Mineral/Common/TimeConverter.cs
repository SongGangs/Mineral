using System;
using System.Windows.Data;

namespace Mineral.Common
{

    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DoubleToTime((double)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return TimeSpan.FromSeconds((double)value);
        }
        public string DoubleToTime(double time)
        {
            int hour = 0;
            int minute = 0;
            int second = 0;
            second = (Int32)time;
            if (second > 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            return (hour + ":" + minute + ":" + second);
        }   
    }
}
