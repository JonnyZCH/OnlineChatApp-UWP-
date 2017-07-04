using OnlineChatApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace OnlineChatApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public User CurrentUser { get; set; }
        public ObservableCollection<Message> RecentMessages { get; set; }

        //好友分组
        public CollectionViewSource Friends { get; set; }
        public MainViewModel()
        {
#if DEBUG
            //CurrentUser = new User
            //{
            //    Username = "383190650",
            //    NickName = "烧锅炉的北极熊",
            //    Password = "1431748WYT",
            //    Signature = "签名测试",
            //};

            #region RecentMessages
            RecentMessages = new ObservableCollection<Message>
            {
                new Message {FromUsername="250821283",ToUsername="383190650",TargetNickName="一叶知秋", Content="有谁能比我知道 你的温柔像羽毛",IsRead=false,SendTime=DateTime.Now.ToString("hh:mm") },
            new Message {FromUsername = "666666", ToUsername = "383190650",TargetNickName="OFF THE Hardwell", Content = "我是傻逼英杰", IsRead = false, SendTime = DateTime.Now.ToString("hh:mm") },
            new Message {FromUsername = "555555", ToUsername = "383190650",TargetNickName="左俊杰", Content = "搞浪子几把", IsRead = false, SendTime = DateTime.Now.ToString("hh:mm") },
            new Message {FromUsername = "123456", ToUsername = "383190650",TargetNickName="王司徒", Content = "扫清六合，席卷八荒！", IsRead = false, SendTime = DateTime.Now.ToString("hh:mm") },
            new Message {FromUsername = "234567", ToUsername = "383190650",TargetNickName="诸葛孔明", Content = "我从未见过如何厚颜无耻之人！", IsRead = false, SendTime = DateTime.Now.ToString("hh:mm") },
            new Message {FromUsername = "345678", ToUsername = "383190650",TargetNickName="刘醒", Content = "吔屎啦梁非凡！", IsRead = false, SendTime = DateTime.Now.ToString("hh:mm") },
            new Message {FromUsername = "456789", ToUsername = "383190650",TargetNickName="梁非凡", Content = "下属不可以啵上司嘴", IsRead = false, SendTime = DateTime.Now.ToString("hh:mm") },
            };
            #endregion

            var temp = new ObservableCollection<FriendGroup>();
            Friends = new CollectionViewSource();
            Friends.IsSourceGrouped = true;
            //设置单项属性的属性名
            Friends.ItemsPath = new PropertyPath("Friends");
            Friends.Source = temp;
            //#endregion
#endif
        }
    }
}
