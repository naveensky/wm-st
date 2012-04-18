using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Resources;

namespace StudentTracker.Models {
    
    public class Topic : IEntity {
        [Key]
        public int Id { get; set; }
        public virtual Course Course { get; set; }
        public string Name { get; set; }
        //public virtual Appointment Appointment { get; set; }
    }
}
