using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository.MongoDb;
using StudentTracker.Services.Core;


namespace StudentTracker.Services.User {
    public class UserService {
        public void SaveUser(Models.User user, ObjectId studyCenterId) {
            using (var userRepo = new MongoRepository<Models.User>(CoreService.GetServer())) {
                using (var studyCenterRepo = new MongoRepository<StudyCenter>(CoreService.GetServer())) {
                    user.StudyCenter = studyCenterRepo.Collection.Single(x => x.Id == studyCenterId);
                    userRepo.Save(user);
                }
            }
        }


    }
}
