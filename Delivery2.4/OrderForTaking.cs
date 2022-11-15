using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Заказ на взятие посылки.
    /// </summary>
    internal class OrderForTaking : Order
    {
        static Random rnd = new();
        private OrderForTaking(int id, Coord start, Coord end, string deadline, double weigth)
        {
            Id = id;
            Start = start;
            End = end;
            Weigth = weigth;
            DeadLine = DateTime.Today + /*TimeCalculator.TimeToMinute(deadline)*/TimeSpan.FromMinutes(rnd.Next(600, 1439));
            Console.WriteLine(DeadLine);
        }
        public static OrderForTaking NewOrder(int OrderNumber)
        {
            Console.WriteLine("Введите координаты начала через запятую(x, y).");
            Coord start = CoordHelper.RandCoord(); //Console.ReadLine();
            Console.WriteLine($"{start.X}, {start.Y}");
            Console.WriteLine("Введите координаты конца через запятую(x, y).");
            Coord end = CoordHelper.RandCoord(); //Console.ReadLine();
            Console.WriteLine($"{end.X}, {end.Y}");
            Console.WriteLine("Введите массу груза.");
            Console.WriteLine("3");
            double weigth = 3; //int.Parse(Console.ReadLine());
            Console.WriteLine("Введите конечное время в формате 00:00.");
            string time = "00:00"; //Console.ReadLine();
            OrderForTaking order = new(OrderNumber, start, end, time, weigth);
            Company.Orders.Add(order);
            return order;
        }
    }
}
