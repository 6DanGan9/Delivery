using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._0
{
    internal class Order
    {
        static Random rnd = new();
        public Order(int number, int mass, Coord coord1, Coord coord2/*, string deadline*/)
        {
            this.number = number;
            this.mass = mass;
            this.coord1 = coord1;
            this.coord2 = coord2;
            distance = References.Distace(coord1, coord2);
            this.deadline = rnd.Next(240, 961);  //References.TimeToMinute(deadline);
            money = (int) Math.Round(distance) * Price;
        }
        public Courier? courier { get; set; }
        public int number { get; set; }
        public int time { get; set; }
        public int profit { get; set; }
        public int mass { get; }
        public static int Price = 200;
        public Coord coord1 { get; }
        public Coord coord2 { get; }
        public int deadline { get; }
        public double distance { get; }
        public int money { get; }
        public static Order NewOrder(int OrderNumber)
        {
            Console.WriteLine("Введите координаты начала через запятую(x, y).");
            Coord coord1 = Courier.RandCoord(); //Console.ReadLine();
            Console.WriteLine($"{coord1.x}, {coord1.y}");
            Console.WriteLine("Введите координаты конца через запятую(x, y).");
            Coord coord2 = Courier.RandCoord(); //Console.ReadLine();
            Console.WriteLine($"{coord2.x}, {coord2.y}");
            Console.WriteLine("Введите массу груза.");
            int mass = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите конечное время в формате 00:00.");
            //string time = (Console.ReadLine());
            Order order = new(OrderNumber, mass, coord1, coord2/*, time*/);
            Console.WriteLine(order.deadline);
            return order;
        }
    }
}
