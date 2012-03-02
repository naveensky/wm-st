using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository;


namespace StudentTracker.Services.Core {
    public class StaticDataService {
         private readonly ISqlUnitOfWork _uow;

        public StaticDataService(ISqlUnitOfWork uow) {
            _uow = uow;
        }
        public IEnumerable<Course> GetCourses() {
            //using (var repo = new MongoRepository<Course>(CoreService.GetServer())) {
                return _uow.Courses.Fetch().ToList();
           // }
        }

        public IEnumerable<Course> GetStudentChildCourse(int studentId) {
            //using (var repo = new MongoRepository<Student>(CoreService.GetServer())) {
                return _uow.Students.Single(x => x.Id == studentId).Course.Children;
            }
        //}

        public IEnumerable<Teacher> GetTeachers() {
            //using (var repo = new MongoRepository<Teacher>(CoreService.GetServer())) {
                return _uow.Teachers.Fetch().ToList();
            }
        //}

        public Teacher GetTeacher(int teacherId) {
           // using (var repo = new MongoRepository<Teacher>(CoreService.GetServer())) {
                return _uow.Teachers.Single(x => x.Id == teacherId);
            }
       // }

        public IEnumerable<Student> GetStudent() {
           // using (var repo = new MongoRepository<Student>(CoreService.GetServer())) {
                return _uow.Students.Fetch().ToList();
            }
        //}

        public IEnumerable<TimeSlot> GetTimeSlots() {
           // using (var repo = new MongoRepository<TimeSlot>(CoreService.GetServer())) {
                return _uow.TimeSlots.Fetch().ToList();
            }
        //}
        
        public IEnumerable<StudyCenter> GetStudyCenters() {
            //using(var repo=new MongoRepository<StudyCenter>(CoreService.GetServer())) {
                return _uow.StudyCenters.Fetch().ToList();
           // }
        }
    }
}
