using System;

namespace GuerillaTactics.Common.Utility
{
    public class DisposableAction : IDisposable
    {
        private readonly Action _disposeAction;
        private bool _isDisposed;

        public DisposableAction(Action initialAction, Action disposeAction) : this(disposeAction)
        {
            initialAction();
        }

        public DisposableAction(Action disposeAction)
        {
            _disposeAction = disposeAction;
        }

        public void Dispose()
        {
            if(_isDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            _isDisposed = true;
            _disposeAction();
        }
    }
}