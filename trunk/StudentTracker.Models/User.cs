using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Norm;

namespace StudentTracker.Models {
    public class User  {
        [MongoIdentifier]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public StudyCenter StudyCenter { get; set; }
    }
}