using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Load {
    public class DataViewModel {
        
        [Required]
        public string Password { get; set; }
    }
}
