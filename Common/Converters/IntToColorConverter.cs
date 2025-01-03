﻿using System.Globalization;
using System.Windows.Data;

namespace MyToDo.Common.Converters
{
    class IntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value == 0 ? "#FFC107" : "#00BCD4";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
