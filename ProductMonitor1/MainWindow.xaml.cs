using ProductMonitor.Views;
using ProductMonitor1.OpCommand;
using ProductMonitor1.UserControls;
using ProductMonitor1.ViewModels;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace ProductMonitor1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 视图模型
        /// </summary>
        MainWindowVM mainWindowVM = new MainWindowVM();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainWindowVM;
        }

        /// <summary>
        /// 显示车间详情页
        /// </summary>
        private void ShowDetailUC()
        {
            WorkShopDetailUC workShopDetailUC = new WorkShopDetailUC();
            mainWindowVM.MonitorUC = workShopDetailUC;

            //动画效果（从下到上）
            //位移 移动时间
            ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 50, 0, -50), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 400));

            //透明度
            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 400));

            Storyboard.SetTarget(thicknessAnimation, workShopDetailUC);
            Storyboard.SetTarget(doubleAnimation, workShopDetailUC);

            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(thicknessAnimation);
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }

        /// <summary>
        /// 返回到监控
        /// </summary>
        private void GoBackMonitor()
        {
            MonitorUC monitorUC = new MonitorUC();
            mainWindowVM.MonitorUC = monitorUC;
        }

        /// <summary>
        /// 展示详情命令
        /// </summary>
        public Command ShowDetailCmm
        {
            get
            {
                return new Command(ShowDetailUC);
            }
        }

        /// <summary>
        /// 返回到监控命令
        /// </summary>
        public Command GoBackCmm
        {
            get
            {
                return new Command(GoBackMonitor);
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Min(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Close(object sender, RoutedEventArgs e)
        {
           //this.Close() 关闭当前窗口
           Environment.Exit(0); //强制退出应用
        }

        /// <summary>
        /// 弹出窗口配置
        /// </summary>
        private void ShowSettingWin()
        {
            SettingsWin settingsWin = new SettingsWin() { Owner = this};
            settingsWin.ShowDialog();//showdialog，弹出的窗口不关闭，无法操作主窗口
        }

        /// <summary>
        /// 创建弹出窗口命令
        /// </summary>
        public Command ShowSettingCmm
        {
            get
            {
                return new Command(ShowSettingWin);
            }
        }
    }
}