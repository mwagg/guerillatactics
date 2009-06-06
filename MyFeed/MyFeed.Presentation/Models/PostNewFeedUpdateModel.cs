using System;
using Castle.Components.Validator;

namespace MyFeed.Presentation.Models
{
    public class PostNewFeedUpdateModel
    {
        public string Username { get; set; }

        [ValidateNonEmpty("Please enter the text of your update.")]
        [ValidateLength(0, 140, "Your update cannot be greater than 140 characters in length.")]
        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}