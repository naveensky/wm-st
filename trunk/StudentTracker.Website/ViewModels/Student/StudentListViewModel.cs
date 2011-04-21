using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentTracker.Website.Helpers;

namespace StudentTracker.Website.ViewModels.Student {
    public class StudentListViewModel {
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Domain.Student> Students { get; set; }
    }
}