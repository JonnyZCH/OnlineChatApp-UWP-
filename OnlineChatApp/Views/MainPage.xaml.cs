using Microsoft.AspNet.SignalR.Client;
using OnlineChatApp.Models;
using OnlineChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static OnlineChatApp.Models.User;

namespace OnlineChatApp.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; set; }
        public MainPage()
        {
            ViewModel = new MainViewModel();
            this.InitializeComponent();
        }

        /// <summary>
        /// 导航到该页面时接收传递过来的用户数据
        /// </summary>
        /// <param name="e">传递过来的数据</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //接收当前登录的用户信息
            ViewModel.CurrentUser = e.Parameter as User;
            //接收当前登录用户的好友分组信息
            App.Current.DataHubProxy.On<ObservableCollection<FriendGroup>>("GetFriendGroupCallback", async data =>
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    ViewModel.Friends = new CollectionViewSource();
                    ViewModel.Friends.IsSourceGrouped = true;
                    ViewModel.Friends.ItemsPath = new PropertyPath("Friends");
                    ViewModel.Friends.Source = data;
                });
            });

            App.Current.AccountHubProxy.On<User>("NewFriendOnline", async user =>
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var list = ViewModel.Friends.Source as IEnumerable<FriendGroup>;
                    var group = list.FirstOrDefault(g => g.GroupName == user.GroupName);
                    var onlineUser = group.Friends.FirstOrDefault(u => u.Username == user.Username);
                    onlineUser.Status = UserStatus.Online;
                    onlineUser.Token = user.Token;
                });
            });

            App.Current.DataHubProxy.Invoke("GetFriendGroup");
        }
    }
}
