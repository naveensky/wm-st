﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Security;
using Norm;

namespace StudentTracker.Models {
    public class User:IEntity {
        [Key]
        public int Id { get; set; }
        
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public virtual StudyCenter StudyCenter { get; set; }

    }
}