using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Domain;
using StudentTracker.Service.Abstract;

namespace StudentTracker.Service.Concrete {
    public class TeacherService : ITeacherService {
        public void AddTeacher(Teacher teacher) {
            throw new NotImplementedException();
        }

        public IEnumerable<Teacher> Teachers {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<Appointment> GetAppointments(ObjectId teacherId) {
            throw new NotImplementedException();
        }
    }
}