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

        public TimeSpan DeadLine { get; set; }

        public int Time { get; set; }

        public Courier[] PriorityCouriers = new Courier[Company.quantityC];

        public double Distance { get { return Start.GetDistance(End); } }
        
        public double Coast { get { return GetOrderPrice(); } }

        /// <summary>
        /// Считает стоимость заказа.
        /// </summary>
        protected double GetOrderPrice()
        {
            return Distance * Company.PricePerDistance;
        }
    }
}
