using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Models;

namespace StudentTracker.Repository {
    public interface ISqlUnitOfWork {
        IRepository<Student> Students { get; set; }
        IRepository<Teacher> Teachers { get; set; }
    }
}
