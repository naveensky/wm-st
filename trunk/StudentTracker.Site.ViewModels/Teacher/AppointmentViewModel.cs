using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentTracker.Models;

namespace StudentTracker.Site.ViewModels.Teacher {
    public class AppointmentViewModel {
        public DateTime? FilterDate { get; set; }
        public IEnumerable<Models.Appointment> Appointments { get; set; }
        public Models.Teacher Teacher { get; set; }
    }
}