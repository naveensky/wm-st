using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Services.Core;

namespace StudentTracker.Site.ViewModels.Appointment {
    public class NewAppointmentViewModel {

        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }
        public IEnumerable<Models.Teacher> Teachers { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public IEnumerable<Course> Courses { get; set; }

        [Required]
        public string Date { get; set; }
        public Models.Student Student { get; set; }

        [Required]
        [Display(Name = "Appointment Time Slot")]
        public int TimeSlotId { get; set; }
        public IEnumerable<TimeSlot> TimeSlots { get; set; }

        [Required]
        [Display(Name = "Start Appointment Time")]
        [RegularExpression(@"^([0-9]|1[0-2]|0[1-9]){1}(:[0-5][0-9])(\s)?([aApP][mM])$", ErrorMessage = "Select the correct time(hh:mm am/pm)")]
        public string StartTime { get; set; }

        [Required]
        [Display(Name = "End Appointment Time")]
        [RegularExpression(@"^([0-9]|1[0-2]|0[1-9]){1}(:[0-5][0-9])(\s)?([aApP][mM])$", ErrorMessage = "Select the correct time(hh:mm am/pm)")]
        public string EndTime { get; set; }

        [Required]
        [Range(0.5, 9, ErrorMessage = "A class has to be atleast of 30 mins and maximum of 9 hours")]
        public float Duration { get; set; }

        [Display(Name = "Type of Appointment")]
        public int AppointmentTypeId { get; set; }
        public IDictionary<int, string> AppointmentType {
            get {
                return new Dictionary<int, string>
                       {
                           {1,"Individual"},
                           {2,"Group"},
                           {3,"Doubt"}

                       };
            }
        }

       
    }
}
