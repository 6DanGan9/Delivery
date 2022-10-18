using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// 
    /// </summary>
    internal abstract class Order
    {
        public Coord Start { get; set; }

        public Coord End { get; set; }

        public double Weigth { get; set; }

        public int DeadLine { get; set; }

        public int Time { get; set; }

        public Courier[] PriorityCouriers = new Courier[Company.quantityC];

        public double OrderDistance
        {
            get { return Start.GetDistance(End); }
        }

        public double OrderPrice { get { return GetOrderPrice(); } }
        /// <summary>
        /// Считает стоимость заказа.
        /// </summary>
        protected double GetOrderPrice()
        {
            return OrderDistance * Company.PricePerDistance;
        }
        /// <summary>
        /// Переводит дедлайн заказа в минуты.
        /// </summary>
        protected static int TimeToMinute(string time)
        {
            int split = time.IndexOf(":");
            int hour = int.Parse(time.Substring(0, split));
            int minute = int.Parse(time.Substring(split + 1));
            int timeMinute = (hour * 60) + minute - 480;
            return timeMinute;
        }
    }
}
