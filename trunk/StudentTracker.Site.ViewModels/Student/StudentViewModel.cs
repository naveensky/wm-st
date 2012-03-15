using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudentTracker.Models;

namespace StudentTracker.Site.ViewModels.Student {
    public class StudentViewModel {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Roll No.")]
        public string Roll { get; set; }
        public DateTime RegisterDate { get; set; }

        public bool SoftwareGiven { get; set; }
        public string Mobile { get; set; }
        public bool BooksGiven { get; set; }
        public string Email { get; set; }
        public DateTime? ProposedExamDate { get; set; }
        public DateTime? ActualExamDate { get; set; }
        public double Score { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDownloaded { get; set; }
        public string WmPrepUsername { get; set; }
        public virtual Common.StudyCenterViewModel StudyCenter { get; set; }
        public ViewModels.Common.CourseViewModel Course { get; set; }
    }
}
