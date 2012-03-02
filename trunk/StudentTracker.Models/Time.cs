using System;
using System.ComponentModel.DataAnnotations;
using Norm;

namespace StudentTracker.Models {
    public class Time : IComparable<Time>,IEntity {
        [Key]
        public int Id { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public bool IsAm { get; set; }

        public int CompareTo(Time other) {

            int myHour = IsAm ? Hour : Hour == 12 ? 12 : Hour + 12;
            int otherHour = other.IsAm ? other.Hour : other.Hour == 12 ? 12 : other.Hour + 12;

            if (myHour < otherHour)
                return 1;

            if (myHour > otherHour)
                return -1;

            if (this.Minute < other.Minute)
                return 1;
            if (this.Minute > other.Minute)
                return -1;

            return 0;

        }

        public decimal SubtractFrom(Time other) {
            int totalStartHours = 0;
            int totalOtherHours = 0;

            if (this.IsAm && this.Hour == 12)
                totalStartHours = 0;

            if (other.IsAm && other.Hour == 12)
                totalOtherHours = 0;

            if (!this.IsAm && this.Hour == 12)
                totalStartHours = 12;

            if (!other.IsAm && other.Hour == 12)
                totalOtherHours = 12;

            if (this.IsAm && (this.Hour != 12))
                totalStartHours = this.Hour;

            if (other.IsAm && (this.Hour != 12))
                totalOtherHours = other.Hour;

            if (!this.IsAm && (this.Hour != 12))
                totalStartHours = 12 + this.Hour;

            if (!other.IsAm && (other.Hour != 12))
                totalOtherHours = 12 + other.Hour;
            decimal duration = (decimal)((60 - this.Minute) + (totalOtherHours - (totalStartHours + 1)) * 60 + other.Minute) / 60;
            return duration;
        }

        public override string ToString() {
            return IsAm ? String.Format("{0:D2}:{1:D2} AM", Hour, Minute) : String.Format("{0:D2}:{1:D2} PM", Hour, Minute);
        }

        public Time() { }

        public Time(string timeString) {
            IsAm = timeString.ToUpper().Contains("AM");
            var ss = timeString.ToUpper().Split(':');
            if (IsAm) {
                Hour = int.Parse(ss[0].ToString());
                Minute = int.Parse(ss[1].Replace("AM", String.Empty).ToString());
            } else {
                Hour = int.Parse(ss[0].ToString());
                Minute = int.Parse(ss[1].Replace("PM", String.Empty).ToString());
            }
        }


    }
}