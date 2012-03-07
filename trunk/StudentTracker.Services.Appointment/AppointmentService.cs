using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository;
//using StudentTracker.Repository.MongoDb;
using StudentTracker.Repository.Sql;
using StudentTracker.Services.Core;
using StudentTracker.Site.ViewModels.Appointment;

namespace StudentTracker.Services.Appointment {
    public class AppointmentService {

        private readonly ISqlUnitOfWork _uow;

        public AppointmentService(ISqlUnitOfWork uow) {
            _uow = uow;
        }

        public void CreateNewAppointment(Models.Appointment appointment, int studentId) {
            var student = _uow.Students.FindById(studentId);
            student.Appointments.Add(appointment);
            _uow.Students.SaveChanges();
        }

        public IEnumerable<StudentTracker.Site.ViewModels.Appointment.NewAppointmentViewModel> GetAppointmentsForStudent(int studentId) {
            var appointments = _uow.Students.Single(x => x.Id == studentId).Appointments;
            if (appointments == null)
                return null;
            IList<int> studentList = new List<int>();
            var student = _uow.Students.FindById(studentId);
            studentList.Add(student.Id);
            return appointments.Select(x => new Site.ViewModels.Appointment.NewAppointmentViewModel { SelectedTeacherId = x.Teacher.Id, Date = x.Date, StartTime = x.StartTime, EndTime = x.EndTime, SelectedStudents = studentList,Topics = x.Course.Topics.ToDictionary(y=>y.Id,z=>z.Name)});
        }

        public void DeleteAppointmentForStudent(int studentId, int appointmentId) {
            // using (var repo = new MongoRepository<Student>(CoreService.GetServer())) {
            var student = _uow.Students.Single(x => x.Id == studentId);
            if (student.Appointments != null) {
                var appointment = student.Appointments.Single(x => x.Id == appointmentId);
                student.Appointments.Remove(appointment);
                _uow.Students.Add(student);
                _uow.Students.SaveChanges();
            }
        }
        // }

        public IEnumerable<Site.ViewModels.Appointment.NewAppointmentViewModel> GetAppointmentForTeacher(int teacherId) {
            //using (var students = new MongoRepository<Student>(CoreService.GetServer())) {
            var appointments = _uow.Students.Find(x => x.Appointments != null)
                                        .Select(x => x.Appointments.Where(y => y.Teacher.Id == teacherId));
            var temp = appointments.ToList().SelectMany(x => x);
            return
                temp.Select(
                    x =>
                    new Site.ViewModels.Appointment.NewAppointmentViewModel {
                        SelectedTeacherId = teacherId,
                        Topics = x.Course.Topics.ToDictionary(y => y.Id, z => z.Name),
                        Date = x.Date,
                        StartTime = x.StartTime
                    });
        }
        //}


        public IEnumerable<Site.ViewModels.Appointment.NewAppointmentViewModel> GetAppointmentForTeacher(int teacherId, DateTime filterDate) {
            //using (var students = new MongoRepository<Student>(CoreService.GetServer())) {
            var appointments = _uow.Students.Find(x => x.Appointments != null)
                                .Select(x => x.Appointments.Where(a => a.Teacher.Id == teacherId && a.Date.Date.Equals(filterDate.Date)));

            var temp = appointments.ToList().SelectMany(x => x);
            return
                temp.Select(
                    x =>
                    new Site.ViewModels.Appointment.NewAppointmentViewModel {
                        SelectedTeacherId = teacherId,
                        Topics = x.Course.Topics.ToDictionary(y => y.Id, z => z.Name),
                        Date = x.Date,
                        StartTime = x.StartTime
                    });
            // }
        }

        public IDictionary<int, string> GetTopicsDictionary() {
            return _uow.Topics.Fetch().ToDictionary(x => x.Id, x => x.Name);
        }


        public void SaveAppointment(NewAppointmentViewModel model) {
            Models.Appointment appointment = new Models.Appointment();
            appointment.StartTime = model.StartTime;
            appointment.EndTime = model.EndTime;
            appointment.Teacher.Id = model.SelectedTeacherId;
            appointment.Date = model.Date;
            appointment.Duration = (float)model.EndTime.SubtractFrom(model.StartTime);
            appointment.Course = _uow.Topics.FindById(model.SelectedTopicId).course;
            _uow.Appointments.Add(appointment);
            _uow.Appointments.SaveChanges();


        }
    }
}
