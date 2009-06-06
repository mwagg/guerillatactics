using System;
using System.Collections.Generic;
using MyFeed.Core.Domain.Model;

namespace specs_for_FeedController
{
    public static class FeedUpdatesObjectMother
    {
        public static IEnumerable<FeedUpdate> FeedsForMike
        {
            get
            {
                return new List<FeedUpdate>
                           {
                               new FeedUpdate("mike", "here is a feed update", new DateTime(2009, 01, 01)),
                               new FeedUpdate("mike", "here is a feed update again", new DateTime(2009, 01, 02)),
                               new FeedUpdate("mike", "here is a feed update again", new DateTime(2009, 02, 02))
                           };
            }
        }

        public static IEnumerable<FeedUpdate> FeedsForJohn
        {
            get
            {
                return new List<FeedUpdate>
                           {
                               new FeedUpdate("john", "here is a feed update", new DateTime(2009, 01, 01)),
                               new FeedUpdate("john", "here is a feed update again", new DateTime(2009, 01, 02)),
                               new FeedUpdate("john", "here is a feed update again", new DateTime(2009, 02, 02))
                           };
            }
        }
    }
}