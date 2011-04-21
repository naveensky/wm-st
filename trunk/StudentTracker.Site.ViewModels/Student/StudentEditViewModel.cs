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
        public ObjectId CourseId { get; set; }
        public IEnumerable<Course> Courses { get; set; }

        [Display(Name = "Study Center")]
        public ObjectId StudyCenterId { get; set; }
        public IEnumerable<StudyCenter> StudyCenters { get; set; }

        public Models.Student Student { get; set; }
    }
}
