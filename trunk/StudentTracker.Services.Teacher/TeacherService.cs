﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Repository;
//using StudentTracker.Repository.MongoDb;
using StudentTracker.Services.Core;

namespace StudentTracker.Services.Teacher {
    public class TeacherService {
         private readonly ISqlUnitOfWork _uow;

        public TeacherService(ISqlUnitOfWork uow) {
            _uow = uow;
        }
        public void AddTeacher(Models.Teacher teacher) {
          //  using (var repo = new MongoRepository<Models.Teacher>(CoreService.GetServer())) {
                _uow.Teachers.Add(teacher);
            //}
        }
    }
}
