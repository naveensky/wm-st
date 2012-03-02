using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentTracker.Models;
using StudentTracker.Site.ViewModels.User;
using StudentTracker.Services.Core;
using StudentTracker.Services.User;
using StudentTracker.Services.Authentication;


namespace StudentTracker.Site.Controllers {

  
    public class UserController : Controller {
        //
        // GET: /User/
        private StaticDataService _staticData;
        private UserService _userService;
        private AuthenticationProvider _authenticationProvider;
        private FormsAuthenticationService _formAuthentication;


        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            _staticData = new StaticDataService();
            _userService = new UserService();
            _authenticationProvider = new AuthenticationProvider();
            _formAuthentication=new FormsAuthenticationService();
            base.Initialize(requestContext);
        }

        public ActionResult Index() {
            return View();
        }

        public ActionResult Create() {
            var viewModel = new UserCreateViewModel();
            viewModel.StudyCenters = _staticData.GetStudyCenters();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(UserCreateViewModel viewModel) {
            _userService.SaveUser(viewModel.User, viewModel.StudyCenterId);
            return RedirectToAction("List", "Student");
        }

        public ActionResult LogOn() {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(User user, string password) {
            if (ModelState.IsValid) {
                if (_authenticationProvider.ValidateUser(user.Username, password)) {
                    _formAuthentication.LogOn(user.Username, false);
                    return RedirectToAction("List", "Student");
                }
            } else {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View(user);
        }

        public ActionResult LogOff() {
            _formAuthentication.LogOut();
            return RedirectToAction("List", "Student");
        }

    }
}
