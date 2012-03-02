using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository;
//using StudentTracker.Repository.MongoDb;
using StudentTracker.Repository.Sql;
using StudentTracker.Services.Core;

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

        public IEnumerable<Models.Appointment> GetAppointmentsForStudent(int studentId) {
            var appointments = _uow.Students.Single(x => x.Id == studentId).Appointments;
            if (appointments == null)
                return null;

            return appointments.ToList();
        }

        public void DeleteAppointmentForStudent(int studentId, int appointmentId) {
            // using (var repo = new MongoRepository<Student>(CoreService.GetServer())) {
            var student = _uow.Students.Single(x => x.Id == studentId);
            if (student.Appointments != null) {
                var appointment = student.Appointments.Single(x => x.Id == appointmentId);
                student.Appointments.Remove(appointment);
                _uow.Students.Add(student);
            }
        }
   // }

    public IEnumerable<Models.Appointment> GetAppointmentForTeacher(int teacherId) {
            //using (var students = new MongoRepository<Student>(CoreService.GetServer())) {
                var appointments = _uow.Students.Find(x=>x.Appointments!=null)
                                            .Select(x => x.Appointments.Where(y => y.Teacher.Id == teacherId));
                return appointments.ToList().SelectMany(x => x);
            }
        //}


        public IEnumerable<Models.Appointment> GetAppointmentForTeacher(int teacherId, DateTime filterDate) {
            //using (var students = new MongoRepository<Student>(CoreService.GetServer())) {
                var appointments = _uow.Students.Find(x => x.Appointments != null)
                                    .Select(x => x.Appointments.Where(a => a.Teacher.Id == teacherId && a.Date.Date.Equals(filterDate.Date)));

                return appointments.ToList().SelectMany(x => x);
           // }
        }


    }
}
