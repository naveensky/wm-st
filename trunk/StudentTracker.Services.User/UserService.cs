using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
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
            user.StudyCenter = _uow.StudyCenters.Single(x => x.Id == studyCenterId);
            _uow.Users.Add(user);
            _uow.Users.SaveChanges();
        }

        public Models.User GetCurrentUser() {
            var userId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            var user = _uow.Users.Single(x => x.UserId == userId, x => x.StudyCenter);
            return user;
        }

        public StudyCenter GetUserCenter() {
            return GetCurrentUser().StudyCenter;
        }
    }
}
