using System.Collections.Generic;

namespace SignalR_Mvc5.Repository
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<string> LoggedUsers = new List<string>();

        public void AddUser(string name)
        {
            LoggedUsers.Add(name);
        }

        public string[] GetAllUsers()
        {
            return LoggedUsers.ToArray();
        }
    }
}