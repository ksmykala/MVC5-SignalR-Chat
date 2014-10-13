using Microsoft.AspNet.SignalR;
using SignalR_Mvc4.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Mvc4.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string message, string recipientId)
        {
            var name = Context.User.Identity.Name;

            if (recipientId == null)
                Clients.All.addNewMessage(name, message);
            else
            {
                Clients.Client(recipientId).addPrivateMessage(name, message, Context.ConnectionId);
                Clients.Client(Context.ConnectionId).addPrivateMessage(name, message, recipientId);
            }
        }

        public void GetClientsCount()
        {
            var count = UserHandler.ConnectedUsers.Select(x => x.UserName).Distinct().Count();
            Clients.All.showClientsCount(count);
        }

        public override Task OnConnected()
        {
            UserHandler.ConnectedUsers.Add(new User
            {
                ConnectionId = Context.ConnectionId,
                UserName = Context.User.Identity.Name
            });

            var message = string.Format("{0} has joined", Context.User.Identity.Name);
            Clients.AllExcept(Context.ConnectionId).addNewMessage("Server", message, "success");
            
            Clients.All.showLoggedUsers(UserHandler.ConnectedUsers.OrderBy(x => x.UserName).Select(x => x.ConnectionId).ToArray());

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            UserHandler.ConnectedUsers.RemoveWhere(x => x.ConnectionId == Context.ConnectionId);
            Clients.All.showClientsCount(UserHandler.ConnectedUsers.Count);

            var message = string.Format("{0} has left", Context.User.Identity.Name);
            Clients.All.addNewMessage("Server", message, "danger");

            Clients.All.showLoggedUsers(UserHandler.ConnectedUsers.OrderBy(x => x.UserName).Select(x => x.ConnectionId).ToArray());

            return base.OnDisconnected(stopCalled);
        }
    }
}