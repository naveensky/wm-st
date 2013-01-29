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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Student> AbsentStudents { get; set; }
            [NotMapped]
        public bool IsPersonal {
            get {
                int count = Students.Count;
                if (count == 1) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        [NotMapped]
        public double Duration {
            get { return EndTime.Subtract(StartTime).TotalMinutes; }
        }
    }
}