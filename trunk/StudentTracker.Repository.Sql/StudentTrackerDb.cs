using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using StudentTracker.Models;

namespace StudentTracker.Repository.Sql {
    public class StudentTrackerDb : DbContext {

        public StudentTrackerDb()
            : base("studentDb") {
        }

        public IDbSet<Student> Students { get; set; }
        public IDbSet<Teacher> Teachers { get; set; }
        public IDbSet<Appointment> Appointments { get; set; }
    }
}
