using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentTracker.Domain;
using StudentTracker.Service.Abstract;

namespace StudentTracker.Website.Controllers {
    public class UserController : Controller {

        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        //GET : /User
        public ViewResult List() {
            throw new NotImplementedException();
        }

        public ViewResult Add() {
            return View();
        }

        [HttpPost]
        public ActionResult Add(User user) {
            if (!ModelState.IsValid)
                return View(user);

            _userService.AddUser(user);
            return RedirectToAction("List");        //return to list of users once user is created
        }

        public ViewResult Logon() {
            return View();
        }

        [HttpPost]
        public ViewResult Logon(string username, string password) {
            throw new NotImplementedException();
        }

        public ActionResult LogOff() {
            throw new NotImplementedException();
            //this will redirect to logon screen 
        }
    }
}
