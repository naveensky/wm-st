﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Norm;
using StudentTracker.Models;
using StudentTracker.Services.Appointment;
using StudentTracker.Services.Core;
using StudentTracker.Services.Student;
using StudentTracker.Services.Teacher;
using StudentTracker.Site.ViewModels.Appointment;

namespace StudentTracker.Site.Controllers {

    [Authorize]
    public class AppointmentController : Controller {

        private StaticDataService _staticData;
        private StudentService _studentService;
        private AppointmentService _appService;
        private TeacherService _teacherService;
        public AppointmentController(StaticDataService staticData, StudentService studentService, AppointmentService appService, TeacherService teacherService) {
            _staticData = staticData;
            _studentService = studentService;
            _appService = appService;
            _teacherService = teacherService;
        }


        public ActionResult New() {
            var model = new NewAppointmentViewModel();
            model.Students = _studentService.GetStudentsDictionary();
            model.Teachers = _teacherService.GetTeachersDictionary();
            model.Topics = _appService.GetTopicsDictionary();
            //model.TimeSlots = _staticData.GetTimeSlots();
            return View(model);
        }

        [HttpPost]
        public void New(NewAppointmentViewModel model) {
            Time startTime = model.StartTime;
            Time endTime = model.EndTime;
            if (startTime.CompareTo(endTime) <= 0) {
                ModelState.AddModelError("EndTime", "Start time should be less than End time");
                // model.Student = _studentService.GetStudent(model.Student.Id);
                //model.Teachers = _staticData.GetTeachers();
                //model.Courses = _staticData.GetStudentChildCourse(model.Student.Id);
                //model.TimeSlots = _staticData.GetTimeSlots();
                //model.Date = string.Format("{0:dd MMM yyy}", DateTime.Now);
                //model.AppointmentTypeId = 1;
            } else {
                _appService.SaveAppointment(model);
                //return View(model);
            }
            //_appService.CreateNewAppointment(GetAppointment(), model.Student.Id);

        }

        public ActionResult List(int id) {
            var model = new ListViewModel {
                Appointments = _appService.GetAppointmentsForStudent(id),
                Student = _studentService.GetStudent(id)
            };
            return View(model);
        }

        /*       public Models.Appointment GetAppointment() {
            
                   var appointment = new Models.Appointment {
                       Course = _staticData.GetStudentChildCourse(Student.Id).Single(x => x.Id == CourseId),
                       Date = DateTime.Parse(Date),
                       StartTime = new Time(StartTime),
                       EndTime = new Time(EndTime),
                       //Timeslot = staticService.GetTimeSlots().Single(x => x.Id == TimeSlotId),
                       //Duration = Duration,
                       AppointmentType = (AppointmentType)AppointmentTypeId,
                       Teacher = staticService.GetTeachers().Single(x => x.Id == TeacherId)
                   };
                   return appointment;
               }*/


        public ActionResult Delete(int studentId, int appointmentId) {
            _appService.DeleteAppointmentForStudent(studentId, appointmentId);
            return RedirectToAction("List", new { id = studentId });
        }

    }
}
