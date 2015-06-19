using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Bootcamp2015.AmazingRace.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public Visibility IsTrue { get; set; }
        public Visibility IsFalse { get; set; }

        public BooleanToVisibilityConverter()
        {
            IsTrue = Visibility.Visible;
            IsFalse = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? IsTrue : IsFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
