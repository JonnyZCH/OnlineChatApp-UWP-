using OnlineChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChatApp.Models
{
    public class FriendGroup
    {
        public string GroupName { get; set; }
        public ObservableCollection<User> Friends { get; set; }
    }
}
