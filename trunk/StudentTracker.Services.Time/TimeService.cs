using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Models;

namespace StudentTracker.Services.Time {
    public class TimeService {
        public static int CompareTo(StudentTracker.Models.Time other1, StudentTracker.Models.Time other2) {

            int myHour = other2.IsAm ? other2.Hour : other2.Hour == 12 ? 12 : other2.Hour + 12;
            int otherHour = other1.IsAm ? other1.Hour : other1.Hour == 12 ? 12 : other1.Hour + 12;

            if (myHour < otherHour)
                return 1;

            if (myHour > otherHour)
                return -1;

            if (other2.Minute < other1.Minute)
                return 1;
            if (other2.Minute > other1.Minute)
                return -1;

            return 0;

        }

        public static decimal SubtractFrom(StudentTracker.Models.Time other1,StudentTracker.Models.Time other2) {
              int totalStartHours = 0;
              int totalOtherHours = 0;

              if (other2.IsAm && other2.Hour == 12)
                  totalStartHours = 0;

              if (other1.IsAm && other1.Hour == 12)
                  totalOtherHours = 0;

              if (!other2.IsAm && other2.Hour == 12)
                  totalStartHours = 12;

              if (!other1.IsAm && other1.Hour == 12)
                  totalOtherHours = 12;

              if (other2.IsAm && (other2.Hour != 12))
                  totalStartHours = other2.Hour;

              if (other1.IsAm && (other2.Hour != 12))
                  totalOtherHours = other1.Hour;

              if (!other2.IsAm && (other2.Hour != 12))
                  totalStartHours = 12 + other2.Hour;

              if (!other1.IsAm && (other1.Hour != 12))
                  totalOtherHours = 12 + other1.Hour;
              decimal duration = (decimal)((60 - other2.Minute) + (totalOtherHours - (totalStartHours + 1)) * 60 + other1.Minute) / 60;
              return duration;
          }
        /*
         public override string ToString() {
             return IsAm ? String.Format("{0:D2}:{1:D2} AM", Hour, Minute) : String.Format("{0:D2}:{1:D2} PM", Hour, Minute);
         }

        

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
     }*/
    }
}
