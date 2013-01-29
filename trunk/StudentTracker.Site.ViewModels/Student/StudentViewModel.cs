using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudentTracker.Models;

namespace StudentTracker.Site.ViewModels.Student {
    public class StudentViewModel {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Student")]
        public string Name { get; set; }
        [Display(Name = "Roll No.")]
        public string Roll { get; set; }
        public DateTime RegisterDate { get; set; }

        public bool SoftwareGiven { get; set; }
        [Required]
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
        public Common.CourseViewModel Course { get; set; }
        public int AppointmentCounts { get; set; }
        [Display(Name = "Amount Paid")]
        public decimal? AmountPaid { get; set; }
        [Display(Name = "Amount Pending")]
        public decimal? AmountPending { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime? PaymentDate { get; set; }
        public string Remarks { get; set; }
    }
}
