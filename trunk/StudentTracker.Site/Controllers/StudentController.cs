using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Norm;
using StudentTracker.Mappings;
using StudentTracker.Services.Core;
using StudentTracker.Services.Student;
using StudentTracker.Services.User;
using StudentTracker.Site.ViewModels;
using StudentTracker.Converters;
using StudentTracker.Site.ViewModels.Student;
using Student = StudentTracker.Models.Student;

namespace StudentTracker.Site.Controllers {

    [Authorize]
    public class StudentController : Controller {

        private readonly StaticDataService _staticData;
        private readonly StudentService _studentService;
        private readonly UserService _userService;

        public StudentController(StaticDataService staticData, StudentService studentService, UserService userService) {
            _staticData = staticData;
            _studentService = studentService;
            _userService = userService;
        }


        public ActionResult Create() {
            var model = new StudentRegisterViewModel {
                Courses = _staticData.GetCourses().ToDictionary(x => x.Id, y => y.Name),
                StudyCenters = _staticData.GetStudyCenters().ToDictionary(x => x.Id, y => y.Name),
                //RegisterDate = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StudentRegisterViewModel viewModel) {
            _studentService.SaveStudent(viewModel.MapToDomain(), viewModel.CourseId, viewModel.StudyCenterId);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List() {
            var model = new StudentListViewModel {
                Students = _studentService.GetStudents(100).Select(x => new StudentViewModel { Id = x.Id, BooksGiven = x.BooksGiven, Mobile = x.Mobile, Course = x.Course.MapToView(), Name = x.Name, Roll = x.Roll, SoftwareGiven = x.SoftwareGiven }),
                Courses = _staticData.GetCourses().ToDictionary(x => x.Id, y => y.Name),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult List(StudentListViewModel studentSearch) {
            var model = new StudentListViewModel {
                Students = studentSearch.IsSearchEmpty
                               ? _studentService.GetAllStudents().Select(x => new StudentViewModel { Id = x.Id, BooksGiven = x.BooksGiven, Mobile = x.Mobile, Course = x.Course.MapToView(), Name = x.Name, Roll = x.Roll, SoftwareGiven = x.SoftwareGiven })
                               : _studentService.SearchStudents(studentSearch.StudentNameSearchText, studentSearch.RollNoSearchText, studentSearch.CourseId).Select(x => new StudentViewModel { Id = x.Id, BooksGiven = x.BooksGiven, Mobile = x.Mobile, Course = x.Course.MapToView(), Name = x.Name, Roll = x.Roll, SoftwareGiven = x.SoftwareGiven }),
                CourseId = studentSearch.CourseId,
                RollNoSearchText = studentSearch.RollNoSearchText,
                StudentNameSearchText = studentSearch.StudentNameSearchText,
                Courses = _staticData.GetCourses().ToDictionary(x => x.Id, y => y.Name)
            };

            return View(model);
        }

        public ActionResult Edit(int id) {
            var temp = _studentService.GetStudent(id);
            var studentEditModel = new StudentEditViewModel { StudentViewModel = temp.MapToView(), Courses = _staticData.GetCourses().ToDictionary(x => x.Id, y => y.Name), StudyCenters = _staticData.GetStudyCenters().ToDictionary(x => x.Id, y => y.Name) };
            // studentEditModel.Courses = _staticData.GetCourses().Select(x => x.MapToDomain());
            //studentEditModel.StudyCenters = _staticData.GetStudyCenters().Select(x => x.MapToDomain());
            studentEditModel.StudyCenterId = _userService.GetCurrentUser().StudyCenter.Id;
            return View(studentEditModel);
        }

        [HttpPost]
        public ActionResult Edit(StudentEditViewModel model) {
            _studentService.UpdateStudent(model.StudentViewModel.MapToDomain(), model.StudyCenterId);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id) {
            _studentService.DeleteStudent(id);
            return RedirectToAction("List");
        }

        public void GenerateExcel(int id) {
            var student = _studentService.GetStudent(id);
            var tempPath = CoreService.ConvertToAbsolute(CoreService.GetTempPath("xlsx"));
            var excelGenerator = new ExcelConverter();
            excelGenerator.GenerateExcel(tempPath, student);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=Student Record.xlsx");
            Response.BinaryWrite(System.IO.File.ReadAllBytes(tempPath));
        }

        public ActionResult DownloadLeads() {
            return View();
        }

        //[HttpPost]
        public void DownloadLeadsExcel(StudentDownloadViewModel viewModel) {
            var students = _studentService.GetStudentsAsLead(viewModel.StartDate, viewModel.EndDate,
                                                             viewModel.DownloadAll);


            var tempPath = CoreService.ConvertToAbsolute(CoreService.GetTempPath("xlsx"));

            var excelGenerator = new ExcelConverter();
            excelGenerator.GenerateLeadsExcel(tempPath, students);
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=Student Record.xlsx");
            Response.BinaryWrite(System.IO.File.ReadAllBytes(tempPath));
            CoreService.DeleteFile(tempPath);
        }
    }
}
