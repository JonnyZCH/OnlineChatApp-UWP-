using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OnlineChatApp.Controls
{
    /// <summary>
    /// 自定义MessageListView控件
    /// </summary>
    public sealed class MessageListView : ListView
    {
        public MessageListView()
        {
            //指定样式
            this.DefaultStyleKey = typeof(ListView);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

    }

    //ListViewItem选择器
    public class MessageListViewTemplateSelector : DataTemplateSelector
    {
        public MessageListViewTemplateSelector()
        {

        }

        public DataTemplate OtherMessageTemplate { get; set; }
        public DataTemplate MyMessageTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var i = (int)item;
            //TODO  消息来源判断
            if (i % 2 == 0)
            {
                return OtherMessageTemplate;
            }
            else
            {
                return MyMessageTemplate;
            }
        }
    }
}
