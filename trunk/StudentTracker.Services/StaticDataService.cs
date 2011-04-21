using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository.MongoDb;

namespace StudentTracker.Services.Core {
    public class StaticDataService {
        public IEnumerable<Course> GetCourses() {
            using (var repo = new MongoRepository<Course>(CoreService.GetServer())) {
                return repo.Collection.ToList();
            }
        }

        public IEnumerable<Course> GetStudentChildCourse(ObjectId studentId) {
            using (var repo = new MongoRepository<Student>(CoreService.GetServer())) {
                return repo.Collection.Single(x => x.Id == studentId).Course.Children;
            }
        }

        public IEnumerable<Teacher> GetTeachers() {
            using (var repo = new MongoRepository<Teacher>(CoreService.GetServer())) {
                return repo.Collection.ToList();
            }
        }

        public Teacher GetTeacher(ObjectId teacherId) {
            using (var repo = new MongoRepository<Teacher>(CoreService.GetServer())) {
                return repo.Collection.Single(x => x.Id == teacherId);
            }
        }

        public IEnumerable<Student> GetStudent() {
            using (var repo = new MongoRepository<Student>(CoreService.GetServer())) {
                return repo.Collection;
            }
        }

        public IEnumerable<TimeSlot> GetTimeSlots() {
            using (var repo = new MongoRepository<TimeSlot>(CoreService.GetServer())) {
                return repo.Collection;
            }
        }
        
        public IEnumerable<StudyCenter> GetStudyCenters() {
            using(var repo=new MongoRepository<StudyCenter>(CoreService.GetServer())) {
                return repo.Collection;
            }
        }
    }
}
