using Prism.Events;

namespace SEFLink.UI.Events
{
    public class NewViewSelectedEvent : PubSubEvent<NewViewSelectedEventArgs> { }

    public class NewViewSelectedEventArgs
    {
        public NewViewSelectedEventArgs() { }

        public NewViewSelectedEventArgs(int minWidth, int minHeight = -1)
        {
            MinWidth = minWidth;
            MinHeight = minHeight;
        }

        public int MinHeight { get; set; }
        public int MinWidth { get; set; }
    }
}
