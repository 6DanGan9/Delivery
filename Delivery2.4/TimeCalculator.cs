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
    internal static class TimeCalculator
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
        /// <summary>
        /// Считает, сколько времени курьер потратит на путь до начальной координаты заказа.
        /// </summary>
        public static TimeSpan TimeToWay(Order order, Courier courier)
        {
            TimeSpan time = TimeSpan.FromMinutes((int) Math.Round(CoordHelper.GetDistance(order.Start, courier.Start) / courier.Speed * 60));
            return time;
        }
        /// <summary>
        /// Считает, сколько времени курьер потратит на полное выполнение заказа с начальной координаты.
        /// </summary>
        public static TimeSpan TimeToCompliteOrder(Order order, Courier courier)
        {
            TimeSpan time = TimeSpan.FromMinutes((int)Math.Round((CoordHelper.GetDistance(order.Start, courier.Start) + order.Distance) / courier.Speed * 60));
            return time;
        }
        /// <summary>
        /// Считает, сколько времени курьер потратит на полное выполнение заказа с заданной коодринаты.
        /// </summary>
        public static TimeSpan TimeToCompliteOrder(Order order, Courier courier , Coord start)
        {
            TimeSpan time = TimeSpan.FromMinutes((int)Math.Round((CoordHelper.GetDistance(order.Start, start) + order.Distance) / courier.Speed * 60));
            return time;
        }
    }
}
