using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ButtonDemo
{
    /// <summary>
    /// 将布尔值转换为可见性的转换器
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 将布尔值转换为Visibility枚举值
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">转换参数，若为"Invert"则反转转换逻辑</param>
        /// <param name="culture">区域性信息</param>
        /// <returns>Visibility值</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (value is bool boolean) && boolean;
            
            // 检查是否需要反转逻辑
            if (parameter is string param && param.Equals("Invert", StringComparison.OrdinalIgnoreCase))
            {
                isVisible = !isVisible;
            }
            
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// 将Visibility枚举值转换回布尔值
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">转换参数，若为"Invert"则反转转换逻辑</param>
        /// <param name="culture">区域性信息</param>
        /// <returns>布尔值</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = value is Visibility visibility && visibility == Visibility.Visible;
            
            // 检查是否需要反转逻辑
            if (parameter is string param && param.Equals("Invert", StringComparison.OrdinalIgnoreCase))
            {
                isVisible = !isVisible;
            }
            
            return isVisible;
        }
    }
} 