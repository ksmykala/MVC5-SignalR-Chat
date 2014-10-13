using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalR_Mvc4.Startup))]
namespace SignalR_Mvc4
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}