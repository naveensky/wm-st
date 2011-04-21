using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norm;
using StudentTracker.Domain;
using StudentTracker.Repository.MongoDb;
using StudentTracker.Repository.Testing;
using StudentTracker.Service.Abstract;
using StudentTracker.Service.Concrete;
using StudentTracker.Tests.Helpers;

namespace StudentTracker.Tests {

    [TestClass]
    public class UserServiceTest {

        [TestMethod]
        public void Can_Add_User() {
            var user = UnitTestHelpers.MockUser();
            var svc = UnitTestHelpers.CreateUserService(new FakeRepository<User>());
            svc.AddUser(user);
            svc.Users.Count().ShouldEqual(1);
            svc.Users.First().Username.ShouldEqual(user.Username);
            svc.Users.First().StudyCenter.ShouldEqual(user.StudyCenter);
        }

        public void Can_Delete_User() {
            //var user = UnitTestHelpers.MockUser();
            //var svc = UnitTestHelpers.CreateUserService(unit)
        }

        [TestMethod]
        public void Can_Validate_User() {
            string username = "username";
            string password = "password";
            IUserService svc = UnitTestHelpers.CreateUserService(UnitTestHelpers.MockupRepository<User>(new User {
                Username = username,
                Password = password
            }));
            svc.ValidateUser(username, password).ShouldEqual(true);
        }

        [TestMethod]
        public void Can_InValidate_User() {
            string username = "username";
            string password = "password";
            string wrongPassword = "wrongPassword";
            IUserService svc = UnitTestHelpers.CreateUserService(UnitTestHelpers.MockupRepository<User>(new User {
                Username = username,
                Password = password
            }));
            svc.ValidateUser(username, wrongPassword).ShouldEqual(false);
        }
    }
}
