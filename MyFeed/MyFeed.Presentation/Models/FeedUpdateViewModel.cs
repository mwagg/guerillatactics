using Castle.Components.Validator;

namespace MyFeed.Presentation.Models
{
    public class FeedUpdateViewModel
    {
        [ValidateNonEmpty("Please enter your update.")]
        [ValidateLength(1, 140, "Your update must be 140 characters or less")]
        public string Content { get; set; }
    }
}