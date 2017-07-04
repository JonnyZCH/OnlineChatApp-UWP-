using OnlineChatApp.Common;
using OnlineChatApp.ViewModels;
using OnlineChatApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OnlineChatApp.Models
{
    public partial class User : ViewModelBase
    {
        public User()
        {
            ClickCommand = new Command(async p =>
            {
                await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                 {
                     var rootFrame = Window.Current.Content as Frame;
                     rootFrame.Navigate(typeof(ChatPage), p);
                 });
            });
        }

        //public int Id { get; set; }
        //public string Username { get; set; }
        //public string Password { get; set; }
        //public string NickName { get; set; }
        //public string Signature { get; set; }//签名
        //public string GroupName { get; set; }
        //public string Token { get; set; }

        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private string nickName;
        public string NickName
        {
            get { return nickName; }
            set { SetProperty(ref nickName, value); }
        }

        private string signature;
        public string Signature
        {
            get { return signature; }
            set { SetProperty(ref signature, value); }
        }

        private string groupName;
        public string GroupName
        {
            get { return groupName; }
            set { SetProperty(ref groupName, value); }
        }

        private string token;
        public string Token
        {
            get { return token; }
            set { SetProperty(ref token, value); }
        }

        private string hardwareInfo;
        public string HardwareInfo
        {
            get { return hardwareInfo; }
            set { SetProperty(ref hardwareInfo, value); }
        }

        private string networkType;
        public string NetworkType
        {
            get { return networkType; }
            set { SetProperty(ref networkType, value); }
        }

        private UserStatus status;
        public UserStatus Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        public Command ClickCommand { get; set; }
    }

    public enum UserStatus
    {
        Online = 1,
        Offline = 0,
    }
}
