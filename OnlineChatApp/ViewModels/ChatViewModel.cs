using OnlineChatApp.Common;
using OnlineChatApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChatApp.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        public ChatViewModel()
        {
            SendCommand = new Command(p =>
            {
                var model = p as ChatViewModel;
                if (model == null)
                    return;
                //客户端调用服务端函数，发送数据
                App.Current.ChatHubProxy.Invoke("Send", model.Target, App.Current.CurrentUser, model.Input);
                System.Diagnostics.Debug.WriteLine(model.Input);
                model.Input = null;
            }, p =>
            {
                var model = p as ChatViewModel;
                return model != null && !string.IsNullOrEmpty(model.Input);
            });

            Messages = new ObservableCollection<MessageBase>();
        }

        public User Target { get; set; }
        public ObservableCollection<MessageBase> Messages { get; set; }
        public Command SendCommand { get; set; }

        private string input;
        public string Input
        {
            get { return input; }
            set
            {
                SetProperty(ref input, value);
                SendCommand.OnCanExecuteChanged();
            }
        }
    }
}
