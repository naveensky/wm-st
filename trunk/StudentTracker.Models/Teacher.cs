﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;

namespace StudentTracker.Models {
    public class Teacher :IEntity{

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Mobile { get; set; }

        
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
