using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Norm;

namespace StudentTracker.Site.ViewModels.Student {
    public class StudentListViewModel {

        [Display(Name = "Search by Roll No.")]
        public string RollNoSearchText { get; set; }

        [Display(Name = "Search By Student Name")]
        public string StudentNameSearchText { get; set; }

        [Display(Name = "Filter by Course")]
        public int CourseId { get; set; }
        public IEnumerable<Course.Course> Courses { get; set; }

        public bool IsSearchEmpty {
            get {
                return string.IsNullOrEmpty(RollNoSearchText)
                    && string.IsNullOrEmpty(StudentNameSearchText)
                    && CourseId == null;
            }
        }

        public IEnumerable<Student> Students { get; set; }
    }
}