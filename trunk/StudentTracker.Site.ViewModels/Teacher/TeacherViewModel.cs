using System.ComponentModel.DataAnnotations;
using StudentTracker.Models;

namespace StudentTracker.Site.ViewModels.Teacher {
    public class TeacherViewModel {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Mobile { get; set; }
    }
}
