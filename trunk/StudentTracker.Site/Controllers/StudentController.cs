using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Norm;
using StudentTracker.Services.Core;
using StudentTracker.Services.Student;
using StudentTracker.Site.ViewModels;
using StudentTracker.Converters;
using StudentTracker.Site.ViewModels.Student;

namespace StudentTracker.Site.Controllers {

    [Authorize]
    public class StudentController : Controller {

        private readonly StaticDataService _staticData;
        private readonly StudentService _studentService;
        public StudentController(StaticDataService staticData, StudentService studentService) {
            _staticData = staticData;
            _studentService = studentService;
        }


        public ActionResult Create() {
            var model = new StudentRegisterViewModel {
                Courses = _staticData.GetCourses(),
                StudyCenters = _staticData.GetStudyCenters(),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StudentRegisterViewModel viewModel) {
            _studentService.SaveStudent(viewModel.Student, viewModel.CourseId, viewModel.StudyCenterId);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List() {
            var model = new StudentListViewModel {
                Students = _studentService.GetStudents(100),
                Courses = _staticData.GetCourses()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult List(StudentListViewModel studentSearch) {
            var model = new StudentListViewModel {
                Students = studentSearch.IsSearchEmpty
                               ? _studentService.GetAllStudents()
                               : _studentService.SearchStudents(studentSearch.StudentNameSearchText,
                                                                studentSearch.RollNoSearchText,
                                                                studentSearch.CourseId),
                CourseId = studentSearch.CourseId,
                RollNoSearchText = studentSearch.RollNoSearchText,
                StudentNameSearchText = studentSearch.StudentNameSearchText,
                Courses = _staticData.GetCourses()
            };

            return View(model);
        }

        public ActionResult Edit(int id) {
            var studentEditModel = new StudentEditViewModel { Student = _studentService.GetStudent(id) };
            studentEditModel.Courses = _staticData.GetCourses();
            studentEditModel.StudyCenters = _staticData.GetStudyCenters();
            studentEditModel.StudyCenterId = CoreService.GetCurrentUser().StudyCenter.Id;
            studentEditModel.CourseId = studentEditModel.Student.Course.Id;
            return View(studentEditModel);
        }

        [HttpPost]
        public ActionResult Edit(StudentEditViewModel model) {
            _studentService.UpdateStudent(model.Student, model.StudyCenterId);
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
