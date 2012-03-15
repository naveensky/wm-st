using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Teacher {
    public class TeacherListViewModel {
        public IEnumerable<TeacherViewModel> Teachers { get; set; }
    }
}
