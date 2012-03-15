using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;

namespace StudentTracker.Models {
    public class Course : IEntity {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Topic> Topics { get; set; }
    }
}
