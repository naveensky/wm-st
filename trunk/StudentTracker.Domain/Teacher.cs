using System.ComponentModel.DataAnnotations;
using Norm;

namespace StudentTracker.Domain {
    public class Teacher {

        [MongoIdentifier]
        public ObjectId Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Mobile { get; set; }
    }
}
