using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Site.ViewModels.Common;
using StudentTracker.Site.ViewModels.Student;
using StudentTracker.Site.ViewModels.Teacher;

//using StudentTracker.Services.Core;

namespace StudentTracker.Site.ViewModels.Appointment {
    public class NewAppointmentViewModel {
        public int Id { get; set; }
        public StudentViewModel Student { get; set; }
        public int TopicId { get; set; }
        public int TeacherId { get; set; }
        [Display(Name = "Teacher")]
        public IDictionary<int, string> Teachers { get; set; }
        public int SelectedTeacherId { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        [Required]
        //[Range(0.5, 9, ErrorMessage = "A class has to be at least of 30 mints and maximum of 9 hours")]
        public string EndTime { get; set; }
        public IDictionary<int,string> Topic { get; set; }
        // public IDictionary<int, string> Topics { get; set; }
        //public int SelectedTopicId { get; set; }



        //public IDictionary<int, string> Students { get; set; }
        //public IList<int> SelectedStudents;



         public TeacherViewModel Teacher { get; set; }


    }
}
