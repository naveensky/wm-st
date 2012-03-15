using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Site.ViewModels.Student;

namespace StudentTracker.Site.ViewModels.Appointment {
    public class ListViewModel {
        public IEnumerable<AppointmentViewModel> Appointments { get; set; }
        public Student.StudentViewModel StudentViewModel { get; set; }
    }
}
