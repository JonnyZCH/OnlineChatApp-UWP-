using OnlineChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OnlineChatApp.Common
{
    public class MessageItemDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// 重写SelectTemplateCore
        /// </summary>
        /// <param name="item">消息类型</param>
        /// <param name="container"></param>
        /// <returns></returns>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is ImageMessage)
            {
                return App.Current.Resources["ImageDataTemplate"] as DataTemplate;
            }
            else if (item is Message)
            {
                if ((item as Message).IsSelf)
                {
                    return App.Current.Resources["SelfMessageDataTemplate"] as DataTemplate;
                }
                else
                {
                    return App.Current.Resources["MessageDataTemplate"] as DataTemplate;
                }
            }

            return base.SelectTemplateCore(item);
        }
    }
}
