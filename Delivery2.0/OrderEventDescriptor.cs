using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    /// <summary>
    /// Описатель события заказа
    /// </summary>
    internal class OrderEventDescriptor : EventArgs
    {
        public Order Order { get; set; }
    }
}
