using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Appointment {
  public class AppointmentViewModels {
      public int Id { get; set; }
      public IDictionary<int, string> Teachers { get; set; }
      [Display(Name = "Teacher")]
      [Required]
      public int TeacherId { get; set; }

      public IList<ViewModels.Common.CourseViewModel> Course { get; set; } 
      [Display(Name = "Topic")]
      [Required]
      [Range(1,1000,ErrorMessage="Please Enter Valid Entries")]
      public int TopicId { get; set; }
      [Required]
      public DateTime Date { get; set; }
      [Required]
      [RegularExpression("([1][0-2]|[1-9]):[0-5][0-9] (AM|PM)", ErrorMessage = "Enter a Valid Time")]
      [Display(Name = "Start Time")]
      public string StartTime { get; set; }
      public IDictionary<string, string> StratTimes { get; set; }
      public IDictionary<string, string> EndTimes { get; set; }
      [Required]
      [Display(Name = "End Time")]
      [RegularExpression("([1][0-2]|[1-9]):[0-5][0-9] (AM|PM)", ErrorMessage = "Enter a Valid Time")]
      public string EndTime { get; set; }
      [Display(Name = "Student")]
      public IDictionary<int, string> Students { get; set; }
      [Required]
      [Display(Name = "Students")]
      public IList<int> StudentsId { get; set; } 
  }
}
