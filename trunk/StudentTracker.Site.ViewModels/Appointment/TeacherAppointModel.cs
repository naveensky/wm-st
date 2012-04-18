using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Site.ViewModels.Student;

namespace StudentTracker.Site.ViewModels.Appointment {
    public class TeacherAppointModel {
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Topic { get; set; }
        public bool IsPersonal { get; set; }
        public TimeSpan Duration {
            get { return EndTime.Subtract(StartTime); }
        }
    }
}
