using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
//using StudentTracker.Repository.MongoDb;
using StudentTracker.Repository;
using StudentTracker.Services.Core;


namespace StudentTracker.Services.User {
    public class UserService {
         private readonly ISqlUnitOfWork _uow;

        public UserService(ISqlUnitOfWork uow) {
            _uow = uow;
        }
        public void SaveUser(Models.User user, int studyCenterId) {
           // using (var userRepo = new MongoRepository<Models.User>(CoreService.GetServer())) {
             //   using (var studyCenterRepo = new MongoRepository<StudyCenter>(CoreService.GetServer())) {
                    user.StudyCenter = _uow.StudyCenters.Single(x => x.Id == studyCenterId);
                    _uow.Users.Add(user);
               // }
            //}
        }


    }
}
