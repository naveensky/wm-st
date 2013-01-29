using System;
using System.Collections.Generic;
using System.Linq;
using StudentTracker.Repository;
using StudentTracker.Services.User;
using StudentTracker.Site.ViewModels.Student;

namespace StudentTracker.Services.Student {
    public class StudentService {
        private readonly ISqlUnitOfWork uow;
        private readonly UserService userService;

        public StudentService(ISqlUnitOfWork uow, UserService userService) {
            this.uow = uow;
            this.userService = userService;
        }

        public void SaveStudent(Models.Student student, int courseId, int studyCenterId) {
            var course = uow.Courses.Single(x => x.Id == courseId);
            student.Course = course;
            var studyCenter = uow.StudyCenters.Single(x => x.Id == studyCenterId);
            student.StudyCenter = studyCenter;
            student.Roll = string.Format("{0}-{1:#0}-{2:####0}", DateTime.Now.Year, DateTime.Now.Month,
                                         uow.Students.Fetch().Count() + 1);
            student.ModifiedDate = DateTime.Now;
            uow.Students.Add(student);
            uow.Students.SaveChanges();
        }

        public Models.Student GetStudent(int studentId) {
            Models.Student student = uow.Students.FindById(studentId);
            return student;
        }

        public IEnumerable<Models.Student> GetAllStudents() {
            return uow.Students.Fetch().OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Models.Student> GetStudents(int count) {
            var studyCentreId = userService.GetCurrentUser().StudyCenter.Id;

            return (uow.Students.Find(x => x.StudyCenter.Id == studyCentreId).Take(count).OrderBy(x => x.Name).ToList());
        }

        public IEnumerable<Models.Student> SearchStudents(string studentName, string rollNo, int courseId) {
            var studyCentreId = userService.GetCurrentUser().StudyCenter.Id;

            var students = uow.Students.Find(x => x.StudyCenter.Id == studyCentreId);

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
            var originalStudent = uow.Students.Single(x => x.Id == student.Id);
            student.StudyCenter = uow.StudyCenters.FindById(studyCenterId);
            originalStudent.StudyCenter = uow.StudyCenters.FindById(studyCenterId);

            originalStudent.ActualExamDate = student.ActualExamDate;
            originalStudent.BooksGiven = student.BooksGiven;
            originalStudent.Email = student.Email;
            originalStudent.Mobile = student.Mobile;
            originalStudent.Score = student.Score;
            originalStudent.SoftwareGiven = student.SoftwareGiven;
            originalStudent.WmPrepUsername = student.WmPrepUsername;
            originalStudent.ModifiedDate = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            uow.Students.SaveChanges();
        }

        public void DeleteStudent(int studentId) {
            var student = uow.Students.Single(x => x.Id == studentId);
            uow.Students.Remove(student);

        }

        public IEnumerable<Models.Student> GetStudentsAsLead(DateTime startTime, DateTime endTime, bool getAll) {
            //using (var repo = new MongoRepository<Models.Student>(CoreService.GetServer())) {
            var studentsWithScore = uow.Students.Find(x => x.Score >= 0).ToList().Where(x => x.ModifiedDate.Date >= startTime.Date && x.ModifiedDate.Date <= endTime.Date);

            // studentsWithScore = studentsWithScore).ToList();

            if (!getAll)
                studentsWithScore = studentsWithScore.Where(x => !x.IsDownloaded).ToList();

            var studentInRange = uow.Students.Find(x => x.ActualExamDate != null).ToList().Where(x => x.ActualExamDate.Value.Date <= endTime.Date && x.ActualExamDate.Value.Date >= endTime.Date);

            if (!getAll)
                studentInRange = studentInRange.Where(x => !x.IsDownloaded);

            var data = studentsWithScore.ToList();
            data.AddRange(studentInRange);

            foreach (var student in data) {
                student.ModifiedDate = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                student.IsDownloaded = true;
                uow.Students.Add(student);
                uow.Students.SaveChanges();
            }

            return studentsWithScore;
            // }
        }

        public IDictionary<int, string> GetStudentsDictionary() {
            return (uow.Students.Fetch().ToDictionary(x => x.Id, x => x.Name));
        }

        /*  public IEnumerable<Models.Student> ConvetToViewModel(IEnumerable<Models.Student> enumerable) {
              return (enumerable.Select(x => Site.ViewModels.Student.Student { Id = x.Id, Name = x.Name, Roll = x.Roll }));
          }*/

        public IEnumerable<Models.Student> GetStudentsByTopic(int topicId) {
            var studyCentreId = userService.GetCurrentUser().StudyCenter.Id;
            int couseId = uow.Topics.FindById(topicId).Course.Id;
            var students = uow.Students.Find(x => x.Course.Id == couseId && x.StudyCenter.Id == studyCentreId);
            return students;
        }

        public IDictionary<Models.Student, DateTime> GetStudentStatus() {
            var studyCentreId = userService.GetCurrentUser().StudyCenter.Id;
            var students = uow.Students.Fetch().Where(x => x.StudyCenter.Id == studyCentreId);
            var status = new Dictionary<Models.Student, DateTime>();
            var statusForNonAppointed = new Dictionary<Models.Student, DateTime>();
            foreach (var student in students) {
                var lastAppointmentDate = new DateTime();
                if (student.Appointments.Count != 0) {
                    lastAppointmentDate = student.Appointments.Max(x => x.Date);


                    var today = DateTime.Now;
                    var temp = lastAppointmentDate.AddDays(10);
                    if (temp <= today)
                        status.Add(student, lastAppointmentDate);
                } else {
                    statusForNonAppointed.Add(student, lastAppointmentDate);
                }
            }
            var sortstatus = (from temp in status orderby temp.Value ascending select temp).ToDictionary(x => x.Key,
                                                                                                         y => y.Value);
            var sortstatusForNonAppointed = (from temp in statusForNonAppointed orderby (DateTime.Now - temp.Key.RegisterDate) descending select temp).ToDictionary(x => x.Key,
                                                                                                         y => y.Value);
            var resultstatus = sortstatusForNonAppointed.Concat(sortstatus).ToDictionary(x => x.Key, y => y.Value);
            return resultstatus;
        }

        public void UpdatePaymentStatus(PaymentModel paymentModel) {
            var student=uow.Students.FindById(paymentModel.StudentId);
            student.AmountPaid=paymentModel.AmountPaid;
            student.AmountPending=paymentModel.AmountPending;
            student.PaymentDate=paymentModel.PaymentDate;
            uow.Students.SaveChanges();
        }
    }
}
