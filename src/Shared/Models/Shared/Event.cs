using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Shared
{
    internal class Event
    {
        public string EventId { get; set; }
        public EventHeader EventHeader { get; set; }
    }

    internal class EventHeader
    {
        public DateTime EventTime { get; set; }
        public string SAOHref { get; set; }
        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
    }

    internal class Team
    {
        public string Name { get; set; }
        public string Abbr { get; set; }
        public bool IsHome { get; set; }
        public bool IsAway { get; set; }
        public bool IsNeutral { get; set; }
    }
}
