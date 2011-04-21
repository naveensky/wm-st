using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentTracker.Service.Abstract;
using StudentTracker.Website.Helpers;
using StudentTracker.Website.Infrastructure;
using StudentTracker.Website.ViewModels.Student;

namespace StudentTracker.Website.Controllers {
    public class StudentController : Controller {

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService) {
            _studentService = studentService;
        }

        public int PageSize {
            get { return 25; }
        }

        [Authorize]
        public ViewResult List(int page = 1) {
            var viewmodel = new StudentListViewModel();
            var students = _studentService.Students.Skip((page - 1) * PageSize).Take(PageSize);
            var total = _studentService.Students.Count();
            viewmodel.PagingInfo = new PagingInfo { CurrentPage = page, PageSize = PageSize, TotalItems = total };
            viewmodel.Students = students;
            return View(viewmodel);
        }



    }
}
