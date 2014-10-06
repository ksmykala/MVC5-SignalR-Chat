using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalR_Mvc4.Infrastructure;

namespace SignalR_Mvc4.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            var name = Context.User.Identity.Name;
            Clients.All.addNewMessage(name, message);
        }

        public void GetClientsCount()
        {
            Clients.All.showClientsCount(UserHandler.ConnectedUsers.Count);
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

            Clients.All.showLoggedUsers(UserHandler.ConnectedUsers.OrderBy(x => x.UserName).Select(x => x.UserName).ToArray());

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            UserHandler.ConnectedUsers.RemoveWhere(x => x.ConnectionId == Context.ConnectionId);
            Clients.All.showClientsCount(UserHandler.ConnectedUsers.Count);

            var message = string.Format("{0} has left", Context.User.Identity.Name);
            Clients.All.addNewMessage("Server", message, "danger");

            Clients.All.showLoggedUsers(UserHandler.ConnectedUsers.Select(x => x.UserName).ToArray());

            return base.OnDisconnected(stopCalled);
        }
    }
}