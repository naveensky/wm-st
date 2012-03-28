using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Norm;
using StudentTracker.Mappings;
//using StudentTracker.Services.Appointment;
using StudentTracker.Services.Appointment;
using StudentTracker.Services.Core;
using StudentTracker.Services.Teacher;
using StudentTracker.Services.Time;
using StudentTracker.Site.ViewModels;
using StudentTracker.Site.ViewModels.Appointment;
using StudentTracker.Site.ViewModels.Teacher;

namespace StudentTracker.Site.Controllers {

    [Authorize]
    public class TeacherController : Controller {

        private TeacherService _teacherSvc;
        private StaticDataService _staticDataSvc;
        private AppointmentService _appSvc;
        private TimeService _timeService;
        public TeacherController(TeacherService teacherSvc, StaticDataService staticDataSvc, AppointmentService appSvc,TimeService timeService) {
            _teacherSvc = teacherSvc;
            _staticDataSvc = staticDataSvc;
            _appSvc = appSvc;
            _timeService = timeService;
        }


        public ActionResult List() {
            var model = new TeacherListViewModel { Teachers = _staticDataSvc.GetTeachers().Select(x=>x.MapToView())};
            return View(model);
        }

        public ActionResult Appointments(int id) {
            var model = new StudentTracker.Site.ViewModels.Teacher.AppointmentViewModel {
                Appointments = _appSvc.GetAppointmentForTeacher(id).Select(x=> new TeacherAppointModel{Date=x.Date,StartTime=x.StartTime.ToString(),EndTime=x.EndTime.ToString(),Topic=x.Topic.Name}),
                Teacher = _staticDataSvc.GetTeacher(id).MapToView(),
                FilterDate = DateTime.Now
            };

            return View(model);
        }
        
        [HttpPost]
        public ActionResult Appointments(AppointmentViewModel viewModel) {
            var model = new AppointmentViewModel {
                Appointments = viewModel.FilterDate == null ?
                        _appSvc.GetAppointmentForTeacher(viewModel.Teacher.Id).Select(x=> new TeacherAppointModel{Date=x.Date,StartTime=x.StartTime.ToString(),EndTime=x.EndTime.ToString(),Topic=x.Topic.Name}) :
                        _appSvc.GetAppointmentForTeacher(viewModel.Teacher.Id).Select(x=> new TeacherAppointModel{Date=x.Date,StartTime=x.StartTime.ToString(),EndTime=x.EndTime.ToString(),Topic=x.Topic.Name}).Where(x=>x.Date==viewModel.FilterDate),
                FilterDate = viewModel.FilterDate,
                Teacher = _staticDataSvc.GetTeacher(viewModel.Teacher.Id).MapToView()
            };
            return View(model);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TeacherCreateViewModel model) {
            _teacherSvc.AddTeacher(model.Teacher.MapToDomain());
           // _teacherSvc.AddTeacher(model.GetTeacher());
            return RedirectToAction("List", "Teacher");
        }


    }
}
