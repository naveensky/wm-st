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
    }
}
