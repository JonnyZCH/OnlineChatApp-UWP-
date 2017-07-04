using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading;
using ChatServer.Models;

namespace ChatServer.Hubs
{
    [HubName("AccountHub")]
    public class AccountHub : Hub
    {
        public void Login(string username, string password, string hardwareInfo, string networkType)
        {
            //启动时有1秒延迟
            Thread.Sleep(1000);

            var user = User.Storages.FirstOrDefault(u => u.Username.Equals(username));
            if (user == null || user.Password != password)
            {
                Clients.Caller.LoginFailedCallback("登录失败！");
                return;
            }
            //登录成功将用户状态修改为在线
            user.Status = UserStatus.Online;

            //将用户的ConnectionId保存
            user.Token = Context.ConnectionId;

            //将用户所登录的设备信息保存
            user.HardwareInfo = hardwareInfo;

            //将用户所处的网络环境类型保存
            user.NetworkType = networkType;

            //登录成功回执
            Clients.Caller.LoginSuccessCallback(user);

            //通知其他客户端新用户上线
            Clients.Others.NewFriendOnline(user);
        }
    }
}
