using Prism.Events;
using SEFLink.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEFLink.UI.Events
{
    public class FilterEvent : PubSubEvent<FilterEventArgs>
    {
    }

    public class FilterEventArgs
    {
        public bool ResetFilter { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public DocType? Type{ get; set; }
        public string SearchTerm { get; set; }
    }
}
