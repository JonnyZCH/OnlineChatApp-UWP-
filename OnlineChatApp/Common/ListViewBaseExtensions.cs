using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.Extensions;

namespace OnlineChatApp.Common
{
    public static class ListViewBaseExtensions
    {
        public static void ScrollIntoViewSmoothly(this ListViewBase listViewBase, object item)
        {
            ScrollIntoViewSmoothly(listViewBase, item, ScrollIntoViewAlignment.Default);
        }

        public static void ScrollIntoViewSmoothly(this ListViewBase listViewBase, object item, ScrollIntoViewAlignment alignment)
        {
            if (listViewBase == null)
            {
                throw new ArgumentNullException(nameof(listViewBase));
            }

            // GetFirstDescendantOfType 是 WinRTXamlToolkit 中的扩展方法，
            // 寻找该控件在可视树上第一个符合类型的子元素。
            ScrollViewer scrollViewer = listViewBase.GetFirstDescendantOfType<ScrollViewer>();

            // 由于 ScrollViewer 肯定有，因此不做 null 检查判断。

            // 记录初始位置，用于 ScrollIntoView 检测目标位置后复原。
            double originHorizontalOffset = scrollViewer.HorizontalOffset;
            double originVerticalOffset = scrollViewer.VerticalOffset;

            EventHandler<object> layoutUpdatedHandler = null;
            layoutUpdatedHandler = delegate
            {
                listViewBase.LayoutUpdated -= layoutUpdatedHandler;

                // 获取目标位置。
                double targetHorizontalOffset = scrollViewer.HorizontalOffset;
                double targetVerticalOffset = scrollViewer.VerticalOffset;

                EventHandler<ScrollViewerViewChangedEventArgs> scrollHandler = null;
                scrollHandler = delegate
                {
                    scrollViewer.ViewChanged -= scrollHandler;

                    //带平滑滚动效果滚动到 item。
                    scrollViewer.ChangeView(targetHorizontalOffset, targetVerticalOffset, null);
                };
                scrollViewer.ViewChanged += scrollHandler;

                // 复原位置，且不需要使用动画效果。
                scrollViewer.ChangeView(originHorizontalOffset, originVerticalOffset, null, true);
            };
            listViewBase.LayoutUpdated += layoutUpdatedHandler;

            listViewBase.ScrollIntoView(item, alignment);
        }
    }
}
