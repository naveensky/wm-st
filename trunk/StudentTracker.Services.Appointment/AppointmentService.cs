using System;
using System.Collections.Generic;
using System.Linq;
using StudentTracker.Models;
using StudentTracker.Repository;
using StudentTracker.Services.User;
using StudentTracker.Site.ViewModels.Student;


namespace StudentTracker.Services.Appointment {
    public class AppointmentService {

        private readonly ISqlUnitOfWork uow;
        private readonly UserService userService;
        public AppointmentService(ISqlUnitOfWork uow, UserService userService) {
            this.uow = uow;
            this.userService = userService;
        }

        public void CreateNewAppointment(Models.Appointment appointment, int studentId) {
            var student = uow.Students.FindById(studentId);
            student.Appointments.Add(appointment);
            uow.Students.SaveChanges();
        }

        public IEnumerable<AppointMentList> GetAppointmentsForStudent(int studentId) {

            var appointments = uow.Students.FindById(studentId).Appointments;
            var missedAppointments=uow.Students.FindById(studentId).Missed;
            var studentAppoinments= appointments.Select(x => new AppointMentList { Id = x.Id, Teacher = x.Teacher.Name, Topic = x.Topic.Name, StartTime = x.StartTime.ToString(), EndTime = x.EndTime.ToString(), Date = x.Date, IsPersonal = x.IsPersonal ,IsMissed = false}).ToList();
            studentAppoinments.AddRange(missedAppointments.Select(x => new AppointMentList { Id = x.Id, Teacher = x.Teacher.Name, Topic = x.Topic.Name, StartTime = x.StartTime.ToString(), EndTime = x.EndTime.ToString(), Date = x.Date, IsMissed = true }));
            return studentAppoinments;
        }

        public void DeleteAppointmentForStudent(int studentId, int appointmentId) {
            var appointment = uow.Appointments.FindById(appointmentId, x => x.Students);

            //if no appointment is found, return
            if (appointment == null)
                return;

            //if appointment is for this only student, delete the whole appointment itself
            if (appointment.Students.Count() == 1 && appointment.Students.First().Id == studentId) {
                uow.Appointments.Remove(appointment);
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
            uow.Students.SaveChanges();
        }

        public IEnumerable<Models.Appointment> GetAppointmentForTeacher(int teacherId) {

            var appointment = uow.Appointments.Fetch().ToList();

            var appointments = appointment.Where(x => x.Teacher.Id == teacherId);

            return appointments.OrderByDescending(x => x.Date);


        }



        public IEnumerable<Models.Appointment> GetAppointmentForTeacher(int teacherId, DateTime filterDate) {

            var appointments = uow.Students.Find(x => x.Appointments != null)
                                .Select(x => x.Appointments.Where(a => a.Teacher.Id == teacherId && a.Date.Date.Equals(filterDate.Date)));
            return (appointments.ToList().SelectMany(x => x));

        }

        public IDictionary<int, string> GetTopicsDictionary() {
            return uow.Topics.Fetch().ToDictionary(x => x.Id, x => x.Name);
        }


        public void SaveAppointment(Models.Appointment model, int topicId, int teacherId, IList<int> studentId, IList<int> absentStudentsId, DateTime sTime, DateTime eTime) {

            model.Teacher = uow.Teachers.FindById(teacherId);
            model.Topic = uow.Topics.FindById(topicId);

            model.Students = studentId.Select(x => uow.Students.FindById(x)).ToList();
            model.StartTime = sTime;
            model.EndTime = eTime;
            model.AbsentStudents=absentStudentsId.Select(x => uow.Students.FindById(x)).ToList();
            uow.Appointments.Add(model);
            uow.Appointments.SaveChanges();


        }

        public IEnumerable<Models.Appointment> GetAllAppointment() {
            var appointments = uow.Appointments.Fetch().ToList();
            return appointments;
        }

        public IEnumerable<IEnumerable<StudentViewModel>> GetAllAppointmentByStudents() {
            var studyCentreId = userService.GetCurrentUser().StudyCenter.Id;
            var totalStudents = uow.Students.Find(x => x.StudyCenter.Id == studyCentreId && x.Appointments.Sum(y => y.Duration) > 60 * 60).Select(x => new StudentViewModel { Name = x.Name, Id = x.Id, AppointmentCounts = (int)x.Appointments.Sum(y => y.Duration), Roll = x.Roll }).OrderByDescending(x=>x.AppointmentCounts);
            var result = new List<IEnumerable<StudentViewModel>>();
            var pStudents = new List<StudentViewModel>();
            var gStudents = new List<StudentViewModel>();
            var students = uow.Students.Find(x => x.StudyCenter.Id == studyCentreId);
            foreach (var student in students) {
                var personal = new List<Models.Appointment>();
                var grouped = new List<Models.Appointment>();
                var appointments = student.Appointments;
                foreach (var appointment in appointments) {
                    if (appointment.Students.Count > 1) {
                        grouped.Add(appointment);
                    } else {
                        personal.Add(appointment);
                    }

                }
                if (grouped.Sum(x => x.Duration) > 45 * 60) {
                    gStudents.Add(new StudentViewModel { Id = student.Id, Name = student.Name, AppointmentCounts = (int)grouped.Sum(x => x.Duration), Roll = student.Roll });
                }
                if (personal.Sum(x => x.Duration) > 15 * 60) {
                    pStudents.Add(new StudentViewModel { Id = student.Id, Name = student.Name, AppointmentCounts = (int)personal.Sum(x => x.Duration), Roll = student.Roll });
                }
            }
            result.Add(totalStudents);
            result.Add(gStudents.OrderByDescending(x=>x.AppointmentCounts));
            result.Add(pStudents.OrderByDescending(x=>x.AppointmentCounts));
            return result;

        }

    }
}
