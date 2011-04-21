using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Appointment {
    public class ListViewModel {
        public IEnumerable<Models.Appointment> Appointments { get; set; }
        public Models.Student Student { get; set; }
    }
}
