using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    internal class CourierEventDescriptor : EventArgs
    {
        public Courier Courier { get; set; }
    }
}
