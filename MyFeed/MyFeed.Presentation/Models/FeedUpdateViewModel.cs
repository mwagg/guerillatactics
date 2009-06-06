using System;
using MyFeed.Core.Domain.Model;

namespace MyFeed.Presentation.Models
{
    public class FeedUpdateViewModel
    {
        private readonly FeedUpdate _feedUpdate;

        public FeedUpdateViewModel(FeedUpdate feedUpdate)
        {
            if (feedUpdate == null) throw new ArgumentNullException("feedUpdate");
            _feedUpdate = feedUpdate;
        }

        public string Content
        {
            get
            {
                return _feedUpdate.Content;
            }
        }

        public string Username
        {
            get { return _feedUpdate.Username; }
        }

        public string PublishedDate
        {
            get
            {
                return _feedUpdate.PublishDateTime.ToString("f");
            }
        }
    }
}