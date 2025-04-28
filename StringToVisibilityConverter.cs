using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ButtonDemo
{
    /// <summary>
    /// 将字符串转换为可见性的转换器
    /// 如果字符串为null或空，则返回Collapsed，否则返回Visible
    /// </summary>
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text && !string.IsNullOrEmpty(text))
            {
                return Visibility.Visible;  // 如果字符串不为空，返回Visible
            }
            return Visibility.Collapsed;    // 如果字符串为空，返回Collapsed
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();  // 不支持反向转换
        }
    }
} 