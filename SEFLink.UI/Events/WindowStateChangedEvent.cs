using Prism.Events;
using System.Windows;

namespace SEFLink.UI.Events
{
    public class WindowStateChangedEvent : PubSubEvent<WindowStateChangedEventArgs> {}

    public class WindowStateChangedEventArgs
    {
        public WindowStateChangedEventArgs(WindowState newState)
        {
            NewState = newState;
        }

        public WindowState NewState { get; set; }
    }
}
