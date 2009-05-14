#region licence

// * Copyright (c) 2009, Michael Wagg
// * All rights reserved.
// *
// * Redistribution and use in source and binary forms, with or without
// * modification, are permitted provided that the following conditions are met:
// *     * Redistributions of source code must retain the above copyright
// *       notice, this list of conditions and the following disclaimer.
// *     * Redistributions in binary form must reproduce the above copyright
// *       notice, this list of conditions and the following disclaimer in the
// *       documentation and/or other materials provided with the distribution.
// *     * Neither the name Michael Wagg nor the
// *       names of its contributors may be used to endorse or promote products
// *       derived from this software without specific prior written permission.
// *
// * THIS SOFTWARE IS PROVIDED BY Michael Wagg ''AS IS'' AND ANY
// * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// * DISCLAIMED. IN NO EVENT SHALL Michael Wagg BE LIABLE FOR ANY
// * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections;
using System.Reflection;
using GuerillaTactics.Common.Eventing;
using GuerillaTactics.Testing;
using NUnit.Framework;
using Rhino.Mocks;

namespace Specs_for_EventHub
{
    public abstract class EventHubSpecification : Specification<EventHub>
    {
        protected Message Message { get; private set; }
        protected IMessageSubscriber<Message> SubscriberA { get; private set; }
        protected IMessageSubscriber<Message> SubscriberB { get; private set; }

        protected override EventHub CreateSubject()
        {
            return new EventHub();
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();

            Message = new Message();
            SubscriberA = MockRepository.GenerateStub<IMessageSubscriber<Message>>();
            SubscriberB = MockRepository.GenerateStub<IMessageSubscriber<Message>>();
        }
    }

    [TestFixture]
    public class when_a_message_is_published_and_an_interested_subscriber_is_registered : EventHubSpecification
    {
        protected override void When()
        {
            Subject.Register(SubscriberA);
            Subject.Publish(Message);
        }

        [Test]
        public void the_subscriber_should_recieve_the_published_message()
        {
            SubscriberA.AssertWasCalled(s => s.HandleMessage(Message));
        }
    }

    [TestFixture]
    public class when_a_subscriber_unsubscribes_and_a_message_is_published_which_the_subscriber_was_interested_in :
        EventHubSpecification
    {
        protected override void When()
        {
            Subject.Register(SubscriberA);
            Subject.Unregister(SubscriberA);
            Subject.Publish(Message);
        }

        [Test]
        public void the_subscriber_should_not_recieve_the_published_message()
        {
            SubscriberA.AssertWasNotCalled(s => s.HandleMessage(Message));
        }
    }

    [TestFixture]
    public class when_multiple_subscribers_are_registered_and_a_message_is_published : EventHubSpecification
    {
        protected override void When()
        {
            Subject.Register(SubscriberA);
            Subject.Register(SubscriberB);
            Subject.Publish(Message);
        }

        [Test]
        public void all_interested_subscribers_should_recieve_the_message()
        {
            SubscriberA.AssertWasCalled(s => s.HandleMessage(Message));
            SubscriberB.AssertWasCalled(s => s.HandleMessage(Message));
        }
    }

    [TestFixture]
    public class when_the_event_hub_has_been_disposed : EventHubSpecification
    {
        protected override void When()
        {
            Subject.Dispose();
        }

        [Test]
        public void calling_dispose_again_should_throw_an_exception()
        {
            Assert.Throws<ObjectDisposedException>(() => Subject.Dispose());
        }

        [Test]
        public void trying_to_publish_a_message_should_throw_an_exception()
        {
            Assert.Throws<ObjectDisposedException>(() => Subject.Publish(Message));
        }

        [Test]
        public void trying_to_register_with_the_event_hub_should_throw_an_exception()
        {
            Assert.Throws<ObjectDisposedException>(() => Subject.Register(SubscriberA));
        }

        [Test]
        public void trying_to_unregister_should_throw_an_exception()
        {
            Assert.Throws<ObjectDisposedException>(() => Subject.Unregister(SubscriberA));
        }
    }

    [TestFixture]
    public class when_the_event_hub_has_subscribers_registered_and_is_disposed : EventHubSpecification
    {
        protected override void When()
        {
            Subject.Register(SubscriberA);
            Subject.Register(SubscriberB);
            Subject.Dispose();
        }

        [Test]
        public void all_subscribers_are_unregistered()
        {
            // its not nice, but given we could have a potential memory leak
            // we are using reflection in this test to check that all references to subscribers
            // are released by the event hub
            FieldInfo subscribersField = typeof (EventHub).GetField("_subscribers",
                                                                    BindingFlags.Instance | BindingFlags.NonPublic);
            var subscribers = (ArrayList) subscribersField.GetValue(Subject);

            Assert.That(subscribers, Is.Empty);
        }
    }

    public class Message
    {
    }
}