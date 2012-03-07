using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Models;

namespace StudentTracker.Repository {
    public interface ISqlUnitOfWork {
        IRepository<Student> Students { get;  set; }
        IRepository<Teacher> Teachers { get; set; }
        IRepository<Course> Courses { get; set; }
        IRepository<Appointment> Appointments { get; set; }
        IRepository<StudyCenter> StudyCenters { get; set; }
        IRepository<Time> Times { get; set; }
        IRepository<TimeSlot> TimeSlots { get; set; }
        IRepository<User> Users { get; set; }
        IRepository<Topic> Topics { get; set; }


    }
}
