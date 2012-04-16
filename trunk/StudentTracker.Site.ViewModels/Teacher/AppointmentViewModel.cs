using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentTracker.Models;

namespace StudentTracker.Site.ViewModels.Teacher {
    public class AppointmentViewModel {
        
        public TeacherViewModel Teacher { get; set; }
        public DateTime FilterDate { get; set; }
        public IEnumerable<ViewModels.Appointment.TeacherAppointModel> Appointments{
            get;
            set;
        } 
    }
}