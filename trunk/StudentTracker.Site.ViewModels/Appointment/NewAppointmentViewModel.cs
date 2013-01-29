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
        public NewAppointmentViewModel() {
            Date = DateTime.Now;
        }
        public int Id { get; set; }
        public StudentViewModel Student { get; set; }
        [Display(Name = "Topic")]
        public int TopicId { get; set; }
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }
        [Display(Name = "Teacher")]
        public IDictionary<int, string> Teachers { get; set; }
        [Display(Name = "Teacher")]
        public int SelectedTeacherId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [RegularExpression("([1][0-2]|[1-9]):[0-5][0-9] (AM|PM)", ErrorMessage = "Enter a Valid Time")]
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }
        [Required]

        [Display(Name = "End Time")]
        [RegularExpression("([1][0-2]|[1-9]):[0-5][0-9] (AM|PM)", ErrorMessage = "Enter a Valid Time")]
        public string EndTime { get; set; }
        [Display(Name = "Topic")]
        public IDictionary<int, string> Topic { get; set; }
        public TeacherViewModel Teacher { get; set; }


    }
}
