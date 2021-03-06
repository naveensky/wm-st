﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Norm;

namespace StudentTracker.Models {
    public class Student : IEntity {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Roll No.")]
        public string Roll { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Registration Date")]
        public DateTime RegisterDate { get; set; }

        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage = "Enter a Valid Email")]
        public string Email { get; set; }

        [Display(Name = "Software Provided")]
        public bool SoftwareGiven { get; set; }

        [Display(Name = "WMPrep Username")]
        public string WmPrepUsername { get; set; }

        [Display(Name = "Books Provided")]
        public bool BooksGiven { get; set; }

        [Required]
        [Display(Name = "Proposed Exam Date")]
        public DateTime? ProposedExamDate { get; set; }

        [Display(Name = "Actual Exam Date")]
        public DateTime? ActualExamDate { get; set; }

        [Display(Name = "Study Center")]
        public virtual StudyCenter StudyCenter { get; set; }

        [Display(Name = "Score")]
        public double Score { get; set; }

        [Display(Name = "Already Downloaded")]
        public bool IsDownloaded { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        [Display(Name = "Mobile / Other Contact Number")]
        public string Mobile { get; set; }
        public virtual Course Course { get; set; }

        public decimal? AmountPaid { get; set; }
        public decimal? AmountPending { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Appointment> Missed { get; set; } 


    }
}
