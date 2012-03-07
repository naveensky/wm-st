using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;

namespace StudentTracker.Site.ViewModels.Student {
    public class StudentEditViewModel {

        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public IEnumerable<Course.Course> Courses { get; set; }

        [Display(Name = "Study Center")]
        public int StudyCenterId { get; set; }
        public IEnumerable<Site.ViewModels.StudyCenter.StudyCenter> StudyCenters { get; set; }

        public Site.ViewModels.Student.Student Student { get; set; }
    }
}
