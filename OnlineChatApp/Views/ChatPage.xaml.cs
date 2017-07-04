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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.AspNet.SignalR.Client;
using Windows.ApplicationModel.Core;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Networking.Connectivity;
using Windows.System;
using Windows.UI.Core;
using OnlineChatApp.Common;
using WinRTXamlToolkit.Input;
using Windows.UI.Xaml.Input;
using User = OnlineChatApp.Models.User;

namespace OnlineChatApp.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ChatPage : Page
    {
        public ChatViewModel ViewModel { get; set; }
        public ChatPage()
        {
            //ViewModel = new ChatViewModel();

            ViewModel = App.Current.ViewModel;
            InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //聊天目标
            ViewModel.Target = e.Parameter as User;
            #region 退出聊天窗口后无法接收到消息，故将接收消息逻辑定义到app.xaml.cs中
            //接收消息并将消息存入Message
            //App.Current.ChatHubProxy.On<User, User, string, DateTime>("Recived", async (t, u, s, d) =>
            // {
            //     await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //     {
            //         ViewModel.Messages.Add(new Message
            //         {
            //             FromUsername = u.Username,
            //             ToUsername = t.Username,
            //             TargetNickName = t.NickName,
            //             SendTime = d.ToString(),
            //             IsRead = false,
            //             IsSelf = u.Username.Equals(App.Current.CurrentUser.Username),
            //             Content = s,
            //         });
            //     });
            // });
            #endregion

            //目标当前登录的设备及网络信息
            hardwareInfo.Text = ViewModel.Target.HardwareInfo + " - " + ViewModel.Target.NetworkType + "在线";
        }

        /// <summary>
        /// 后退按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        /// <summary>
        /// 将消息滚动到最新的一项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MessageList_OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            MessageList.ScrollIntoViewSmoothly(MessageList.Items[MessageList.Items.Count - 1]);
        }

    }
}
