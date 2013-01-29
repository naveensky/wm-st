using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Student {
    public class PaymentModel {
        public int StudentId { get; set; }
        [Display(Name = "Amount Paid")]
        public decimal? AmountPaid { get; set; }
        [Display(Name = "Amount Pending")]
        public decimal? AmountPending { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime? PaymentDate { get; set; }
    }
}
