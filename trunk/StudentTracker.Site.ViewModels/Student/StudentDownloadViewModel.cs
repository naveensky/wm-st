using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Student {
    public class StudentDownloadViewModel {

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public bool DownloadAll { get; set; }
    }
}
