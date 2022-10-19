using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Заказ на взятие посылки.
    /// </summary>
    internal class OrderForTaking : Order
    {
        public OrderForTaking(Coord start, Coord end, string deadline, double weigth)
        {
            Start = start;
            End = end;
            Weigth = weigth;
            DeadLine = DateTime.Today + TimeCalculator.TimeToMinute(deadline);
        }
    }
}
