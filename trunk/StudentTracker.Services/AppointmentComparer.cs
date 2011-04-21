using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Models;

namespace StudentTracker.Services.Core {
    public class AppointmentComparer : EqualityComparer<Models.Appointment> {
        public override bool Equals(Appointment x, Appointment y) {
            return x.AppointmentType == y.AppointmentType
                 && x.Date.Date == y.Date.Date
                 && x.StartTime.IsAm == y.StartTime.IsAm
                 && x.StartTime.Hour == y.StartTime.Hour
                 && x.StartTime.Minute == y.StartTime.Minute
                 && x.Course.Id == y.Course.Id;
        }

        public override int GetHashCode(Appointment obj) {
            return 0;
        }
    }
}
