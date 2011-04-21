using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using StudentTracker.Models;
using StudentTracker.Repository.MongoDb;
using StudentTracker.Services.Core;

namespace StudentTracker.Services.Authentication {
    public class AuthenticationProvider : System.Web.Security.MembershipProvider {
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer) {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword) {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user) {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password) {
            using (var repo = new MongoRepository<User>(CoreService.GetServer())) {
                return repo.Collection.Count(x => x.Username == username && x.Password == password) > 0;
            }
        }

        public override bool UnlockUser(string userName) {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline) {
            using (var repo = new MongoRepository<User>(CoreService.GetServer())) {
                var user = repo.Collection.Single(x => x.Username == username);
                return new MembershipUser(this.Name, user.Name, user.Id, null, null, null, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now,
                    DateTime.Now);
            }
        }

        public override string GetUserNameByEmail(string email) {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline() {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override int MaxInvalidPasswordAttempts {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression {
            get { throw new NotImplementedException(); }
        }


    }
    public interface IFormsAuthenticationService {
        void LogOn(string userName, bool createPersistentCookie);
        void LogOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService {
        public void LogOn(string userName, bool createPersistentCookie) {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }


        public void LogOut() {
            FormsAuthentication.SignOut();
        }
    }


}