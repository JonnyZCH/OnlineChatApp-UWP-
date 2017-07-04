using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.ObjectModel;
using ChatServer.Models;

namespace ChatServer.Hubs
{
    [HubName("DataHub")]
    public class DataHub : Hub
    {
        public void GetFriendGroup()
        {
            #region 好友分组信息  假数据
            //var temp = new ObservableCollection<FriendGroup>
            //{
            //    new FriendGroup
            //    {
            //        GroupName = "傻逼",
            //        Friends = new ObservableCollection<User>
            //        {
            //            new User {Id=10001,Username="OFF THE Hardwell",Signature="我是傻逼英杰" },
            //            new User {Id=10002,Username="左俊杰",Signature="我是傻逼左俊杰" },
            //        }
            //    },

            //    new FriendGroup
            //    {
            //        GroupName = "三国饶舌",
            //        Friends = new ObservableCollection<User>
            //        {
            //            new User {Id=10003,Username="王司徒",Signature="扫清六合，席卷八荒!"},
            //            new User {Id=10004,Username="诸葛孔明",Signature="我从未见过如此厚颜无耻之人！"},
            //        }
            //    },

            //    new FriendGroup
            //    {
            //        GroupName = "巾帼枭雄之义海豪情",
            //        Friends = new ObservableCollection<User>
            //        {
            //            new User {Id=10005,Username="刘醒",Signature="吔屎啦梁非凡！"},
            //            new User {Id=10006,Username="梁非凡",Signature="下属不可以顶上司嘴！" },

            //        }
            //    }
            //};
            #endregion

            //好友及分组信息
            var temp = new ObservableCollection<FriendGroup>();
            var group = User.Storages.GroupBy(u => u.GroupName);
            foreach (var item in group)
            {
                temp.Add(new FriendGroup
                {
                    GroupName = item.Key,
                    Friends = new ObservableCollection<User>(item.AsEnumerable())
                });
            }

            ////看不懂的写法   select遍历
            //var temp = new ObservableCollection<FriendGroup>(User.Storages.GroupBy(u => u.GroupName).Select(g => new FriendGroup
            //{
            //    GroupName = g.Key,
            //    Friends = new ObservableCollection<User>(g.AsEnumerable())
            //}));

            Clients.Caller.GetFriendGroupCallback(temp);
        }

    }
}