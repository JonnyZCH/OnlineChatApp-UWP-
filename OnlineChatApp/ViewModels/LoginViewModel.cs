using Microsoft.AspNet.SignalR.Client;
using OnlineChatApp.Common;
using OnlineChatApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Networking.Connectivity;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OnlineChatApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            LoginCommand = new Command(async p =>
            {
                var model = p as LoginViewModel;
                if (model == null)
                {
                    return;
                }
                model.Loading = true;

                #region 无法复用，需要定义到App.xaml.cs中
                ////连接到服务端
                //var hubConnection = new HubConnection("http://localhost:58034");
                ////客户端需要调用服务端Hub  必须创建一个Hub代理
                //var hubProxy = hubConnection.CreateHubProxy("AccountHub");
                ////注册客户端方法
                //hubProxy.On<string>("LoginFailedCallback", async msg =>
                //  {
                //      System.Diagnostics.Debug.WriteLine(msg);
                //      //windows.Current不是无处不在的   CoreApplication.MainView也能调用Dispatcher
                //      await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                //        {
                //            // model.Loading = false;
                //        });
                //  });
                //hubProxy.On<Models.User>("LoginSuccessCallback", user =>
                // {
                //     System.Diagnostics.Debug.WriteLine("Success" + user.Username);
                //     //await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                //     //{
                //     //    //model.Loading = false;
                //     //});
                // });
                ////启动连接
                ////在UWP应用程序中必须要指定WebSocketTransport或LongPollingTransport方式传输
                ////否则将会有灾难性的延迟
                //await hubConnection.Start(new WebSocketTransport());
                ////调用服务端方法
                //await hubProxy.Invoke("Login", model.Username, model.Password); 
                #endregion

                //注册客户端失败回调方法
                App.Current.AccountHubProxy.On<string>("LoginFailedCallback", async msg =>
                {
                    System.Diagnostics.Debug.WriteLine(msg);
                    //windows.Current不是无处不在的   CoreApplication.MainView也能调用Dispatcher
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        model.Loading = false;
                    });
                });
                //注册客户端成功回调方法
                App.Current.AccountHubProxy.On<Models.User>("LoginSuccessCallback", async user =>
                {
                    System.Diagnostics.Debug.WriteLine("Success" + user.Username);
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        App.Current.CurrentUser = user;
                        //登录成功  跳转到主页面
                        var rootFrame = Window.Current.Content as Frame;
                        rootFrame.Navigate(typeof(MainPage), user);
                    });
                });
                try
                {
                    //调用服务器函数登录
                    await App.Current.AccountHubProxy.Invoke("Login", model.Username, model.Password, GetHardwareInfo(), GetInternetStatus());
                }
                catch (Exception)
                {
                    await new MessageDialog("网络连接出错！").ShowAsync();
                }

            },
            p =>
            {
                var model = p as LoginViewModel;
                if (model == null)
                {
                    return false;
                }
                return !string.IsNullOrEmpty(model.Username) && !string.IsNullOrEmpty(model.Password);
            });

            AboutCommand = new Command(async p =>
            {
                await Launcher.LaunchUriAsync(new Uri("http://www.qq.com"));
            });

            //NewUserCommand = new Command();
        }

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                SetProperty(ref username, value);
                LoginCommand.OnCanExecuteChanged();
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                SetProperty(ref password, value);
                LoginCommand.OnCanExecuteChanged();
            }
        }

        //加载状态
        private bool loading;
        public bool Loading
        {
            get { return loading; }
            set { SetProperty(ref loading, value); }
        }


        public Command LoginCommand { get; set; }
        public Command AboutCommand { get; set; }
        public Command NewUserCommand { get; set; }

        /// <summary>
        ///获取当前设备型号 
        /// </summary>
        /// <returns>设备型号</returns>
        public string GetHardwareInfo()
        {
            string info = null;
            EasClientDeviceInformation deviceInfo = new EasClientDeviceInformation();
            if (deviceInfo.FriendlyName.Substring(0, 7) == "DESKTOP")
            {
                info = "Windows";
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(deviceInfo.FriendlyName + "在线");
                info = deviceInfo.FriendlyName;
            }
            return info;
        }

        /// <summary>
        /// 获取当前网络环境
        /// </summary>
        /// <returns>网络环境信息</returns>
        public string GetInternetStatus()
        {
            const string IIG = "2G";
            const string IIIG = "3G";
            const string IVG = "4G";
            const string Wifi = "WIFI";
            const string Lan = "";

            string InternetType = null;
            ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();
            if (profile.IsWwanConnectionProfile)
            {
                if (profile.WwanConnectionProfileDetails == null)
                {
                    InternetType = string.Empty;
                }
                WwanDataClass connectionClass = profile.WwanConnectionProfileDetails.GetCurrentDataClass();
                switch (connectionClass)
                {
                    //2G网络
                    case WwanDataClass.Edge:
                    case WwanDataClass.Gprs:
                        InternetType = IIG;
                        break;
                    //3G网络
                    case WwanDataClass.Cdma1xEvdo:
                    case WwanDataClass.Cdma1xEvdoRevA:
                    case WwanDataClass.Cdma1xEvdoRevB:
                    case WwanDataClass.Cdma1xEvdv:
                    case WwanDataClass.Cdma1xRtt:
                    case WwanDataClass.Cdma3xRtt:
                    case WwanDataClass.CdmaUmb:
                    case WwanDataClass.Umts:
                    case WwanDataClass.Hsdpa:
                    case WwanDataClass.Hsupa:
                        InternetType = IIIG;
                        break;
                    //4G
                    case WwanDataClass.LteAdvanced:
                        InternetType = IVG;
                        break;
                    //无网
                    case WwanDataClass.None:
                        InternetType = string.Empty;
                        break;
                    default:
                        InternetType = string.Empty;
                        break;
                }
            }
            else if (profile.IsWlanConnectionProfile)
            {
                InternetType = Wifi;
            }
            else
            {
                //不是Wifi也不是蜂窝数据判断为Lan，返回空
                InternetType = Lan;
            }
            return InternetType;

        }
    }
}
