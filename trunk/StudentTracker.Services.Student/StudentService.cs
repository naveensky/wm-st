using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository.MongoDb;
using StudentTracker.Services.Core;

namespace StudentTracker.Services.Student {
    public class StudentService {
        public void SaveStudent(Models.Student student, ObjectId courseId, ObjectId studyCenterId) {
            using (var studentRepo = new MongoRepository<Models.Student>(CoreService.GetServer())) {
                using (var courseRepo = new MongoRepository<Course>(CoreService.GetServer())) {
                    using (var studyCenterRepo = new MongoRepository<StudyCenter>(CoreService.GetServer())) {
                        var course = courseRepo.Collection.Single(x => x.Id == courseId);
                        student.Course = course;
                        var studyCenter = studyCenterRepo.Collection.Single(x => x.Id == studyCenterId);
                        student.StudyCenter = studyCenter;
                        student.Roll = string.Format("{0}-{1:#0}-{2:####0}", DateTime.Now.Year, DateTime.Now.Month,
                                                     studentRepo.Collection.Count() + 1);
                        studentRepo.Save(student);
                    }
                }
            }
        }

        public Models.Student GetStudent(ObjectId studentId) {
            using (var repo = new MongoRepository<Models.Student>(CoreService.GetServer())) {
                return repo.Collection.Single(x => x.Id == studentId);
            }
        }

        public IEnumerable<Models.Student> GetAllStudents() {
            using (var studentRepo = new MongoRepository<Models.Student>(CoreService.GetServer())) {
                return studentRepo.Collection.OrderBy(x => x.Name).ToList();
            }
        }

        public IEnumerable<Models.Student> GetStudents(int count) {
            using (var studentRepo = new MongoRepository<Models.Student>(CoreService.GetServer())) {
                var studyCentreId = CoreService.GetCurrentUser().StudyCenter.Id;
                return studentRepo.Collection.Where(x => x.StudyCenter.Id == studyCentreId).Take(count).OrderBy(x => x.Name).ToList();
            }
        }

        public IEnumerable<Models.Student> SearchStudents(string studentName, string rollNo, ObjectId courseId) {
            using (var studentRepo = new MongoRepository<Models.Student>(CoreService.GetServer())) {
                var studyCentreId = CoreService.GetCurrentUser().StudyCenter.Id;
                var students = studentRepo.Collection.Where(x => x.StudyCenter.Id == studyCentreId);

                //filter by studentname if not empty
                students = string.IsNullOrEmpty(studentName)
                               ? students
                               : students.Where(x => x.Name.ToLower().Contains(studentName.ToLower()));

                //filter by roll number if not empty
                students = string.IsNullOrEmpty(rollNo)
                               ? students
                               : students.Where(x => x.Roll.ToLower().Contains(rollNo.ToLower()));

                //filter by course if not empty
                students = courseId != null
                               ? students.Where(x => x.Course.Id == courseId)
                               : students;

                return students.OrderBy(x => x.Name).ToList();
            }
        }

        public void UpdateStudent(Models.Student student, ObjectId studyCenterId) {
            using (var repo = new MongoRepository<Models.Student>(CoreService.GetServer())) {
                var originalStudent = repo.Collection.Single(x => x.Id == student.Id);
                using (var studyCenterRepo = new MongoRepository<StudyCenter>(CoreService.GetServer())) {
                    originalStudent.StudyCenter = studyCenterRepo.Collection.Single(x => x.Id == studyCenterId);
                    originalStudent.ActualExamDate = student.ActualExamDate;
                    originalStudent.BooksGiven = student.BooksGiven;
                    originalStudent.Email = student.Email;
                    originalStudent.Mobile = student.Mobile;
                    originalStudent.SoftwareGiven = student.SoftwareGiven;
                    originalStudent.WmPrepUsername = student.WmPrepUsername;
                    originalStudent.ModifiedDate = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    repo.Save(originalStudent);
                }
            }
        }

        public void DeleteStudent(ObjectId studentId) {
            using (var repo = new MongoRepository<Models.Student>(CoreService.GetServer())) {
                var student = repo.Collection.Single(x => x.Id == studentId);
                repo.Delete(student);
            }
        }

        public IEnumerable<Models.Student> GetStudentsAsLead(DateTime startTime, DateTime endTime, bool getAll) {
            using (var repo = new MongoRepository<Models.Student>(CoreService.GetServer())) {
                var students = repo.Collection.Where(x => x.Score != 0);

                students = students.Where(x => x.ModifiedDate.Date >= startTime.Date && x.ModifiedDate.Date <= endTime.Date);

                if (!getAll)
                    students = students.Where(x => !x.IsDownloaded);

                var studentInRange = repo.Collection.Where(x => x.ActualExamDate.HasValue && x.ActualExamDate.Value <= endTime.Date && x.ActualExamDate.Value.Date >= endTime.Date);

                if (!getAll)
                    studentInRange = studentInRange.Where(x => !x.IsDownloaded);

                var data = students.ToList();
                data.AddRange(studentInRange);
                data.ForEach(x => x.ModifiedDate = DateTime.UtcNow.AddHours(5).AddMinutes(30));
                return students;
            }
        }

    }
}
