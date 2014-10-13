using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR_Mvc5.Hubs
{
    [HubName("userActivityHub")]
    public class UserActivityHub : Hub
    {
        public static List<string> LoggedUsers = new List<string>();

        public void AddLoggedUser(string name)
        {
            LoggedUsers.Add(name);

            var context = GlobalHost.ConnectionManager.GetHubContext<UserActivityHub>();
            context.Clients.All.updateLoggedUsers(LoggedUsers.ToArray());
        }
    }
}