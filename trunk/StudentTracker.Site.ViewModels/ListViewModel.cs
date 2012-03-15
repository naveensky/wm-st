using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Appointment {
    public class ListViewModel {
        public IEnumerable<NewAppointmentViewModel> Appointments { get; set; }
        public Student.StudentViewModel StudentViewModel { get; set; }
    }
}
