using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal abstract class Courier
    {

        public string Name { get; set; }

        public Coord Start { get; set; }

        public double Capacity { get; set; }

        public double Speed { get; set; }

        public double Price { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        public int Profit { get; set; }

        public List<Order> Orders { get; set; }

        public int BusyTime { get { return CalculateBusyTime(); } }
        /// <summary>
        /// Считает суммарное время, которое курьер потратит на выполнение всех заказов в его списке заказов.
        /// </summary>
        protected  int CalculateBusyTime()
        {
            if ((Orders != null) && (Orders.Count > 0))
            {
                int time = Orders.Sum(x => x.Time);
                return time;
            }
            else
            {
                return 0;
            }
            
        }
    }
}
