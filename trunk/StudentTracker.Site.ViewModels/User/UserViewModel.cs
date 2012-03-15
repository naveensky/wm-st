using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Site.ViewModels.Common;

namespace StudentTracker.Site.ViewModels.User {
  public class UserViewModel {
      public int Id { get; set; }

      public string UserId { get; set; }
      public string Name { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }
      public bool IsAdmin { get; set; }
      public virtual StudyCenterViewModel StudyCenter { get; set; }
    }
}
