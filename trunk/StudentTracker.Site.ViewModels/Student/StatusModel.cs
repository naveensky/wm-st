using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Student {
  public class StatusModel {
      public IEnumerable<ValueModel> StudentStatus { get; set; } 
     
    }
      public class ValueModel {
          public DateTime LastAppointment { get; set; }
          public StudentViewModel Student { get; set; }
          public int TotalDays{get {
              DateTime today = DateTime.Now;
              TimeSpan timePeriod;
              if (LastAppointment.Year != 1) {
                  DateTime lastAppointment = LastAppointment;
                   timePeriod = today - lastAppointment;
              }
              else {
                  DateTime registerDate = Student.RegisterDate;
                  timePeriod = today - registerDate;
              }
              return  timePeriod.Days;
          }
              
          }
      }
}
