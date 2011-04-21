using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTracker.Repository.MongoDb {
    public class ServerConfig {
        public string ServerAddress { get; set; }
        public string Database { get; set; }
        public int Port { get; set; }
    }
}
