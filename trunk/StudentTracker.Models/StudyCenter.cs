using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;

namespace StudentTracker.Models {
    public class StudyCenter {
        [MongoIdentifier]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
