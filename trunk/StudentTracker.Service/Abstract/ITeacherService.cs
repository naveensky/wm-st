using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Domain;

namespace StudentTracker.Service.Abstract {
    public interface ITeacherService {
        void AddTeacher(Teacher teacher);
        IEnumerable<Teacher> Teachers { get; }
        IEnumerable<Appointment> GetAppointments(ObjectId teacherId);
    }
}
