using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using StudentTracker.Models;

namespace StudentTracker.Repository.Sql {
    public class StudentTrackerDb : DbContext {

        protected  override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Student>().HasMany(x => x.Appointments).WithMany(x => x.Students).Map(x => x.ToTable("AppointmentStudents"));
            modelBuilder.Entity<Student>().HasMany(x=>x.Missed).WithMany(x=>x.AbsentStudents).Map(x=>x.ToTable("MissedAppoinments"));

        }

        public StudentTrackerDb()
            : base("studentDb") {
        }

        public IDbSet<Student> Students { get; set; }
        public IDbSet<Teacher> Teachers { get; set; }
        public IDbSet<Appointment> Appointments { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<StudyCenter> StudyCenters { get; set; }
        public IDbSet<Time> Times { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Topic> Topics { get; set; }
    }
}
