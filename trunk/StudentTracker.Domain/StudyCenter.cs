using Norm;

namespace StudentTracker.Domain {
    public class StudyCenter {
        [MongoIdentifier]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
