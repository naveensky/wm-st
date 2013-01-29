using System.Linq;
using System.Web.Mvc;
using StudentTracker.Mappings;
using StudentTracker.Services.Core;
using StudentTracker.Services.Student;
using StudentTracker.Services.User;
using StudentTracker.Converters;
using StudentTracker.Site.ViewModels.Student;

namespace StudentTracker.Site.Controllers {

    [Authorize]
    public class StudentController : Controller {

        private readonly StaticDataService staticData;
        private readonly StudentService studentService;
        private readonly UserService userService;

        public StudentController(StaticDataService staticData, StudentService studentService, UserService userService) {
            this.staticData = staticData;
            this.studentService = studentService;
            this.userService = userService;
        }


        public ActionResult Create() {
            var model = new StudentRegisterViewModel {
                Courses = staticData.GetCourses().ToDictionary(x => x.Id, y => y.Name),
                StudyCenters = staticData.GetStudyCenters().ToDictionary(x => x.Id, y => y.Name),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StudentRegisterViewModel viewModel) {
            studentService.SaveStudent(viewModel.MapToDomain(), viewModel.CourseId, viewModel.StudyCenterId);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List() {
            var model = new StudentListViewModel {
                Students = studentService.GetStudents(100).Select(x => new StudentViewModel { Id = x.Id, BooksGiven = x.BooksGiven, Mobile = x.Mobile, Course = x.Course.MapToView(), Name = x.Name, Roll = x.Roll, SoftwareGiven = x.SoftwareGiven }),
                Courses = staticData.GetCourses().ToDictionary(x => x.Id, y => y.Name),
            };
            model.Courses.Add(0, "All Course");
            return View(model);
        }

        [HttpPost]
        public ActionResult List(StudentListViewModel studentSearch) {
            var model = new StudentListViewModel {
                Students = studentSearch.IsSearchEmpty
                               ? studentService.GetAllStudents().Select(x => new StudentViewModel { Id = x.Id, BooksGiven = x.BooksGiven, Mobile = x.Mobile, Course = x.Course.MapToView(), Name = x.Name, Roll = x.Roll, SoftwareGiven = x.SoftwareGiven })
                               : studentService.SearchStudents(studentSearch.StudentNameSearchText, studentSearch.RollNoSearchText, studentSearch.CourseId).Select(x => new StudentViewModel { Id = x.Id, BooksGiven = x.BooksGiven, Mobile = x.Mobile, Course = x.Course.MapToView(), Name = x.Name, Roll = x.Roll, SoftwareGiven = x.SoftwareGiven }),
                CourseId = studentSearch.CourseId,
                RollNoSearchText = studentSearch.RollNoSearchText,
                StudentNameSearchText = studentSearch.StudentNameSearchText,
                Courses = staticData.GetCourses().ToDictionary(x => x.Id, y => y.Name)
            };
            model.Courses.Add(0, "All Course");
            return View(model);
        }

        public ActionResult Edit(int id) {
            var temp = studentService.GetStudent(id);
            var studentEditModel = new StudentEditViewModel { StudentViewModel = temp.MapToView(), Courses = staticData.GetCourses().ToDictionary(x => x.Id, y => y.Name), StudyCenters = staticData.GetStudyCenters().ToDictionary(x => x.Id, y => y.Name) };
            studentEditModel.StudyCenterId = userService.GetCurrentUser().StudyCenter.Id;
            return View(studentEditModel);
        }

        [HttpPost]
        public ActionResult Edit(StudentEditViewModel model) {
            studentService.UpdateStudent(model.StudentViewModel.MapToDomain(), model.StudyCenterId);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id) {
            studentService.DeleteStudent(id);
            return RedirectToAction("List");
        }

        public void GenerateExcel(int id) {
            var student = studentService.GetStudent(id);
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


        public void DownloadLeadsExcel(StudentDownloadViewModel viewModel) {
            var students = studentService.GetStudentsAsLead(viewModel.StartDate, viewModel.EndDate,
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

        public ActionResult GenerateStatus() {
            var status = new StatusModel();
            status.StudentStatus = studentService.GetStudentStatus().Select(x => new ValueModel { Student = x.Key.MapToView(), LastAppointment = x.Value });
            return View(status);
        }

        public ActionResult PaymentStatus(int studentId) {
            var student = studentService.GetStudent(studentId);
            var model = new PaymentModel() {
                AmountPaid = student.AmountPaid,
                PaymentDate = student.PaymentDate,
                AmountPending = student.AmountPending,
                StudentId = studentId
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult PaymentStatus(PaymentModel paymentModel) {
            studentService.UpdatePaymentStatus(paymentModel);
            return RedirectToAction("List");
        }
    }
}
