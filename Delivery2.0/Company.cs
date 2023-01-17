using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    internal static class Company
    {
        public const double PricePerDistance = 200;
        //Стандартные характеристики пешего курьера.
        public const double DefaultFootCurierSpeed = 7;
        public const double DefaultFootCurierCapacity = 4;
        public const double DefaultFootCurierPricePerDistance = 40;
        //Стандартные характеристики курьера на велосипеде.
        public const double DefaultBikeCurierSpeed = 15;
        public const double DefaultBikeCurierCapacity = 6;
        public const double DefaultBikeCurierPricePerDistance = 60;
        //Стандартные характеристики курьера на скутере.
        public const double DefaultScuterCurierSpeed = 30;
        public const double DefaultScuterCurierCapacity = 8;
        public const double DefaultScyterCurierPricePerDistance = 80;
        //Стандартные характеристики курьера на машине.
        public const double DefaultCarCurierSpeed = 25;
        public const double DefaultCarCurierCapacity = 60;
        public const double DefaultCarCurierPricePerDistance = 80;

        public static Stack<List<List<int>>> LoopChecker = new();
        public static bool InLoop = false;

        public static List<Courier> CouriersList = new();
        public static int QuantityC;
        private static int quantityFC;
        private static int quantityBC;
        private static int quantitySC;
        private static int quantityCC;
        public static Courier[] Couriers = new Courier[QuantityC];

        public static List<Order> Orders = new();
        public static Stack<Order> FreeOrders = new();
        public static List<Order> RejectedOrders = new();
        public static List<Order> DelitedOrders = new();

        public static event EventHandler<OrderEventDescriptor> DeliteOrderEvent;

        public static event EventHandler<CourierEventDescriptor> NewCourierEvent;

        public static event EventHandler<CourierEventDescriptor> DeliteCourierEvent;

        public static void StartProgram()
        {
            CreateCouriersList();
            WaitCommand();
        }

        public static void DestributeFreeOrders(object? sender, CourierEventDescriptor e)
        {
            Console.Write($"На перераспределение направились заказы:");
            foreach (var order in FreeOrders)
                Console.Write($"{order.Id} ");
            Console.WriteLine(".");
            CheckLoop();
            if (InLoop)
            {
                Console.WriteLine("Loop");
                Console.ReadKey();
            }
            while (FreeOrders.Count > 0)
            {
                var order = FreeOrders.Pop();
                Console.WriteLine($"Перераспределяется заказ №{order.Id}");
                order.Redestribute();
            }
        }

        private static void CheckLoop()
        {
            var schedule = new List<int>();
            schedule.Add(FullProfit());
            foreach (var courier in Couriers)
            {
                schedule.Add(courier.CourierID);
                foreach (var order in courier.Orders)
                    schedule.Add(order.Id);
            }
            foreach (var order in RejectedOrders)
                schedule.Add(order.Id);
            foreach (var order in FreeOrders)
                schedule.Add(order.Id);
            foreach (var sched in LoopChecker.Peek())
            {
                if (schedule.Is(sched))
                {
                    InLoop = true;
                    return;
                }
            }
            LoopChecker.Peek().Add(schedule);
        }

        private static bool Is(this List<int> ints1, List<int> ints2)
        {
            if (ints1.Count != ints2.Count)
                return false;
            for (int i = 0; i < ints1.Count; i++)
                if (ints1[i] != ints2[i])
                    return false;
            return true;
        }

        private static void CreateCouriersList()
        {
            //Добавляет заданное кол-во пеших курьеров.
            Console.WriteLine("Введите количество пеших курьеров.");
            quantityFC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityFC; i++)
            {
                CouriersList.Add(new FootCourier(i));
            }
            //Добавляет заданное кол-во курьеров на велосипедах.
            Console.WriteLine("Введите количество курьеров на велосипедах.");
            quantityBC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityBC; i++)
            {
                CouriersList.Add(new BikeCourier(i));
            }
            //Добавляет заданное кол-во курьеров на скутерах.
            Console.WriteLine("Введите количество курьеров на скутерах.");
            quantitySC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantitySC; i++)
            {
                CouriersList.Add(new ScuterCourier(i));
            }
            //Добавляет заданное кол-во курьеров на машинах.
            Console.WriteLine("Введите количество курьеров на машинах.");
            quantityCC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityCC; i++)
            {
                CouriersList.Add(new CarCourier(i));
            }
            //Считает общее кол-во курьеров и переделывает список в массив.
            QuantityC = quantityFC + quantityBC + quantitySC + quantityCC;
            Couriers = CouriersList.ToArray();
            foreach (var courier in Couriers)
            {
                courier.Intilize();
            }
            LoopChecker.Push(new List<List<int>>());
        }

        private static void WaitCommand()
        {
            int orderNum = 1;
            while (orderNum >= 0)
            {
                Console.WriteLine($"Введите действие: Добавить заказ(1)/Удалить Заказ(2)/Добавить курьера(3)/Удалить курьера(4)/Закончить работу(5)");
                string command1 = Console.ReadLine();
                if (command1 == "1")
                {
                    Console.WriteLine($"Введите тип заказа: Доставить(1)/Забрать(2)");
                    string command2 = Console.ReadLine();
                    if (command2 == "1")
                    {
                        var orderD = OrderForDelivery.NewOrder(orderNum);
                        var order = (Order)orderD;
                        order.Destribute();
                        orderNum++;
                    }
                    else if (command2 == "2")
                    {
                        var orderT = OrderForTaking.NewOrder(orderNum);
                        var order = (Order)orderT;
                        Orders.Add(order);
                        order.Destribute();
                        orderNum++;
                    }
                    GetInfo();
                }
                else if (command1 == "2")
                {
                    //Console.WriteLine("Введите ID заказа, который хотите удалить");
                    //Company.DeliteOrder(int.Parse(Console.ReadLine()));
                    //GetInfo();
                }
                else if (command1 == "3")
                {
                    //Company.AddCourier();
                    //GetInfo();
                }
                else if (command1 == "4")
                {
                    //foreach (var cour in Company.Couriers)
                    //    Console.WriteLine($"{cour.Name} ({cour.CourierID})");
                    //Console.WriteLine("Введите ID курьера");
                    //Company.DeliteCourier(int.Parse(Console.ReadLine()));
                    //GetInfo();
                }
                else if (command1 == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введите действие заново.");
                }
            }
        }

        /// <summary>
        /// Показывает текущую информацию о курьерах и заказах.
        /// </summary>
        public static void GetInfo()
        {
            Console.WriteLine("==============================");
            for (int i = 0; i < QuantityC; i++)
            {
                if (Couriers[i].Orders.Count > 0)
                {
                    Console.Write($"{Couriers[i].Name} будет выполнять заказ(ы):");
                    for (int j = 0; j < Couriers[i].Orders.Count; j++)
                    {
                        Console.Write($" {Couriers[i].Orders[j].Id} ({Couriers[i].Orders[j].Profit})");
                    }
                    Console.Write($" Суммарное время:{Couriers[i].BusyTime}");
                    Console.WriteLine(".");
                }
            }
            if (RejectedOrders.Count > 0)
            {
                Console.Write($"Непринятые заказы:");
                foreach (var order in RejectedOrders)
                {
                    Console.Write($" {order.Id}");
                }
                Console.WriteLine(".");
            }
            if (DelitedOrders.Count > 0)
            {
                Console.Write($"Удалённые заказы:");
                for (int i = 0; i < DelitedOrders.Count; i++)
                {
                    Console.Write($" {DelitedOrders[i].Id}");
                }
                Console.WriteLine(".");
            }
            int fullProfit = FullProfit();
            Console.WriteLine($"Суммарная прибыль: {fullProfit}.");
            Console.WriteLine("==============================");
        }

        public static int FullProfit()
        {
            int profit = 0;
            foreach (var courier in Couriers)
                foreach (var order in courier.Orders)
                    profit += order.Profit;
            return profit;
        }
    }
}
