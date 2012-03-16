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
      public int TeacherId { get; set; }
      public IDictionary<int, string> Topics { get; set; }
      [Display(Name = "Topic")]
      public int TopicId { get; set; }
      public DateTime Date { get; set; }
      public string StartTime { get; set; }
      public string EndTime { get; set; }
      [Display(Name = "Student")]
      public IDictionary<int, string> Students { get; set; }
      public IList<int> StudentsId { get; set; } 
  }
}
