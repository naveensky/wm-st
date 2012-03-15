using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Site.ViewModels.Common;

namespace StudentTracker.Site.ViewModels.Student {
    public class StudentEditViewModel {

        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public IDictionary<int,string> Courses { get; set; }

        [Display(Name = "Study Center")]
        public int StudyCenterId { get; set; }
        public IDictionary<int,string> StudyCenters { get; set; }

        public StudentViewModel StudentViewModel { get; set; }
    }
}
