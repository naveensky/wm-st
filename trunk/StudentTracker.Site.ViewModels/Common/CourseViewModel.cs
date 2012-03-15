using System.Collections.Generic;

namespace StudentTracker.Site.ViewModels.Common {
    public class CourseViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TopicViewModel> Topics { get; set; }
    }
}
