using System;
using Norm;

namespace StudentTracker.Domain {
    public class Appointment {

        [MongoIdentifier]
        public ObjectId Id { get; set; }
        public DateTime Date { get; set; }
        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
        public float Duration { get; set; }
        public AppointmentType AppointmentType { get; set; }
    }
}