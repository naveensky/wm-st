using System.Collections.Generic;
using Norm;

namespace StudentTracker.Domain {
    public class Course {
        
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Course> Children { get; set; }
    }
}
