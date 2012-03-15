using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Site.ViewModels.Common;
using StudentTracker.Site.ViewModels.Teacher;

//using StudentTracker.Services.Core;

namespace StudentTracker.Site.ViewModels.Appointment {
    public class NewAppointmentViewModel {
        
        [Display(Name = "Teacher")]
        public IDictionary<int, string> Teachers { get; set; }

        public int SelectedTeacherId { get; set; }

        public IDictionary<int, string> Topics { get; set; }
        public int SelectedTopicId { get; set; }

        public DateTime Date { get; set; }
        public Time StartTime { get; set; }

        public IDictionary<int, string> Students { get; set; }
        public IList<int> SelectedStudents;

        [Required]
        [Range(0.5, 9, ErrorMessage = "A class has to be at least of 30 mints and maximum of 9 hours")]
        public Time EndTime { get; set; }

        public TeacherViewModel Teacher { get; set; }

        public CourseViewModel Course { get; set; }
    }
}
