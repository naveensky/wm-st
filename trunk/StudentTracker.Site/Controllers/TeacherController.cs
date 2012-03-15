using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Norm;
using StudentTracker.Mappings;
//using StudentTracker.Services.Appointment;
using StudentTracker.Services.Core;
using StudentTracker.Services.Teacher;
using StudentTracker.Site.ViewModels;
using StudentTracker.Site.ViewModels.Teacher;

namespace StudentTracker.Site.Controllers {

    [Authorize]
    public class TeacherController : Controller {

        private TeacherService _teacherSvc;
        private StaticDataService _staticDataSvc;
       // private AppointmentService _appSvc;
        public TeacherController(TeacherService teacherSvc, StaticDataService staticDataSvc/*, AppointmentService appSvc*/) {
            _teacherSvc = teacherSvc;
            _staticDataSvc = staticDataSvc;
         //   _appSvc = appSvc;
        }


        public ActionResult List() {
            var model = new TeacherListViewModel { Teachers = _staticDataSvc.GetTeachers().Select(x=>x.MapToView())};
            return View(model);
        }

      /*  public ActionResult Appointments(int id) {
            var model = new AppointmentViewModel {
                Appointments = _appSvc.GetAppointmentForTeacher(id).Select(x=>x.MapToView()),
                Teacher = _staticDataSvc.GetTeacher(id).MapToView()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Appointments(AppointmentViewModel viewModel) {
            var model = new AppointmentViewModel {
                Appointments = viewModel.FilterDate == null ?
                        _appSvc.GetAppointmentForTeacher(viewModel.Teacher.Id).Select(x => x.MapToView()) :
                        _appSvc.GetAppointmentForTeacher(viewModel.Teacher.Id, viewModel.FilterDate.Value).Select(x => x.MapToView()),
                FilterDate = viewModel.FilterDate,
                Teacher = _staticDataSvc.GetTeacher(viewModel.Teacher.Id).MapToView()
            };
            return View(model);
        }*/

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
