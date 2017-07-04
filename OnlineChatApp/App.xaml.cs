using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using OnlineChatApp.Models;
using OnlineChatApp.ViewModels;
using OnlineChatApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace OnlineChatApp
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        public ChatViewModel ViewModel { get; set; }

        //本地服务器
        const string ServerAddress = "http://localhost:56998/";
        //const string ServerAddress = "http://qxw1152140092.my3w.com";

        //因为是父类成员而无法调用，故重写
        public new static App Current { get { return Application.Current as App; } }
        public HubConnection HubConnection { get; private set; }
        //Hub代理
        public IHubProxy AccountHubProxy { get; private set; }
        public IHubProxy DataHubProxy { get; private set; }
        public IHubProxy ChatHubProxy { get; private set; }
        //当前登录用户信息
        public User CurrentUser { get; set; }

        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            ViewModel = new ChatViewModel();
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            InitHubConnection();

            //判断当前是否支持物理back键
            IsHardwareButtonsAPIPresent = Windows.Foundation.Metadata.ApiInformation.IsTypePresent
                ("Windows.Phone.UI.Input.HardwareButtons");
            if (IsHardwareButtonsAPIPresent)
            {
                //添加back事件
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }

            //接收消息
            App.Current.ChatHubProxy.On<User, User, string, DateTime>("Recived", async (t, u, s, d) =>
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    ViewModel.Messages.Add(new Message
                    {
                        FromUsername = u.Username,
                        ToUsername = t.Username,
                        TargetNickName = t.NickName,
                        SendTime = d.ToString(),
                        IsRead = false,
                        IsSelf = u.Username.Equals(App.Current.CurrentUser.Username),
                        Content = s,
                    });
                });
            });

        }
        public static bool IsHardwareButtonsAPIPresent;
        //back事件    如果可以返回上一个页面则返回上一个页面
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                return;
            }
            if (rootFrame.CanGoBack)
            {
                //如果回退窗口记录大于1（1：代表上一个窗口是登录窗口），则回退，否则return
                if (rootFrame.BackStack.Count > 1)
                {
                    e.Handled = true;
                    rootFrame.GoBack();
                }

            }
        }

        private async void InitHubConnection()
        {
            try
            {
                HubConnection = new HubConnection(ServerAddress);
                AccountHubProxy = HubConnection.CreateHubProxy("AccountHub");
                DataHubProxy = HubConnection.CreateHubProxy("DataHub");
                ChatHubProxy = HubConnection.CreateHubProxy("ChatHub");
                HubConnection.Error += HubConnection_Error;
                HubConnection.TraceLevel = TraceLevels.All;
                await HubConnection.Start(new WebSocketTransport());
            }
            catch (Exception)
            {

                await new MessageDialog("网络连接出错！").ShowAsync();
            }
        }

        void HubConnection_Error(Exception obj)
        {

        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态 
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // 当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    rootFrame.Navigate(typeof(LoginPage), e.Arguments);
                }
                // 确保当前窗口处于活动状态
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }
    }
}
