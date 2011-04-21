using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Domain;
using StudentTracker.Service.Abstract;

namespace StudentTracker.Service.Concrete {
    public class StudentService : IStudentService {
        public void Add(Student student) {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> Students {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<Appointment> GetAppointment(ObjectId studentId) {
            throw new NotImplementedException();
        }

        public void AddAppointment(Appointment appointment, ObjectId studentId) {
            throw new NotImplementedException();
        }

        public void UpdateStudent(Student student) {
            throw new NotImplementedException();
        }
    }
}
