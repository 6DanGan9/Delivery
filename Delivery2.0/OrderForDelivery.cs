using Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    /// <summary>
    /// Заказ на доставку посылки.
    /// </summary>
    internal class OrderForDelivery : Order
    {
        private static Random rnd = new();
        private static int ExcelLine = 2;

        public OrderForDelivery(int id, Coord start, Coord end, DateTime deadline, double weigth, TimeSpan time, Variant actualeVariant)
        {
            Id = id;
            Start = start;
            End = end;
            Weigth = weigth;
            DeadLine = deadline;
            Time = time;
            ActualeVariant = actualeVariant;
        }

        private OrderForDelivery(int id, Coord start, Coord end, string deadline, double weigth)
        {
            Id = id;
            Start = start;
            End = end;
            Weigth = weigth;
            if (deadline == null)
                DeadLine = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(600, 1439));
            else
                DeadLine = DateTime.Today + TimeCalculator.TimeToMinute(deadline);
        }
        /// <summary>
        /// Создание нового заказа.
        /// </summary>
        public static OrderForDelivery NewOrder(int orderNumber)
        {
            Console.WriteLine("Введите координаты начала через пробел(x y).");
            Coord start = CoordHelper.NewCoord(Console.ReadLine());
            Console.WriteLine("Введите координаты конца через пробел(x y).");
            Coord end = CoordHelper.NewCoord(Console.ReadLine());
            Console.WriteLine("Введите массу груза.");
            double weigth = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите конечное время в формате 00:00.");
            string time = Console.ReadLine();
            OrderForDelivery order = new(orderNumber, start, end, time, weigth);
            Company.Orders.Add(order);
            return order;
        }
        /// <summary>
        /// Создание нового заказа.
        /// </summary>
        public static OrderForDelivery NewRandomOrder(int orderNumber)
        {
            Coord start = CoordHelper.RandCoord();
            Console.WriteLine($"{start.X}, {start.Y}");
            Coord end = CoordHelper.RandCoord();
            Console.WriteLine($"{end.X}, {end.Y}");
            Console.WriteLine("3");
            double weigth = 3;
            string time = null;
            OrderForDelivery order = new(orderNumber, start, end, time, weigth);
            Console.WriteLine(order.DeadLine);
            Company.Orders.Add(order);
            return order;
        }

        public static OrderForDelivery NewOrderFromExcel(int orderNumber)
        {
            var excel = new ExcelHelper();
            excel.Open("OrdersForDelivery");
            if (excel.Get(ExcelLine, 1) == "")
            {
                Console.WriteLine("В файле заказов больше нету, создайте новый заказ.");
                excel.Close();
                Console.WriteLine("Если хотите создать случайный заказ нажмите (+).");
                if (Console.ReadLine() == "+")
                    return NewRandomOrder(orderNumber);
                else
                    return NewOrder(orderNumber);
            }
            else
            {
                Coord start = new(Convert.ToInt32(excel.Get(ExcelLine, 2)), Convert.ToInt32(excel.Get(ExcelLine, 3)));
                Coord end = new(Convert.ToInt32(excel.Get(ExcelLine, 4)), Convert.ToInt32(excel.Get(ExcelLine, 5)));
                double weigth = Convert.ToInt32(excel.Get(ExcelLine, 6));
                string time = excel.Get(ExcelLine, 7);
                excel.Close();
                OrderForDelivery order = new(orderNumber, start, end, time, weigth);
                Company.Orders.Add(order);
                ExcelLine++;
                return order;
            }
        }
    }
}
