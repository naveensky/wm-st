using System.ComponentModel.DataAnnotations;
using Norm;

namespace StudentTracker.Models {
    public class TimeSlot {
        [Key]
        public int Id { get; set; }
        public string Display { get; set; }
        public int Order { get; set; }
    }
}