using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_Mvc4.Infrastructure
{
    public static class UserHandler
    {
        public static HashSet<User> ConnectedUsers = new HashSet<User>();
    }
}