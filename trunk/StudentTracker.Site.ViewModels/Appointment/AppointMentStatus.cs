using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Appointment {
    public class AppointMentStatus {
        public IEnumerable<Student.StudentViewModel> GroupedStatus { get; set; }
        public IEnumerable<Student.StudentViewModel> TotalStatus { get; set; }
        public IEnumerable<Student.StudentViewModel> PersonalStatus { get; set; }

    }
}
