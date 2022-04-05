using System;

namespace iPhoto.Commands
{
    public class InvokeEventCommand : CommandBase
    {
        private EventHandler _eventHandler;
        public InvokeEventCommand(EventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }
        public override void Execute(object parameter)
        {
            _eventHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
