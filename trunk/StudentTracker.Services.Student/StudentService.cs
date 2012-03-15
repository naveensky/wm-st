using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository;
using StudentTracker.Services.Core;
using StudentTracker.Services.User;

namespace StudentTracker.Services.Student {
    public class StudentService {
        private readonly ISqlUnitOfWork _uow;
        private readonly UserService _userService;

        public StudentService(ISqlUnitOfWork uow, UserService userService) {
            _uow = uow;
            _userService = userService;
        }

        public void SaveStudent(Models.Student student, int courseId, int studyCenterId) {
            var course = _uow.Courses.Single(x => x.Id == courseId);
            student.Course = course;
            var studyCenter = _uow.StudyCenters.Single(x => x.Id == studyCenterId);
            student.StudyCenter = studyCenter;
            student.Roll = string.Format("{0}-{1:#0}-{2:####0}", DateTime.Now.Year, DateTime.Now.Month,
                                         _uow.Students.Fetch().Count() + 1);
            student.ModifiedDate = DateTime.Now;
            _uow.Students.Add(student);
            _uow.Students.SaveChanges();
        }

        public Models.Student GetStudent(int studentId) {
            Models.Student student = _uow.Students.FindById(studentId);
            /*student.Id = studentId;
            var temp = _uow.Students.Single(x => x.Id == studentId);
            student.Name = temp.Name;
            student.Roll = temp.Roll;
            //return _uow.Students.Single(x => x.Id == studentId);*/
            return student;
        }

        public IEnumerable<Models.Student> GetAllStudents() {
            return _uow.Students.Fetch().OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Models.Student> GetStudents(int count) {
            var studyCentreId = _userService.GetCurrentUser().StudyCenter.Id;
            return (_uow.Students.Find(x => x.StudyCenter.Id == studyCentreId).Take(count).OrderBy(x => x.Name).ToList());
            /*
            var temp = _uow.Students.Find(x => x.StudyCenter.Id == studyCentreId).Take(count).OrderBy(x => x.Name).ToList();
            return (temp.Select(x => new Site.ViewModels.Student.Student { Id = x.Id, Name = x.Name, Roll = x.Roll }));*/
        }

        public IEnumerable<Models.Student> SearchStudents(string studentName, string rollNo, int courseId) {
            var studyCentreId = _userService.GetCurrentUser().StudyCenter.Id;
            var students = _uow.Students.Find(x => x.StudyCenter.Id == studyCentreId);

            //filter by studentname if not empty
            students = string.IsNullOrEmpty(studentName)
                           ? students
                           : students.Where(x => x.Name.ToLower().Contains(studentName.ToLower()));

            //filter by roll number if not empty
            students = string.IsNullOrEmpty(rollNo)
                           ? students
                           : students.Where(x => x.Roll.ToLower().Contains(rollNo.ToLower()));

            //filter by course if not empty
            students = courseId != 0
                           ? students.Where(x => x.Course.Id == courseId)
                           : students;

            return students.OrderBy(x => x.Name).ToList();
        }

        public void UpdateStudent(Models.Student student, int studyCenterId) {
            var originalStudent = _uow.Students.Single(x => x.Id == student.Id);
            student.StudyCenter = _uow.StudyCenters.FindById(studyCenterId);
            originalStudent.StudyCenter = _uow.StudyCenters.FindById(studyCenterId);

            originalStudent.ActualExamDate = student.ActualExamDate;
            originalStudent.BooksGiven = student.BooksGiven;
            originalStudent.Email = student.Email;
            originalStudent.Mobile = student.Mobile;
            originalStudent.Score = student.Score;
            originalStudent.SoftwareGiven = student.SoftwareGiven;
            originalStudent.WmPrepUsername = student.WmPrepUsername;
            originalStudent.ModifiedDate = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            _uow.Students.SaveChanges();
        }

        public void DeleteStudent(int studentId) {
            var student = _uow.Students.Single(x => x.Id == studentId);
            _uow.Students.Remove(student);

        }

        public IEnumerable<Models.Student> GetStudentsAsLead(DateTime startTime, DateTime endTime, bool getAll) {
            //using (var repo = new MongoRepository<Models.Student>(CoreService.GetServer())) {
            var studentsWithScore = _uow.Students.Find(x => x.Score >= 0).ToList().Where(x => x.ModifiedDate.Date >= startTime.Date && x.ModifiedDate.Date <= endTime.Date);

            // studentsWithScore = studentsWithScore).ToList();

            if (!getAll)
                studentsWithScore = studentsWithScore.Where(x => !x.IsDownloaded).ToList();

            var studentInRange = _uow.Students.Find(x => x.ActualExamDate != null).ToList().Where(x => x.ActualExamDate.Value.Date <= endTime.Date && x.ActualExamDate.Value.Date >= endTime.Date);

            if (!getAll)
                studentInRange = studentInRange.Where(x => !x.IsDownloaded);

            var data = studentsWithScore.ToList();
            data.AddRange(studentInRange);

            foreach (var student in data) {
                student.ModifiedDate = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                student.IsDownloaded = true;
                _uow.Students.Add(student);
                _uow.Students.SaveChanges();
            }

            return studentsWithScore;
            // }
        }

        public IDictionary<int, string> GetStudentsDictionary() {
            return (_uow.Students.Fetch().ToDictionary(x => x.Id, x => x.Name));
        }

        /*  public IEnumerable<Models.Student> ConvetToViewModel(IEnumerable<Models.Student> enumerable) {
              return (enumerable.Select(x => Site.ViewModels.Student.Student { Id = x.Id, Name = x.Name, Roll = x.Roll }));
          }*/
    }
}
