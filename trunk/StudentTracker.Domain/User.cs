using Norm;

namespace StudentTracker.Domain {
    public class User {
        [MongoIdentifier]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public StudyCenter StudyCenter { get; set; }
    }
}