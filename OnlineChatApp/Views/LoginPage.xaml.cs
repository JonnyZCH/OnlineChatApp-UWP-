using OnlineChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace OnlineChatApp.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private static bool isExiting = false;
        //当前页面的视图模型
        public LoginViewModel ViewModel { get; set; }
        public static bool IsHardwareButtonsAPIPresent;
        public LoginPage()
        {
            //构建视图模型对象
            ViewModel = new LoginViewModel();
            this.InitializeComponent();
            //进入动画
            fadeIn.Begin();
            //注册动画的完成事件
            //tipsFade.Completed += (p1, p2) => { isExiting = false; };

        }

        //private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        //{
        //    Frame rootFrame = Window.Current.Content as Frame;
        //    if (isExiting)
        //    {
        //        Application.Current.Exit();
        //    }
        //    else
        //    {
        //        tipsFade.Begin();
        //        isExiting = true;
        //        e.Handled = true;
        //    }
        //}
    }
}
