using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Domain;

namespace StudentTracker.Service.Abstract {
    public interface IStudentService {
        void Add(Student student);
        IEnumerable<Student> Students { get; }
        IEnumerable<Appointment> GetAppointment(ObjectId studentId);
        void AddAppointment(Appointment appointment, ObjectId studentId);
        void UpdateStudent(Student student);
    }
}
