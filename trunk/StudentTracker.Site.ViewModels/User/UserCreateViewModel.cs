using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Site.ViewModels.Common;

namespace StudentTracker.Site.ViewModels.User {
  public class UserCreateViewModel {
        public UserViewModel User { get; set; }

        [Display(Name ="Study Center")]
        public int StudyCenterId { get; set; }
        public IEnumerable<StudyCenterViewModel> StudyCenters { get; set; }
    }
}
