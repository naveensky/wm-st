using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentTracker.Services.Loader;
using StudentTracker.Site.ViewModels.Load;

namespace StudentTracker.Site.Controllers {
    public class LoadController : Controller {

        private LoaderService _loaderService;
        private const string Password = "L0@d123";

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            _loaderService = new LoaderService();
            base.Initialize(requestContext);
        }

        public ActionResult Data() {
            return View();
        }

        [HttpPost]
        public ActionResult Data(DataViewModel model) {
            if (model.Password.Equals(Password)) {
                _loaderService.LoadData();
                return RedirectToAction("Create", "Teacher");
            }

            ModelState.AddModelError("Password", "Incorrect Password");
            return View(model);
        }

        //public ActionResult LoadTimeSlot() {
        //    _loaderService.LoadTimeSlot();
        //    return RedirectToAction("List", "Student");
        //}

        public ActionResult LoadStudyCenter() {
            _loaderService.LoadStudyCenters();
            return RedirectToAction("List", "Student");
        }

        public ActionResult CreateAdmin() {
            _loaderService.CreateAdmin();
            return RedirectToAction("List", "Student");
        }

        public ActionResult DeleteAllAppointments() {
            _loaderService.DeleteAllAppointments();
            return RedirectToAction("List", "Student");
        }

        public ActionResult AssignStudyCenter() {
            _loaderService.AssignStudyCenter();
            return RedirectToAction("List", "Student");
        }
    }
}
