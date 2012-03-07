using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;

namespace StudentTracker.Models {
    public class Appointment : IEntity {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        [RegularExpression("^([1-9]|1[0-2]|0[1-9]){1}(:[0-5][0-9][aApP][mM]){1}$", ErrorMessage = "Select the correct time(hh:mm am/pm)")]
        public Time StartTime { get; set; }
        [RegularExpression("^([1-9]|1[0-2]|0[1-9]){1}(:[0-5][0-9][aApP][mM]){1}$", ErrorMessage = "Select the correct time(hh:mm am/pm)")]
        public Time EndTime { get; set; }
        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
        public float Duration { get; set; }
        public virtual TimeSlot Timeslot { get; set; }
        public virtual AppointmentType AppointmentType { get; set; }
    }
}