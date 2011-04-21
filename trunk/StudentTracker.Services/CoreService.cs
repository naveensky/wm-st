using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository.MongoDb;

namespace StudentTracker.Services.Core {
    public class CoreService {
        private const string _serverKey = "Server";
        private const string _portKey = "Port";
        private const string _dbKey = "Database";

        private const string _tempPath = "/Tmp/";

        public static ServerConfig GetServer() {
            var config = new ServerConfig {
                ServerAddress = System.Configuration.ConfigurationManager.AppSettings[_serverKey],
                Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[_portKey]),
                Database = System.Configuration.ConfigurationManager.AppSettings[_dbKey]
            };
            return config;
        }

        public static string GetTempPath(string extension) {
            return string.Format("{0}{1}.{2}", _tempPath, Guid.NewGuid(), extension);
        }

        public static string ConvertToAbsolute(string relativePath) {
            return HttpContext.Current.Server.MapPath(relativePath);
        }

        public static User GetCurrentUser() {
          var userId= (ObjectId)(Membership.GetUser().ProviderUserKey.ToString());
            using(var userRepo=new MongoRepository<User>(CoreService.GetServer())) {
                var user=userRepo.Collection.Single(x => x.Id==userId);
                return user;
            }
        }

    }
}
