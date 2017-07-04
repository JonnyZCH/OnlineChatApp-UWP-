using OnlineChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using static OnlineChatApp.Models.User;

namespace OnlineChatApp.Common
{
    public class OnlineOrNotValueConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var status = (UserStatus)value;
            switch (status)
            {
                case UserStatus.Online:
                    return 1;
                case UserStatus.Offline:
                    return 0.3;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
