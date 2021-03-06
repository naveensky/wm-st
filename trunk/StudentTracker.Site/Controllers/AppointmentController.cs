﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StudentTracker.Mappings;
using StudentTracker.Services.Appointment;
using StudentTracker.Services.Core;
using StudentTracker.Services.Student;
using StudentTracker.Services.Teacher;
using StudentTracker.Services.Time;
using StudentTracker.Site.ViewModels.Appointment;
using StudentTracker.Site.ViewModels.Common;


namespace StudentTracker.Site.Controllers {

    [Authorize]
    public class AppointmentController : Controller {

        private readonly StaticDataService _staticData;
        private readonly StudentService _studentService;
        private readonly AppointmentService _appService;
        private readonly TeacherService _teacherService;

        public AppointmentController(StaticDataService staticData, StudentService studentService,
                                     AppointmentService appService, TeacherService teacherService, TimeService timeService) {
            _staticData = staticData;
            _studentService = studentService;
            _appService = appService;
            _teacherService = teacherService;
        }


        public ActionResult New(int id) {
            var model = new NewAppointmentViewModel();
            model.Student = _studentService.GetStudent(id).MapToView();
            model.Teachers = _teacherService.GetTeachersDictionary();
            model.Topic = _staticData.GetTopics(id).ToDictionary(x => x.Id, y => y.Name);

            return View(model);
        }

        [HttpPost]
        public ActionResult New(NewAppointmentViewModel model) {
            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();
            char[] timeSpltiArray = { ' ', ':' };
            string[] startTimeArray = model.StartTime.Split(timeSpltiArray);
            string[] endTimeArray = model.EndTime.Split(timeSpltiArray);
            int hour = Convert.ToInt32(startTimeArray[0]);
            int minute = Convert.ToInt32(startTimeArray[1]);
            bool isAm = startTimeArray[2].Equals("AM") ? true : false;
            if (!isAm) {
                hour = hour + 12;
            }
            startTime = model.Date.AddHours(hour).AddMinutes(minute);
            hour = Convert.ToInt32(endTimeArray[0]);
            minute = Convert.ToInt32(endTimeArray[1]);
            isAm = endTimeArray[2].Equals("AM") ? true : false;
            if (!isAm) {
                hour = hour + 12;
            }
            endTime = model.Date.AddHours(hour).AddMinutes(minute);
            if (endTime.Subtract(startTime).Ticks <= 0) {
                ModelState.AddModelError("EndTime", "Start time should be less than End time");

            } else {
                List<int> temp = new List<int>();
                temp.Add(model.Student.Id);
                _appService.SaveAppointment(model.MapToDomain(), model.TopicId, model.TeacherId, temp,new List<int?>(), 
                                            startTime, endTime);

            }

            return RedirectToAction("List", new { id = model.Student.Id });
        }

        public ActionResult List(int id) {
           
            var model = new ListViewModel {
                Appointments =
                    _appService.GetAppointmentsForStudent(id).OrderByDescending(x => x.Date).Select(x => x.MapToView()),
                StudentViewModel = _studentService.GetStudent(id).MapToView()
            };

            return View(model);

        }




        public ActionResult Delete(int studentId, int appointmentId) {
            _appService.DeleteAppointmentForStudent(studentId, appointmentId);
            return RedirectToAction("List", new { id = studentId });
        }

        public ActionResult Create() {
            var model = new AppointmentViewModels();
            var temp = _staticData.GetCourses().ToList();
            var courses =
                temp.Select(
                    x =>
                    new CourseViewModel { Name = x.Name, Topics = x.Topics.Select(y => y.MapToView()) }).ToList();

            model.Course = courses;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AppointmentViewModels appointmentViewModels) {
            if (appointmentViewModels.TopicId != 0)

                return RedirectToAction("CreateAppointment", appointmentViewModels);
            else {
                return RedirectToAction("Create");
            }
        }




        public ActionResult CreateAppointment(AppointmentViewModels appointmentViewModels) {
            appointmentViewModels.Students =
                    _studentService.GetStudentsByTopic(appointmentViewModels.TopicId).ToDictionary(x => x.Id,
                                                                                                   y => y.Name);
           appointmentViewModels.AbsentStudents=appointmentViewModels.Students.ToDictionary(x => x.Key, y => y.Value);
            appointmentViewModels.Teachers = _teacherService.GetTeachersDictionary();
            var temp = _staticData.GetCourses().ToList();
            var courses =
                temp.Select(
                    x =>
                    new CourseViewModel { Name = x.Name, Topics = x.Topics.Select(y => y.MapToView()) }).ToList();
            appointmentViewModels.Course = courses;
            String[] list = {"8:00 AM",
"8:30 AM",
"9:00 AM",
"9:30 AM",
"10:00 AM",
"10:30 AM",
"11:00 AM",
"11:30 AM",
"12:00 PM",
"12:30 PM",
"1:00 PM",
"1:30 PM",
"2:00 PM",
"2:30 PM",
"3:00 PM",
"3:30 PM",
"4:00 PM",
"4:30 PM",
"5:00 PM",
"5:30 PM",
"6:00 PM",
"6:30 PM",
"7:00 PM",
"7:30 PM",
"8:00 PM"
};
            appointmentViewModels.StratTimes = list.ToDictionary(x => x, y => y);
            appointmentViewModels.EndTimes = list.ToDictionary(x => x, y => y);
            return View(appointmentViewModels);
        }

        [HttpPost]
        public ActionResult SaveAppintment(AppointmentViewModels appointmentViewModels) {
            if(appointmentViewModels.AbsentStudentsId==null)
                appointmentViewModels.AbsentStudentsId=new List<int?>();
            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();
            char[] timeSpltiArray = { ' ', ':' };
            string[] startTimeArray = appointmentViewModels.StartTime.Split(timeSpltiArray);
            string[] endTimeArray = appointmentViewModels.EndTime.Split(timeSpltiArray);

            int hour = Convert.ToInt32(startTimeArray[0]);
            int minute = Convert.ToInt32(startTimeArray[1]);
            bool isAm = startTimeArray[2].Equals("AM") ? true : false;
            if (!isAm) {
                if (hour != 12)
                    hour = hour + 12;
            }
            startTime = appointmentViewModels.Date.AddHours(hour).AddMinutes(minute);
            hour = Convert.ToInt32(endTimeArray[0]);
            minute = Convert.ToInt32(endTimeArray[1]);
            isAm = endTimeArray[2].Equals("AM") ? true : false;
            if (!isAm) {
                if (hour != 12)
                    hour = hour + 12;
            }
            endTime = appointmentViewModels.Date.AddHours(hour).AddMinutes(minute);

            if (endTime.Subtract(startTime).Ticks <= 0) {
                ModelState.AddModelError("EndTime", "Start time should be less than End time");
                return RedirectToAction("CreateAppointment", appointmentViewModels);
            } else {
                _appService.SaveAppointment(appointmentViewModels.MapToDomain(), appointmentViewModels.TopicId,
                                            appointmentViewModels.TeacherId, appointmentViewModels.StudentsId,appointmentViewModels.AbsentStudentsId,
                                            startTime, endTime);

            }
            return RedirectToAction("List", "Student");

        }

        public ActionResult GetAppointmentStatus() {
            var allAppointmentByStudents=_appService.GetAllAppointmentByStudents();
            var appointMentStatus=new AppointMentStatus();
            appointMentStatus.TotalStatus=allAppointmentByStudents.ElementAt(0);
            appointMentStatus.GroupedStatus=allAppointmentByStudents.ElementAt(1);
            appointMentStatus.PersonalStatus=allAppointmentByStudents.ElementAt(2);
            return View(appointMentStatus);
        }

        [NonAction]
        public ActionResult ListAll() {
            var Appointment = _appService.GetAllAppointment();
            IEnumerable<Appointmentlist> appointmentlist = Appointment.Select(x => new Appointmentlist { Id = x.Id, Date = x.Date, Teacher = x.Teacher.Name, Topic = x.Topic.Name, StartTime = x.StartTime.ToString(), EndTime = x.EndTime.ToString() });
            return View(appointmentlist);
        }
    }
}
