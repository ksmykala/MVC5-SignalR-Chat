using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalR_Mvc5.Startup))]
namespace SignalR_Mvc5
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}