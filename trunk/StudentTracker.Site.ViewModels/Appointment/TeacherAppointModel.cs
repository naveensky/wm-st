using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Site.ViewModels.Student;

namespace StudentTracker.Site.ViewModels.Appointment {
  public class TeacherAppointModel {
      public DateTime Date { get; set; }
      public string StartTime { get; set; }
      public string EndTime { get; set; }
      public string Topic { get; set; }
      
    }
}
