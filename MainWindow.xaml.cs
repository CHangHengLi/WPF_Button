using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text;

namespace ButtonDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        // 计数器用于重复按钮测试
        private int _repeatCount = 0;
        
        // 自定义命令实例
        public static readonly RoutedCommand CustomCommand = new RoutedCommand("Custom", typeof(MainWindow));

        // MVVM方式的自定义命令
        public ICommand CustomMvvmCommand { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            
            // 设置数据上下文为自身，用于命令绑定
            this.DataContext = this;
            
            // 初始化自定义命令
            InitializeCommands();
            
            // 初始化默认命令绑定
            InitializeDefaultCommands();

            // 创建MVVM自定义命令对象并绑定
            CustomMvvmCommand = new RelayCommand(
                param => ExecuteCustomCommand(),
                param => CanExecuteCustomCommand()
            );
        }

        #region 命令处理

        private void InitializeCommands()
        {
            // 注册自定义命令绑定
            CommandBindings.Add(new CommandBinding(CustomCommand, 
                ExecuteCustomCommandHandler, 
                CanExecuteCustomCommandHandler));
        }

        private void InitializeDefaultCommands()
        {
            // 处理Copy和Paste命令
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, 
                (sender, e) => { MessageBox.Show("复制命令已执行"); },
                (sender, e) => { e.CanExecute = true; }));
                
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, 
                (sender, e) => { MessageBox.Show("粘贴命令已执行"); },
                (sender, e) => { e.CanExecute = true; }));
        }

        private void ExecuteCustomCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            // 执行自定义命令逻辑
            MessageBox.Show("自定义命令已执行！");
        }

        private void CanExecuteCustomCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            // 确定命令是否可执行的逻辑
            e.CanExecute = true; // 在此示例中始终可执行
        }
        
        // MVVM式命令实现
        private void ExecuteCustomCommand()
        {
            MessageBox.Show("MVVM自定义命令已执行！");
        }
        
        private bool CanExecuteCustomCommand()
        {
            return true; // 在此示例中始终可执行
        }

        #endregion

        #region 按钮事件处理

        private void StandardButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("标准按钮被点击了！");
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("默认按钮被点击了！你也可以通过按Enter键来触发此按钮。");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("取消按钮被点击了！你也可以通过按Esc键来触发此按钮。");
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            eventText.Text = "ToggleButton: 选中状态";
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            eventText.Text = "ToggleButton: 未选中状态";
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            _repeatCount++;
            eventText.Text = $"重复按钮点击次数: {_repeatCount}";
        }

        private void MessageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("这是一个简单的按钮点击消息框！", "按钮演示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CircleButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("自定义圆形按钮被点击了！", "自定义按钮演示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #region 事件测试

        private StringBuilder _eventLog = new StringBuilder();

        private void EventButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LogEvent("PreviewMouseDown");
        }

        private void EventButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LogEvent("MouseDown");
        }

        private void EventButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            LogEvent("PreviewMouseUp");
        }

        private void EventButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LogEvent("MouseUp");
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            LogEvent("Click");
            
            // 显示完整的事件序列
            MessageBox.Show($"按钮事件序列:\n{_eventLog.ToString()}", "事件序列");
            
            // 重置日志
            _eventLog.Clear();
            eventText.Text = "事件信息将显示在这里";
        }

        private void LogEvent(string eventName)
        {
            // 将事件添加到日志
            _eventLog.AppendLine(eventName);
            
            // 更新UI
            eventText.Text = $"最近触发事件: {eventName}";
        }

        #endregion

        #region 评分控件

        private void ResetRating_Click(object sender, RoutedEventArgs e)
        {
            ratingControl.Value = 0;  // 将评分重置为0
        }

        private void SetMaxRating_Click(object sender, RoutedEventArgs e)
        {
            ratingControl.Value = ratingControl.Maximum;  // 将评分设置为最大值
        }

        #endregion
    }

    #region MVVM 辅助类

    // 实现ICommand接口的简单命令类
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;    // 存储要执行的操作委托
        private readonly Predicate<object> _canExecute;    // 存储用于判断命令是否可执行的委托

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));    // 如果execute为null，抛出参数空异常
            _canExecute = canExecute;    // canExecute可以为null，表示命令总是可执行的
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);    // 如果_canExecute为null，返回true；否则调用_canExecute委托
        }

        public void Execute(object parameter)
        {
            _execute(parameter);    // 调用_execute委托执行命令操作
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }    // 添加事件处理程序到CommandManager的RequerySuggested事件
            remove { CommandManager.RequerySuggested -= value; }    // 从CommandManager的RequerySuggested事件中移除事件处理程序
        }
    }

    #endregion
} 