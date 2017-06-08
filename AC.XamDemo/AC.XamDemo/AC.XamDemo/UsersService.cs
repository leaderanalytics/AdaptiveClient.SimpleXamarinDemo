﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AC.XamDemo
{
    class UsersService : IUsersService
    {
        public void SaveUser(User user)
        {
            // call database client here and insert....
        }

        public User GetUserByID(int id)
        {
            // call database client here and select...
            return new User { ID = id, Name = "Bob" };
        }

        #region IDisposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
