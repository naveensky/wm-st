using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using StudentTracker.Services.Core;
using StudentTracker.Services.Student;
using StudentTracker.Services.User;
using StudentTracker.Site.Controllers;

using NUnit.Framework;
using StudentTracker.Site.ViewModels.Student;

namespace StudentTracker.Test {
     [TestFixture]
    public class StudentControllerTest {

        private StaticDataService _staticData;
        private StudentService _studentService;
        private UserService _userService;
        private StudentController _studentController;
          [TestFixtureSetUp]
        public void Setup(){
            _staticData = new Mock<StaticDataService>().Object;
            _studentService = new Mock<StudentService>().Object;
            _userService = new Mock<UserService>().Object;
            _studentController = new StudentController(_staticData, _studentService, _userService);
        }
         [Test]
          public void Create_Test() {
              var actionResult = _studentController.Create();
              Assert.IsNotNull(actionResult);
          }

          [Test]
          public void Create_Post_Test() {
              var actionResult = _studentController.Create(new StudentRegisterViewModel());
              Assert.IsNotNull(actionResult);
          }

          [Test]
          public void List_Test() {
              var actionResult = _studentController.Create(new StudentRegisterViewModel());
              Assert.IsNotNull(actionResult);
          }

          [Test]
          public void List_Post_Test() {
              var actionResult = _studentController.List(new StudentListViewModel());
              Assert.IsNotNull(actionResult);
          }
          [Test]
          public void Edit_Test() {
              var actionResult = _studentController.Edit(3);
              Assert.IsNotNull(actionResult);
          }

          [Test]
          public void Edit_Post_Test() {
              var actionResult = _studentController.Edit(new StudentEditViewModel());
              Assert.IsNotNull(actionResult);
          }
         [Test]
          public void Delete_Test() {
              var actionResult = _studentController.Delete(7);
              Assert.IsNotNull(actionResult);
          }
          [Test]
          public void GenerateExcel_Test() {
              _studentController.GenerateExcel(6);
          }
         [Test]
          public void DownloadLeads_Test() {
              var actionResult = _studentController.DownloadLeads();
              Assert.IsNotNull(actionResult);
          }

          [Test]
          public void DownloadLeadsExcel_Test_Post() {
            _studentController.DownloadLeadsExcel(new StudentDownloadViewModel());
          }

          [TestFixtureTearDown]
          public void Teardown() {
              _studentController.Dispose();
          }
    }
}
