using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Appointment {
  public class Appointmentlist {
      public int Id { get; set; }
      public DateTime Date { get; set; }
      public string Teacher { get; set; }
      public string Topic { get; set; }
      public string StartTime { get; set; }
      public string EndTime { get; set; }

  }
}
