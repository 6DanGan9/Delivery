using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal class OrderForTaking : Order
    {
        public OrderForTaking(Coord start, Coord end, string deadline, double weigth)
        {
            Start = start;
            End = end;
            Weigth = weigth;
            DeadLine = Calculator.TimeToMinute(deadline);
        }
    }
}
