using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    public interface IComparable
    {
        int CompareTo(object obj);
    }
    /// <summary>
    /// Типичный курьер.
    /// </summary>
    internal abstract class Courier : IComparable
    {
        public int CompareTo(object obj)
        {
            Courier courier1 = obj as Courier;
            if (courier1 != null)
                return this.Profit.CompareTo(courier1.Profit);
            else
                throw new Exception("Не то...");
        }
        public int CourierID { get; set; }

        public string Name { get; set; }

        public Coord Start { get; set; }

        public double Capacity { get; set; }

        public double Speed { get; set; }

        public double Price { get; set; }

        public DateTime StartTime { get { return CourierScheduleStart(); } }

        public DateTime EndTime { get { return CourierScheduleEnd(); } }

        public int Profit { get; set; }

        public int NumberPrioritiCoord { get; set; }

        public List<Order> Orders { get; set; }

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
    }
}
