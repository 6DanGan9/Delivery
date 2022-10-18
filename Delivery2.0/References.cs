using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._0
{
    internal class References
    {
        private static int XCoord(string fullAdress)
        {
            int comma = fullAdress.IndexOf(", ");
            int xAdress = int.Parse(fullAdress.Substring(0, comma));
            return xAdress;
        }
        private static int YCoord(string fullAdress)
        {
            int comma = fullAdress.IndexOf(", ");
            int yAdress = int.Parse(fullAdress.Substring(comma + 2));
            return yAdress;
        }
        public static Coord ToCoord(string cord)
        {
            Coord coord = new();
            coord.x = XCoord(cord);
            coord.y = YCoord(cord);
            return coord;
        }
        public static double Distace(Coord coord1, Coord coord2)
        {
            double distanse = Math.Sqrt(Math.Pow(coord2.x - coord1.x, 2) + Math.Pow(coord2.y - coord1.y, 2));
            return distanse;
        }
        public static int TimeToMinute(string time)
        {
            int split = time.IndexOf(":");
            int hour = int.Parse(time.Substring(0, split));
            int minute = int.Parse(time.Substring(split + 1));
            int timeMinute = (hour * 60) + minute - 480;
            return timeMinute;
        }
        public static int Profit(Order order, Courier courier, double distance)
        {
            int profit = order.money - (int)Math.Round((order.distance + distance) * courier.price);
            return profit;
        }
        public static int TimeToOrder(Order order, Courier courier, double distance)
        {
            int time = (int)Math.Round((order.distance + distance) / courier.speed * 60);
            return time;
        }
        public static void Result()
        {
            int summTime = 0;
            int fullProfit = 0;
            Console.WriteLine("==============================");
            for (int i = 0; i < CourierLogic.quantityC; i++)
            {
                if (CourierLogic.couriers[i].orders.Count > 0)
                {
                    Console.Write($"{CourierLogic.couriers[i].tip} будет выполнять заказ(ы):");
                    for (int j = 0; j < CourierLogic.couriers[i].orders.Count; j++)
                    {
                        summTime += CourierLogic.couriers[i].orders[j].time;
                        fullProfit += CourierLogic.couriers[i].orders[j].profit;
                        Console.Write($" {CourierLogic.couriers[i].orders[j].number} ({CourierLogic.couriers[i].orders[j].profit})");
                    }
                    Console.Write($" Суммарное время:{summTime}");
                    summTime = 0;
                    Console.WriteLine(".");
                }
            }
            if (OrderDestributor.rejectedOrders.Count > 0)
            {
            Console.Write($" Непринятые заказы:");
            for (int i = 0; i < OrderDestributor.rejectedOrders.Count; i++)
            {
                Console.Write($" {OrderDestributor.rejectedOrders[i].number}");
            }
            Console.WriteLine(".");
            }
            Console.WriteLine($"Суммарная прибыль: {fullProfit}.");
            Console.WriteLine("==============================");
        }
    }
}
