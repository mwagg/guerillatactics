using System;

namespace MyFeed.Core.Domain.Model
{
    public class FeedUpdate
    {
        private readonly string _username;
        private readonly string _content;
        private readonly DateTime _publishDateTime;

        public FeedUpdate(string userName, string content, DateTime publishDateTime)
        {
            _username = userName;
            _content = content;
            _publishDateTime = publishDateTime;
        }

        public string Username
        {
            get { return _username; }
        }

        public bool Equals(FeedUpdate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._username, _username) && Equals(other._content, _content) && other._publishDateTime.Equals(_publishDateTime);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (FeedUpdate)) return false;
            return Equals((FeedUpdate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (_username != null ? _username.GetHashCode() : 0);
                result = (result*397) ^ (_content != null ? _content.GetHashCode() : 0);
                result = (result*397) ^ _publishDateTime.GetHashCode();
                return result;
            }
        }

        public string Content
        {
            get { return _content; }
        }

        public DateTime PublishDateTime
        {
            get { return _publishDateTime; }
        }
    }
}