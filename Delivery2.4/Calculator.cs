using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Формулы.
    /// </summary>
    internal static class Calculator
    {
        /// <summary>
        /// Переводит дедлайн заказа в минуты.
        /// </summary>
        public static int TimeToMinute(string time)
        {
            int split = time.IndexOf(":");
            int hour = int.Parse(time.Substring(0, split));
            int minute = int.Parse(time.Substring(split + 1));
            int timeMinute = (hour * 60) + minute - 480;
            return timeMinute;
        }
        public static int TimeToWay(Order order, Courier courier)
        {
            int time = (int) Math.Round(CoordHelper.GetDistance(order.Start, courier.Start) / courier.Speed * 60);
            return time;
        }

        public static int TimeToCompliteOrder(Order order, Courier courier)
        {
            int time = (int)Math.Round((CoordHelper.GetDistance(order.Start, courier.Start) + order.Distance) / courier.Speed * 60);
            return time;
        }
    }
}
