using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Student {
    public class StudentDownloadViewModel {

        public StudentDownloadViewModel() {
            StartDate = DateTime.Now.Date.AddDays(-15);
            EndDate = DateTime.Now.Date;
        }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Download Previous Leads")]
        public bool DownloadAll { get; set; }
    }
}
