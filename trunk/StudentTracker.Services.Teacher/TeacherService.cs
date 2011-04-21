using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Repository.MongoDb;
using StudentTracker.Services.Core;

namespace StudentTracker.Services.Teacher {
    public class TeacherService {

        public void AddTeacher(Models.Teacher teacher) {
            using (var repo = new MongoRepository<Models.Teacher>(CoreService.GetServer())) {
                repo.Save(teacher);
            }
        }
    }
}
