using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ButtonDemo
{
    /// <summary>
    /// 使用按钮实现的评分控件
    /// </summary>
    public class RatingControl : Control
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",                    // 属性名：当前评分值
                typeof(int),                // 属性类型：整数
                typeof(RatingControl),      // 所有者类型：RatingControl
                new PropertyMetadata(0, OnValueChanged));  // 默认值为0，并指定属性变更时的回调方法
                
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }  // 获取当前评分值
            set { SetValue(ValueProperty, value); }       // 设置当前评分值
        }
        
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum",                  // 属性名：最大评分值
                typeof(int),                // 属性类型：整数
                typeof(RatingControl),      // 所有者类型：RatingControl
                new PropertyMetadata(5, OnMaximumChanged));  // 默认值为5，并指定属性变更时的回调方法
                
        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }  // 获取最大评分值
            set { SetValue(MaximumProperty, value); }       // 设置最大评分值
        }
        
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RatingControl)d).UpdateStars();  // 当Value属性变更时，更新星星显示
        }
        
        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RatingControl)d).UpdateStars();  // 当Maximum属性变更时，更新星星显示
        }
        
        private StackPanel _starPanel;  // 用于存放星星按钮的面板
        
        static RatingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(RatingControl),
                new FrameworkPropertyMetadata(typeof(RatingControl)));  // 设置默认样式键
        }
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _starPanel = GetTemplateChild("PART_StarPanel") as StackPanel;  // 从模板中获取名为"PART_StarPanel"的面板元素
            UpdateStars();  // 应用模板后更新星星显示
        }
        
        private void UpdateStars()
        {
            if (_starPanel == null) return;  // 如果面板未初始化，直接返回
            
            _starPanel.Children.Clear();  // 清除现有的所有星星按钮
            
            for (int i = 1; i <= Maximum; i++)
            {
                Button starButton = new Button();
                starButton.Content = i <= Value ? "★" : "☆";  // 如果当前索引小于等于Value，显示实心星星，否则显示空心星星
                starButton.Tag = i;  // 将按钮索引保存在Tag属性中
                starButton.Click += StarButton_Click;  // 添加点击事件处理
                starButton.Margin = new Thickness(2);
                starButton.Width = 30;
                starButton.Height = 30;
                starButton.FontSize = 18;
                starButton.Padding = new Thickness(0);
                starButton.BorderThickness = new Thickness(0);
                
                // 设置不同状态的颜色
                if (i <= Value)
                {
                    starButton.Foreground = Brushes.Gold;  // 选中的星星为金色
                    starButton.Background = Brushes.Transparent;
                }
                else
                {
                    starButton.Foreground = Brushes.Gray;  // 未选中的星星为灰色
                    starButton.Background = Brushes.Transparent;
                }
                
                _starPanel.Children.Add(starButton);  // 将按钮添加到面板中
            }
        }
        
        private void StarButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Value = (int)button.Tag;  // 点击星星时，将Value设置为对应星星的索引
            }
        }
    }
} 