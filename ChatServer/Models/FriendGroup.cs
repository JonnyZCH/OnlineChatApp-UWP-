using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ChatServer.Models
{
    public class FriendGroup
    {
        public string GroupName { get; set; }
        public ObservableCollection<User> Friends { get; set; }
    }
}