using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository;
using Topic = StudentTracker.Site.ViewModels.Topic.Topic;


namespace StudentTracker.Services.Core {
    public class StaticDataService {
        private readonly ISqlUnitOfWork _uow;

        public StaticDataService(ISqlUnitOfWork uow) {
            _uow = uow;
        }
        public IEnumerable<Site.ViewModels.Course.Course> GetCourses() {
            //using (var repo = new MongoRepository<Course>(CoreService.GetServer())) {
            //return _uow.Courses.Fetch().ToList();
            return _uow.Courses.Fetch().ToList().Select(x => new StudentTracker.Site.ViewModels.Course.Course { Id = x.Id, Name = x.Name, Topics = x.Topics.Select(y => new Topic { Id = y.Id, Name = y.Name }) });
            // }
        }

        public Course GetStudentChildCourse(int studentId) {
            //using (var repo = new MongoRepository<Student>(CoreService.GetServer())) {
            return _uow.Students.Single(x => x.Id == studentId).Course;
        }
        //}

        public IEnumerable<Site.ViewModels.Teacher.Teacher> GetTeachers() {
            //using (var repo = new MongoRepository<Teacher>(CoreService.GetServer())) {
            return _uow.Teachers.Fetch().Select(x => new Site.ViewModels.Teacher.Teacher { Id = x.Id, Mobile = x.Mobile, Name = x.Mobile });
        }
        //}

        public Site.ViewModels.Teacher.Teacher GetTeacher(int teacherId) {
            // using (var repo = new MongoRepository<Teacher>(CoreService.GetServer())) {
            var temp = _uow.Teachers.Single(x => x.Id == teacherId); ;
            //return _uow.Teachers.Single(x => x.Id == teacherId);
            return (new Site.ViewModels.Teacher.Teacher { Id = temp.Id, Mobile = temp.Mobile, Name = temp.Name })
            ;
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

        public IEnumerable<Site.ViewModels.StudyCenter.StudyCenter> GetStudyCenters() {
            //using(var repo=new MongoRepository<StudyCenter>(CoreService.GetServer())) {
            var temp= _uow.StudyCenters.Fetch().ToList();
            return (temp.Select(x => new Site.ViewModels.StudyCenter.StudyCenter { Id = x.Id, Name = x.Name, Address = x.Address }));
            // }
        }

        public IEnumerable<Site.ViewModels.Teacher.Teacher> ConvetToViewModel(IEnumerable<Models.Teacher> enumerable) {
            return (enumerable.Select(x => new Site.ViewModels.Teacher.Teacher { Id = x.Id, Name = x.Name, Mobile = x.Mobile }));
        }
    }
}
