using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using SignalR_Mvc5.Repository;

namespace SignalR_Mvc5.Hubs
{
    public class ChatHub : Hub
    {
        public static List<string>  LoggedUsers = new List<string>();

        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}