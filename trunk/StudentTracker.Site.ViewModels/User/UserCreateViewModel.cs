using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;

namespace StudentTracker.Site.ViewModels.User {
  public class UserCreateViewModel {
        public Models.User User { get; set; }

        [Display(Name ="Study Center")]
        public int StudyCenterId { get; set; }
        public IEnumerable<Site.ViewModels.StudyCenter.StudyCenter> StudyCenters { get; set; }
    }
}
