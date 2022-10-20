using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Типичный курьер.
    /// </summary>
    internal abstract class Courier
    {
        public int CourierID { get; protected set; }

        public string Name { get; protected set; }

        public Coord Start { get; protected set; }

        public double Capacity { get; protected set; }

        public double Speed { get; protected set; }

        public double Price { get; protected set; }

        public DateTime StartTime { get { return CourierScheduleStart(); } }

        public DateTime EndTime { get { return CourierScheduleEnd(); } }

        public List<Order> Orders = new();

        public TimeSpan BusyTime { get { return CalculateBusyTime(); } }
        /// <summary>
        /// Считает суммарное время, которое курьер потратит на выполнение всех заказов в его списке заказов.
        /// </summary>
        protected TimeSpan CalculateBusyTime()
        {
            TimeSpan time = TimeSpan.FromMinutes(0);
            if ((Orders != null) && (Orders.Count > 0))
            {
                foreach (Order order in Orders)
                {
                    time += order.Time;
                }
                return time;
            }
            else
            {
                return time;
            }
        }
        /// <summary>
        /// Задаёт время начала работы курьера.
        /// </summary>
        protected DateTime CourierScheduleStart()
        {
            DateTime dateTime = DateTime.Today;
            switch (CourierID % 3)
            {
                case 0:
                    TimeSpan time1 = TimeSpan.FromHours(8);
                    dateTime += time1;
                    break;
                case 1:
                    TimeSpan time2 = TimeSpan.FromHours(12);
                    dateTime += time2;
                    break;
                case 2:
                    TimeSpan time3 = TimeSpan.FromHours(16);
                    dateTime += time3;
                    break;
            }
            return dateTime;
        }
        /// <summary>
        /// Задаёт время конца работы курьера.
        /// </summary>
        protected DateTime CourierScheduleEnd()
        {
            DateTime dateTime = DateTime.Today;
            switch (CourierID % 3)
            {
                case 0:
                    TimeSpan time1 = TimeSpan.FromHours(16);
                    dateTime += time1;
                    break;
                case 1:
                    TimeSpan time2 = TimeSpan.FromHours(20);
                    dateTime += time2;
                    break;
                case 2:
                    TimeSpan time3 = TimeSpan.FromHours(24);
                    dateTime += time3;
                    break;
            }
            return dateTime;
        }
        /// <summary>
        /// Прикрепляет заказ к курьеру, если это не возможно возвращает false.
        /// </summary>
        public bool AttachingOrder(Order order, int numberPriorityCoord, int profit)
        {
            //Если заказ хочет прикрепиться в конец, то он прикрепляется.
            if (numberPriorityCoord == Orders.Count)
            {
                if (numberPriorityCoord == 0)
                {
                    order.Time = TimeCalculator.TimeToCompliteOrder(order, this);
                }
                else
                {
                    order.Time = TimeCalculator.TimeToCompliteOrder(order, this, Orders[^1].End);
                }
                order.Couriers.Clear();
                Orders.Add(order);
                return true;
            }
            //Если заказ менее выгоден, нежели заказ, вместо которого он хочет встать, то курьер от него отказывается(возвращается false).
            if (profit <= Orders[numberPriorityCoord].Profit)
            {
                return false;
            }
            //Все заказы начиная с того, вместо которого хочет встать заказ, переходят в список свободных заказов, а заказ встаёт на их место.
            int quantityOrders = (Orders.Count - numberPriorityCoord);
            for (int i = 0; i < quantityOrders; i++)
            {
                Company.FreeOrders.Add(Orders[^1]);
                Orders.RemoveAt(Orders.Count - 1);
            }
            //Заказу присваиваются его время на выполнение.
            if (numberPriorityCoord == 0)
            {
                order.Time = TimeCalculator.TimeToCompliteOrder(order, this);
            }
            else
            {
                order.Time = TimeCalculator.TimeToCompliteOrder(order, this, Orders[^1].End);
            }
            order.Couriers.Clear();
            Orders.Add(order);
            return true;
        }
    }
}
