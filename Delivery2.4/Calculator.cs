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
        public static TimeSpan TimeToMinute(string time)
        {
            int split = time.IndexOf(":");
            int hour = int.Parse(time.Substring(0, split));
            int minute = int.Parse(time.Substring(split + 1));
            TimeSpan timeMinute = TimeSpan.FromMinutes((hour * 60) + minute);
            return timeMinute;
        }
        public static TimeSpan TimeToWay(Order order, Courier courier)
        {
            TimeSpan time = TimeSpan.FromMinutes((int) Math.Round(CoordHelper.GetDistance(order.Start, courier.Start) / courier.Speed * 60));
            return time;
        }

        public static TimeSpan TimeToCompliteOrder(Order order, Courier courier)
        {
            TimeSpan time = TimeSpan.FromMinutes((int)Math.Round((CoordHelper.GetDistance(order.Start, courier.Start) + order.Distance) / courier.Speed * 60));
            return time;
        }
    }
}
