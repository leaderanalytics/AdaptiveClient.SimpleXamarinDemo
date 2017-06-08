using System;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient;

namespace AC.XamDemo
{
    public class Demo
    {
        private IAdaptiveClient<IUsersService> adaptiveClient;
        private API_Result apiResult;

        public Demo(IAdaptiveClient<IUsersService> adaptiveClient)
        {
            this.adaptiveClient = adaptiveClient;
            this.apiResult = new API_Result();
        }

        public API_Result Simulate_API_Calls()
        {
            // In this demo we make some service calls and populate
            // the apiResult object with info about what Endpoint was used.
            // Ideally we have access to a SQL server on the LAN but if 
            // that fails we fall back to a WebAPI server:

            int id = 1;
            User user = GetUser(id);
            SaveUser(user);
            return apiResult;
        }

        private User GetUser(int id)
        {
            User user = adaptiveClient.Try(x => x.GetUserByID(id));
            apiResult.GetUserResult = $"User {user.Name} was found.  EndPoint used was {adaptiveClient.CurrentEndPoint.Name}.";
            return user;
        }

        private void SaveUser(User user)
        {
            adaptiveClient.Try(x => x.SaveUser(user));
            apiResult.SaveUserResult = $"User {user.Name} was saved.  EndPoint used was {adaptiveClient.CurrentEndPoint.Name}.";
        }
    }
}
