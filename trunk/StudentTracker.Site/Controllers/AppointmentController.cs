using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Norm;
using StudentTracker.Mappings;
using StudentTracker.Models;
using StudentTracker.Services.Appointment;
using StudentTracker.Services.Core;
using StudentTracker.Services.Student;
using StudentTracker.Services.Teacher;
using StudentTracker.Services.Time;
using StudentTracker.Site.ViewModels.Appointment;
using StudentTracker.Site.ViewModels.Time;

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


        public ActionResult New(int Id) {
            var model = new NewAppointmentViewModel();
            model.Student = _studentService.GetStudent(Id).MapToView();
            model.Teachers = _teacherService.GetTeachersDictionary();
            model.Topic = _staticData.GetTopics(Id).ToDictionary(x=>x.Id,y=>y.Name);
            //model.Topics = _appService.GetTopicsDictionary();
            //model.TimeSlots = _staticData.GetTimeSlots();
            return View(model);
        }

        [HttpPost]
        public void New(NewAppointmentViewModel model) {
            TimeViewModel startTime = null;
            TimeViewModel endTime = null;
            char[] timeSpltiArray= {' ',':'};
            string[] startTimeArray = model.StartTime.Split(timeSpltiArray);
            string[] endTimeArray = model.EndTime.Split(timeSpltiArray);
            startTime = new TimeViewModel { Hour = Convert.ToInt32(startTimeArray[0]), Minute = Convert.ToInt32(startTimeArray[1]),IsAm = startTimeArray[2].Equals("AM")?true:false};
            endTime = new TimeViewModel { Hour = Convert.ToInt32(endTimeArray[0]), Minute = Convert.ToInt32(endTimeArray[1]), IsAm = endTimeArray[2].Equals("AM") ? true : false };
            if (TimeService.CompareTo(endTime.MapToDomain(),startTime.MapToDomain()) <= 0) {
                ModelState.AddModelError("EndTime", "Start time should be less than End time");
                // model.Student = _studentService.GetStudent(model.Student.Id);
                //model.Teachers = _staticData.GetTeachers();
                //model.Courses = _staticData.GetStudentChildCourse(model.Student.Id);
                //model.TimeSlots = _staticData.GetTimeSlots();
                //model.Date = string.Format("{0:dd MMM yyy}", DateTime.Now);
                //model.AppointmentTypeId = 1;
            } else {
                _appService.SaveAppointment(model.MapToDomain(),model.TopicId,model.TeacherId,model.Student.Id,startTime.MapToDomain(),endTime.MapToDomain());
                //return View(model);
            }
            //_appService.CreateNewAppointment(GetAppointment(), model.Student.Id);

        }
        
       public ActionResult List(int id) {
            var model = new ListViewModel {
                Appointments = _appService.GetAppointmentsForStudent(id).Select(x=>x.MapToView()),
                StudentViewModel = _studentService.GetStudent(id).MapToView()
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
