using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentTracker.Domain;
using StudentTracker.Repository;
using StudentTracker.Service.Abstract;
using StudentTracker.Service.Concrete;

namespace StudentTracker.Tests.Helpers {
    public static class UnitTestHelpers {
        public static void ShouldEqual<T>(this T actualValue, T expectedValue) {
            Assert.AreEqual(expectedValue, actualValue);
        }

        public static IRepository<T> MockupRepository<T>(params T[] users) where T : class {
            var mockupRepo = new Mock<IRepository<T>>();
            mockupRepo.Setup(x => x.Collection).Returns(users.AsQueryable());
            return mockupRepo.Object;
        }

        public static User MockUser() {
            var mock = new Mock<User>();
            mock.SetupAllProperties();
            return mock.Object;
        }

        //TODO : Not liking this thing. Need to resolve this
        public static IUserService CreateUserService(IRepository<User> userRepo) {
            var svc = new UserService(userRepo);
            return svc;
        }
    }
}
