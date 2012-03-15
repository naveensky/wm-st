using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository;
using StudentTracker.Site.ViewModels.Common;


namespace StudentTracker.Services.Core {
    public class StaticDataService {
        private readonly ISqlUnitOfWork _uow;

        public StaticDataService(ISqlUnitOfWork uow) {
            _uow = uow;
        }
        public IEnumerable<Course> GetCourses() {
            return _uow.Courses.Fetch();
        }
        public IEnumerable<Topic> GetTopics(int id) {
            int courseId = _uow.Students.FindById(id).Course.Id;
            return _uow.Topics.Fetch().Where(x=>x.Course.Id==courseId);
        }

        public Course GetStudentChildCourse(int studentId) {
            return _uow.Students.Single(x => x.Id == studentId).Course;
        }

        public IEnumerable<Teacher> GetTeachers() {
            return _uow.Teachers.Fetch();
            //.Select(x => new Site.ViewModels.Teacher.Teacher { Id = x.Id, Mobile = x.Mobile, Name = x.Mobile });
        }

        public Teacher GetTeacher(int teacherId) {
            return (_uow.Teachers.Single(x => x.Id == teacherId));
            //var temp = _uow.Teachers.Single(x => x.Id == teacherId); ;
            //return (new Site.ViewModels.Teacher.Teacher { Id = temp.Id, Mobile = temp.Mobile, Name = temp.Name });
        }

        public IEnumerable<Student> GetStudent() {
            return _uow.Students.Fetch().ToList();
        }

        public IEnumerable<TimeSlot> GetTimeSlots() {
            return _uow.TimeSlots.Fetch().ToList();
        }

        public IEnumerable<StudyCenter> GetStudyCenters() {
            return _uow.StudyCenters.Fetch();
        }

      /*  public IEnumerable<Teacher> ConvetToViewModel(IEnumerable<Models.Teacher> enumerable) {
            return (enumerable.Select(x => new Site.ViewModels.Teacher.Teacher { Id = x.Id, Name = x.Name, Mobile = x.Mobile }));
        }*/
    }
}
