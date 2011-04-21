using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Domain;
using StudentTracker.Repository;
using StudentTracker.Service.Abstract;

namespace StudentTracker.Service.Concrete {
    public class UserService : IUserService {

        private readonly IRepository<User> _userRepo;

        public UserService(IRepository<User> userRepo) {
            _userRepo = userRepo;
        }

        public IQueryable<User> Users {
            get { return _userRepo.Collection; }
        }

        public bool ValidateUser(string username, string password) {
            //TODO: Make password hash-safe
            return _userRepo.Collection.Single(x => x.Username == username).Password == password;
        }

        public void AddUser(User user) {
            _userRepo.Add(user);
        }
    }
}
