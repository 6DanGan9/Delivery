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

        /// <summary>
        /// Считает суммарное время, которое курьер потратит на выполнение всех заказов в его списке заказов.
        /// </summary>
        public static int BusyTime(Courier courier)
        {
            int time = 0;
            if (courier.Orders.Count > 0)
            {
                for (int i = 0; i < courier.Orders.Count; i++)
                {
                    time += courier.Orders[i].Time;
                }
            }
            return time;
        }
        /// <summary>
        /// Определяет время начала рабочего дня курьера.
        /// </summary>
        protected static int CourierScheduleStart()
        {
            int start = 0;
            switch (Company.CouriersList.Count % 3)
            {
                case 0:
                    break;
                case 1:
                    start = 240;
                    break;
                case 2:
                    start = 480;
                    break;
            }
            return start;
        }
        /// <summary>
        /// Определяет время конца рабочего дня курьера.
        /// </summary>
        protected static int CourierScheduleEnd()
        {
            int end = 0;
            switch (Company.CouriersList.Count % 3)
            {
                case 0:
                    end = 480;
                    break;
                case 1:
                    end = 720;
                    break;
                case 2:
                    end = 960;
                    break;
            }
            return end;
        }
        
        public static int MaxProfit
    }
}
