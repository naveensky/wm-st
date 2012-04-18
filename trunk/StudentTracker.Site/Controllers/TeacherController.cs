using System;
using System.Linq;
using System.Web.Mvc;
using StudentTracker.Mappings;
using StudentTracker.Services.Appointment;
using StudentTracker.Services.Core;
using StudentTracker.Services.Teacher;
using StudentTracker.Services.Time;
using StudentTracker.Site.ViewModels.Appointment;
using StudentTracker.Site.ViewModels.Teacher;

namespace StudentTracker.Site.Controllers {

    [Authorize]
    public class TeacherController : Controller {

        private readonly TeacherService _teacherSvc;
        private readonly StaticDataService _staticDataSvc;
        private readonly AppointmentService _appSvc;
        private readonly TimeService _timeService;

        public TeacherController(TeacherService teacherSvc, StaticDataService staticDataSvc, AppointmentService appSvc, TimeService timeService) {
            _teacherSvc = teacherSvc;
            _staticDataSvc = staticDataSvc;
            _appSvc = appSvc;
            _timeService = timeService;
        }


        public ActionResult List() {
            var model = new TeacherListViewModel { Teachers = _staticDataSvc.GetTeachers().Select(x => x.MapToView()) };
            return View(model);
        }

        public ActionResult Appointments(int id) {
            var model = new AppointmentViewModel {
                Appointments = _appSvc.GetAppointmentForTeacher(id).Select(x => new TeacherAppointModel { Date = x.Date, StartTime = x.StartTime, EndTime = x.EndTime, Topic = x.Topic.Name,IsPersonal = x.IsPersonal}),
                Teacher = _staticDataSvc.GetTeacher(id).MapToView(),
                FilterDate = DateTime.Now
            };
            var totalDuration = _timeService.GetTotalDuration(model.Appointments.Select(x => x.Duration));
            if (totalDuration - Math.Floor(totalDuration) != 0)
                ViewBag.TotalDuration = (int)totalDuration + " Hours " + ((int)(totalDuration - Math.Floor(totalDuration)) * 60) + " Minutes";
            else {
                ViewBag.TotalDuration = (int)totalDuration + " Hours ";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Appointments(AppointmentViewModel viewModel) {
            var model = new AppointmentViewModel {
                Appointments = _appSvc.GetAppointmentForTeacher(viewModel.Teacher.Id).Select(x => new TeacherAppointModel { Date = x.Date, StartTime = x.StartTime, EndTime = x.EndTime, Topic = x.Topic.Name,IsPersonal = x.IsPersonal}).Where(x => x.Date.Date.Ticks == viewModel.FilterDate.Date.Ticks),
                FilterDate = viewModel.FilterDate,
                Teacher = _staticDataSvc.GetTeacher(viewModel.Teacher.Id).MapToView()
            };
            var totalDuration = _timeService.GetTotalDuration(model.Appointments.Select(x => x.Duration));
            if (totalDuration - Math.Floor(totalDuration) != 0)
                ViewBag.TotalDuration = (int)totalDuration + " Hours " + (int)((totalDuration - Math.Floor(totalDuration)) * 60) + " Minutes";
            else {
                ViewBag.TotalDuration = (int)totalDuration + " Hours ";
            }
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
