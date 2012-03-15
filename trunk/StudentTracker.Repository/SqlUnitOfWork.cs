using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Models;

namespace StudentTracker.Repository {
  public  class SqlUnitOfWork : ISqlUnitOfWork {
      public SqlUnitOfWork(IRepository<Student> student, IRepository<Teacher> teachers, IRepository<Course> course, IRepository<Appointment> appointments, IRepository<StudyCenter> studyCenters, IRepository<Time> time, IRepository<TimeSlot> timeSlots, IRepository<User> users, IRepository<Topic> topics) {
            Students = student;
            Teachers = teachers;
            Courses = course;
            Appointments = appointments;
            StudyCenters = studyCenters;
            Times = time;
            TimeSlots = timeSlots;
            Users = users;
            Topics = topics;
      }

        public IRepository<Student> Students { get; set; }

        public IRepository<Teacher> Teachers {get;set; }
        

        public IRepository<Course> Courses {get;set; }
        

        public IRepository<Appointment> Appointments {get;set; }
        

        public IRepository<StudyCenter> StudyCenters {get ;set; }
        

        public IRepository<Time> Times {get ;set ; }

        public IRepository<TimeSlot> TimeSlots { get;set; }

        public IRepository<User> Users {get ;set; }

        public IRepository<Topic> Topics { get; set; }
    }
}
