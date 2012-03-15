using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Repository;
//using StudentTracker.Repository.MongoDb;
using StudentTracker.Services.Core;
using StudentTracker.Models;
namespace StudentTracker.Services.Teacher {
    public class TeacherService {
        private readonly ISqlUnitOfWork _uow;

        public TeacherService(ISqlUnitOfWork uow) {
            _uow = uow;
        }
        public void AddTeacher(Models.Teacher teacher) {
            //  using (var repo = new MongoRepository<Models.Teacher>(CoreService.GetServer())) {
            _uow.Teachers.Add(teacher);
            _uow.Teachers.SaveChanges();
            //}
        }

        public Models.Teacher GetTeacher(int teacherId) {
            return (_uow.Teachers.FindById(teacherId));
        }

        public IDictionary<int, string> GetTeachersDictionary() {
            return (_uow.Teachers.Fetch().ToDictionary(x => x.Id, x => x.Name));
        }
    }
}
