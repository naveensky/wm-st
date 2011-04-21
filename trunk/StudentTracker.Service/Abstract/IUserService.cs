using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Domain;
using StudentTracker.Repository;

namespace StudentTracker.Service.Abstract {
    public interface IUserService {
        IQueryable<User> Users { get; }
        bool ValidateUser(string username, string password);
        void AddUser(User user);
    }
}
