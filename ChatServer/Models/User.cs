using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace ChatServer.Models
{
    public partial class User : INotifyPropertyChanged
    {
        //替代数据库
        static User()
        {
            Storages = new List<User>
            {
                new User {Id=1,Username="383190650",Password="1431748WYT",NickName="烧锅炉的北极熊",Signature="有谁能比我知道，你的温柔像羽毛"},
                new User {Id=2,Username="250821283",Password="1431748",NickName="一叶知秋",Signature="How can we go back to the way it used to be?",GroupName="test1"},
                new User {Id=3,Username="666666",Password="666666", NickName="OFF THE Hardwell",Signature="我是傻逼英杰" ,GroupName="test"},
                new User {Id=4,Username="555555",Password="555555", NickName="左俊杰",Signature="我是傻逼左俊杰",GroupName="test1" },
                new User {Id=5,Username="123456",Password="123456", NickName="王司徒",Signature="扫清六合，席卷八荒!",GroupName="test1"},
                new User {Id=6,Username="234567",Password="234567", NickName="诸葛孔明",Signature="我从未见过如此厚颜无耻之人！",GroupName="test2"},
                new User {Id=7,Username="345678",Password="345678", NickName="刘醒",Signature="吔屎啦梁非凡！",GroupName="test2"},
                new User {Id=8,Username="456789",Password="456789", NickName="梁非凡",Signature="下属不可以顶上司嘴！" ,GroupName="test2"},
            };
        }

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

        public static IList<User> Storages { get; set; }


        /// <summary>
        /// NotifyPropertyChanged接口实现
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            //数据源发生变化  通知所有绑定该属性的对象进行数据同步
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SetProperty<NotiProperty>(ref NotiProperty original, NotiProperty NewValue, [CallerMemberName]string propName = null)
        {
            //原始值与新值相同时
            if (Equals(original, NewValue))
            {
                return;
            }
            //不相等时赋值
            original = NewValue;
            if (string.IsNullOrEmpty(propName))
            {
                return;
            }
            OnPropertyChanged(propName);
        }
    }

    public enum UserStatus
    {
        Offline = 0,
        Online = 1,
    }
}