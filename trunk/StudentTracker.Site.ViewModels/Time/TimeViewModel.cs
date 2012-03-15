using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTracker.Site.ViewModels.Time {
 public class TimeViewModel {
     public int Id { get; set; }
     public int Hour { get; set; }
     public int Minute { get; set; }
     public bool IsAm { get; set; }
 }
}
