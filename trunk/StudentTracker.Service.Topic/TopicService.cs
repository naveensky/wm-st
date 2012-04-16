using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTracker.Repository;
namespace StudentTracker.Service.Topic {
    public class TopicService {
         private readonly ISqlUnitOfWork _uow;

        public TopicService(ISqlUnitOfWork uow) {
            _uow = uow;
        }

        public IEnumerable<Models.Topic> GetTopics() {
            return _uow.Topics.Fetch();
        }

        public ICollection<Models.Topic> GetTopicsByCourse(int id) {
          return  _uow.Topics.Fetch().Where(x => x.Course.Id == id).ToList();
        }
    }
}
