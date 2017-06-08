using System;
using System.Collections.Generic;
using System.Text;

namespace AC.XamDemo
{
    public interface IUsersService : IDisposable
    {
        void SaveUser(User user);
        User GetUserByID(int id);
    }
}
