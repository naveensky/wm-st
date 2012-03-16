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
using StudentTracker.Services.Time;


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

        public IEnumerable<Models.AppointMentList> GetAppointmentsForStudent(int studentId) {
            var appointments = _uow.Students.Single(x => x.Id == studentId).Appointments;
            if (appointments == null)
                return null;
            else {
                return appointments.Select(x => new AppointMentList { Id = x.Id, Teacher = x.Teacher.Name, Topic = x.Topic.Name, StartTime = x.StartTime.ToString(), EndTime = x.EndTime.ToString(), Date = x.Date });
            }
            /*  IList<int> studentList = new List<int>();
              var student = _uow.Students.FindById(studentId);
              studentList.Add(student.Id);
              return appointments.Select(x => new Site.ViewModels.Appointment.NewAppointmentViewModel { SelectedTeacherId = x.Teacher.Id, Date = x.Date, StartTime = x.StartTime, EndTime = x.EndTime, SelectedStudents = studentList,Topics = x.Course.Topics.ToDictionary(y=>y.Id,z=>z.Name)});*/
        }

        public void DeleteAppointmentForStudent(int studentId, int appointmentId) {
            var appointment = _uow.Appointments.FindById(appointmentId, x => x.Students);

            //if no appointment is found, return
            if (appointment == null)
                return;

            //if appointment is for this only student, delete the whole appointment itself
            if (appointment.Students.Count() == 1 && appointment.Students.First().Id == studentId) {
                _uow.Appointments.Remove(appointment);
            } else {

                //find student
                var student = appointment.Students.SingleOrDefault(x => x.Id == studentId);

                //if no such student, return
                if (student == null)
                    return;

                //remove student 
                appointment.Students.Remove(student);
            }

            //save database changes
            _uow.Students.SaveChanges();
        }

        public IEnumerable<Models.Appointment> GetAppointmentForTeacher(int teacherId) {
            //using (var students = new MongoRepository<Student>(CoreService.GetServer())) {
            var students = _uow.Students.Fetch().ToList();
           
            var appointments = students.Where(x => x.Appointments != null).Select(x => x.Appointments.Where(y => y.Teacher.Id == teacherId)).SelectMany(x => x);
           
           return appointments;
            return null;
            //var temp = appointments.ToList().SelectMany(x => x);
            /*   return
                   temp.Select(
                       x =>
                       new Site.ViewModels.Appointment.NewAppointmentViewModel {
                           SelectedTeacherId = teacherId,
                           Topics = x.Course.Topics.ToDictionary(y => y.Id, z => z.Name),
                           Date = x.Date,
                           StartTime = x.StartTime
                       });*/
        }
        //}


        public IEnumerable<Models.Appointment> GetAppointmentForTeacher(int teacherId, DateTime filterDate) {
            //using (var students = new MongoRepository<Student>(CoreService.GetServer())) {
            var appointments = _uow.Students.Find(x => x.Appointments != null)
                                .Select(x => x.Appointments.Where(a => a.Teacher.Id == teacherId && a.Date.Date.Equals(filterDate.Date)));
            return (appointments.ToList().SelectMany(x => x));
            /* var temp = appointments.ToList().SelectMany(x => x);
             return
                 temp.Select(
                     x =>
                     new Site.ViewModels.Appointment.NewAppointmentViewModel {
                         SelectedTeacherId = teacherId,
                         Topics = x.Course.Topics.ToDictionary(y => y.Id, z => z.Name),
                         Date = x.Date,
                         StartTime = x.StartTime
                     });*/
            // }
        }

        public IDictionary<int, string> GetTopicsDictionary() {
            return _uow.Topics.Fetch().ToDictionary(x => x.Id, x => x.Name);
        }


        public void SaveAppointment(Models.Appointment model, int topicId, int teacherId, IList<int> studentId, StudentTracker.Models.Time sTime, StudentTracker.Models.Time eTime) {

            model.Teacher = _uow.Teachers.FindById(teacherId);
            model.Duration = (float)TimeService.SubtractFrom(eTime, sTime);
            model.Topic = _uow.Topics.FindById(topicId);
           // model.Students=new List<Student>();
            model.Students = studentId.Select(x => _uow.Students.FindById(x)).ToList();
            model.StartTime = sTime;
            model.EndTime = eTime;
            _uow.Appointments.Add(model);
            _uow.Appointments.SaveChanges();


        }
    }
}
