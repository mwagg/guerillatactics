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

namespace GuerillaTactics.Common.Eventing
{
    public class EventHub : IEventHub, IDisposable
    {
        private readonly ArrayList _subscribers = new ArrayList();
        private bool _isDisposed;

        public void Register(object subscriber)
        {
            ThrowIfDisposed();

            _subscribers.Add(subscriber);
        }

        public void Publish<T>(T message)
        {
            ThrowIfDisposed();

            foreach (var subscriber in _subscribers)
            {
                var interestedSubscriber = subscriber as IMessageSubscriber<T>;
                if (interestedSubscriber != null)
                {
                    interestedSubscriber.HandleMessage(message);
                }
            }
        }

        public void Unregister(object subscriber)
        {
            ThrowIfDisposed();

            _subscribers.Remove(subscriber);
        }

        public void Dispose()
        {
            ThrowIfDisposed();

            _isDisposed = true;

           _subscribers.Clear();
        }

        private void ThrowIfDisposed()
        {
            if(_isDisposed)
            {
                throw new ObjectDisposedException("EventHub");
            }
        }
    }
}