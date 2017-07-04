using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ChatServer.Models;

namespace ChatServer.Hubs
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        public void Send(User target, User current, string message)
        {
            var sendTime = DateTime.Now;
            //给指定客户端发送消息
            Clients.Client(target.Token).Recived(target, current, message, sendTime);
            
            Clients.Caller.Recived(target, current, message, sendTime);
        }
    }
}