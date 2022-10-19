using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Типичный заказ.
    /// </summary>
    internal abstract class Order
    {
        public Coord Start { get; set; }

        public Coord End { get; set; }

        public double Weigth { get; set; }

        public DateTime DeadLine { get; set; }

        public TimeSpan Time { get; set; }

        public Courier[] PriorityCouriers = new Courier[Company.quantityC];

        public double Distance { get { return Start.GetDistance(End); } }
        
        public int Coast { get { return GetOrderPrice(); } }

        /// <summary>
        /// Считает стоимость заказа.
        /// </summary>
        protected int  GetOrderPrice()
        {
            return (int)Math.Round(Distance * Company.PricePerDistance);
        }
    }
}
