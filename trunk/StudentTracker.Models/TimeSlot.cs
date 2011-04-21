using Norm;

namespace StudentTracker.Models {
    public class TimeSlot {
        public ObjectId Id { get; set; }
        public string Display { get; set; }
        public int Order { get; set; }
    }
}