using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;

namespace StudentTracker.Models {
    public class Course {
        
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Course> Children { get; set; }
    }
}
