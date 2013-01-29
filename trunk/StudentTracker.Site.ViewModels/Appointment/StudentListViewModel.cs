using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Site.ViewModels.Student;

namespace StudentTracker.Site.ViewModels.Appointment {
    public class ListViewModel {
        public IEnumerable<AppointmentViewModel> Appointments { get; set; }
        public StudentViewModel StudentViewModel { get; set; }
        public TimeSpan GroupedDurations {
            get {
                var totalAppontMents = Appointments.Where(x => x.IsPersonal.HasValue&&!x.IsPersonal.Value);

                var totalTime = new TimeSpan(0, 0, 0, 0, 0);
                totalTime = totalAppontMents.Aggregate(totalTime, (current, appointmentViewModel) => current.Add(appointmentViewModel.Duration));
                return totalTime;
            }
        }

        public TimeSpan PersonalDurations {
            get {
                var totalAppontMents = Appointments.Where(x => x.IsPersonal.HasValue && x.IsPersonal.Value);

                var totalTime = new TimeSpan(0, 0, 0, 0, 0);
                totalTime = totalAppontMents.Aggregate(totalTime, (current, appointmentViewModel) => current.Add(appointmentViewModel.Duration));
                return totalTime;
            }
        }
        public TimeSpan MissedDurations {
            get {
                var totalAppontMents = Appointments.Where(x => x.IsMissed);

                var totalTime = new TimeSpan(0, 0, 0, 0, 0);
                totalTime = totalAppontMents.Aggregate(totalTime, (current, appointmentViewModel) => current.Add(appointmentViewModel.Duration));
                return totalTime;
            }
        }

        public TimeSpan TotalDurations {
            get {
                

                var totalTime = new TimeSpan(0, 0, 0, 0, 0);
                totalTime = Appointments.Aggregate(totalTime, (current, appointmentViewModel) => current.Add(appointmentViewModel.Duration));
                return totalTime;
            }
        }
        
    }
}
