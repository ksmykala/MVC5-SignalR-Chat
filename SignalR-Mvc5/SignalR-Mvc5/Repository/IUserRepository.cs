using System.Collections.Generic;

namespace SignalR_Mvc5.Repository
{
    public interface IUserRepository
    {
        void AddUser(string name);
        string[] GetAllUsers();
    }
}