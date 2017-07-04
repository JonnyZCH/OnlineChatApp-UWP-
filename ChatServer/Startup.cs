using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ChatServer.Startup))]

namespace ChatServer
{
    public class Startup
    {
        /// <summary>
        /// 应用程序启动时调用该方法
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
