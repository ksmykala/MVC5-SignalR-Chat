using Microsoft.AspNet.SignalR;

namespace SignalR_Mvc5.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public void AddLoggedUser(string name)
        {
            Clients.All.updateLoggedUsers(name);
        }
    }
}