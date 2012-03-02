using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Norm;
using StudentTracker.Services.Appointment;
using StudentTracker.Services.Core;
using StudentTracker.Services.Teacher;
using StudentTracker.Site.ViewModels;
using StudentTracker.Site.ViewModels.Teacher;

namespace StudentTracker.Site.Controllers {

    [Authorize]
    public class TeacherController : Controller {

        private TeacherService _teacherSvc;
        private StaticDataService _staticDataSvc;
        private AppointmentService _appSvc;
        public TeacherController(TeacherService teacherSvc, StaticDataService staticDataSvc, AppointmentService appSvc) {
            _teacherSvc = teacherSvc;
            _staticDataSvc = staticDataSvc;
            _appSvc = appSvc;
        }


        public ActionResult List() {
            var model = new TeacherListViewModel { Teachers = _staticDataSvc.GetTeachers() };
            return View(model);
        }

        public ActionResult Appointments(int id) {
            var model = new AppointmentViewModel {
                Appointments = _appSvc.GetAppointmentForTeacher(id),
                Teacher = _staticDataSvc.GetTeacher(id)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Appointments(AppointmentViewModel viewModel) {
            var model = new AppointmentViewModel {
                Appointments = viewModel.FilterDate == null ? 
                        _appSvc.GetAppointmentForTeacher(viewModel.Teacher.Id) : 
                        _appSvc.GetAppointmentForTeacher(viewModel.Teacher.Id, viewModel.FilterDate.Value),
                FilterDate = viewModel.FilterDate,
                Teacher = _staticDataSvc.GetTeacher(viewModel.Teacher.Id)
            };
            return View(model);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TeacherCreateViewModel model) {
            _teacherSvc.AddTeacher(model.GetTeacher());
            return RedirectToAction("List", "Teacher");
        }


    }
}
