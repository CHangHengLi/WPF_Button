using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ButtonDemo
{
    /// <summary>
    /// 自定义圆形按钮控件
    /// </summary>
    public class CircleButton : Button
    {
        static CircleButton()
        {
            // 覆盖默认样式
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CircleButton),
                new FrameworkPropertyMetadata(typeof(CircleButton)));  // 将默认样式键设置为CircleButton类型，确保应用时查找正确的样式
        }
        
        // 圆的半径
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }  // 获取Radius依赖属性的值
            set { SetValue(RadiusProperty, value); }          // 设置Radius依赖属性的值
        }
        
        // 定义Radius依赖属性
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(
                "Radius",                    // 属性名
                typeof(double),              // 属性类型
                typeof(CircleButton),        // 属性所有者类型
                new PropertyMetadata(20.0)); // 默认值为20.0
        
        // 填充颜色
        public Brush CircleFill
        {
            get { return (Brush)GetValue(CircleFillProperty); }
            set { SetValue(CircleFillProperty, value); }
        }
        
        // 定义CircleFill依赖属性
        public static readonly DependencyProperty CircleFillProperty =
            DependencyProperty.Register(
                "CircleFill",                // 属性名
                typeof(Brush),               // 属性类型
                typeof(CircleButton),        // 属性所有者类型
                new PropertyMetadata(Brushes.RoyalBlue)); // 默认为RoyalBlue
        
        // 鼠标悬停时的填充颜色
        public Brush HoverFill
        {
            get { return (Brush)GetValue(HoverFillProperty); }
            set { SetValue(HoverFillProperty, value); }
        }
        
        // 定义HoverFill依赖属性
        public static readonly DependencyProperty HoverFillProperty =
            DependencyProperty.Register(
                "HoverFill",                 // 属性名
                typeof(Brush),               // 属性类型
                typeof(CircleButton),        // 属性所有者类型
                new PropertyMetadata(Brushes.CornflowerBlue)); // 默认为CornflowerBlue
    }
} 