using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient;
using Autofac;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AC.XamDemo
{
    public class AutofacHelper
    {
        public static void RegisterComponents(ContainerBuilder builder)
        {
            AutofacRegistrationHelper registrationHelper = new AutofacRegistrationHelper(builder);
            IEnumerable<IEndPointConfiguration> endPoints = ReadEndPoints();

            // API Name is an arbitrary name of your choosing that AdaptiveClient uses to link interfaces (IUsersService) to the
            // EndPoints that expose them (Prod_SQL_01, Prod_WebAPI_01).  The API_Name used here must match the API_Name 
            // of related EndPoints in EndPoints.json file.
            string apiName = API_Name.UsersAPI.ToString();

            // register endPoints before registering clients
            registrationHelper.RegisterEndPoints(endPoints);

            // register clients
            registrationHelper.Register<UsersWebAPIClient, IUsersService>(EndPointType.HTTP, apiName);
            registrationHelper.Register<UsersService, IUsersService>(EndPointType.InProcess, apiName);

            // register logger (optional)
            registrationHelper.RegisterLogger(logMessage => Console.WriteLine(logMessage.Substring(0, 203)));

            builder.RegisterType<Demo>();
            builder.RegisterType<MainPageViewModel>();
        }

        public static void RegisterMocks(ContainerBuilder builder)
        {
            // this method is for mocks - see RegisterComponents for examples of how to register your components with AdaptiveClient.
            AutofacRegistrationHelper registrationHelper = new AutofacRegistrationHelper(builder);
            IEnumerable<IEndPointConfiguration> endPoints = ReadEndPoints();
            string apiName = API_Name.UsersAPI.ToString();
            registrationHelper.RegisterEndPoints(endPoints);
            registrationHelper.Register<UsersWebAPIClient, IUsersService>(EndPointType.HTTP, apiName);
            var usersServiceMock = new Mock<IUsersService>();
            usersServiceMock.Setup(x => x.SaveUser(It.IsAny<User>())).Throws(new Exception("Cant find database server."));
            usersServiceMock.Setup(x => x.GetUserByID(It.IsAny<int>())).Throws(new Exception("Cant find database server."));
            builder.RegisterInstance(usersServiceMock.Object).Keyed<IUsersService>(EndPointType.InProcess);
            registrationHelper.RegisterLogger(x => Console.WriteLine(x.Substring(0, 203)));
            builder.RegisterType<Demo>();
            builder.RegisterType<MainPageViewModel>();
        }

        private static IEnumerable<IEndPointConfiguration> ReadEndPoints()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            string json = null;

            using (Stream stream = assembly.GetManifestResourceStream("AC.XamDemo.Droid.EndPoints.json"))
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }


            if (!String.IsNullOrEmpty(json))
            {
                JObject obj = JsonConvert.DeserializeObject(json) as JObject;
                return obj["EndPointConfigurations"].ToObject<List<EndPointConfiguration>>();
            }
            return null;
        }
    }
}
