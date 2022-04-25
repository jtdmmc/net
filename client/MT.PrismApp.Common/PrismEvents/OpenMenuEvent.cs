using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.PrismApp.Common.PrismEvents
{
    public class OpenMenuEvent : PubSubEvent<OpenMenuEventParm>
    {

    }

    public class OpenMenuEventParm
    {
        public string RegionName { get; set; }

        public override string ToString()
        {
            return "RegionName:" + RegionName;
        }
    }
}
