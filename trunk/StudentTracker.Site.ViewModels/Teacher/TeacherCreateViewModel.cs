using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;

namespace StudentTracker.Site.ViewModels.Teacher {
    public class TeacherCreateViewModel {
        public Models.Teacher Teacher { get; set; }

        public Models.Teacher GetTeacher() {
            Teacher.Id = ObjectId.NewObjectId();
            return Teacher;
        }
    }
}
