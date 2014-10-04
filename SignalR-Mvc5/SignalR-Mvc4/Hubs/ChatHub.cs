using Microsoft.AspNet.SignalR;

namespace SignalR_Mvc4.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            var name = Context.User.Identity.Name;
            Clients.All.addNewMessage(name, message);
        }
    }
}