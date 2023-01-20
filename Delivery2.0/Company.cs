﻿using System;
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
        //Информация о курьерах.
        public static List<Courier> CouriersList = new();
        public static Courier[] Couriers = new Courier[QuantityC];
        public static int QuantityC;
        private static int quantityFC;
        private static int quantityBC;
        private static int quantitySC;
        private static int quantityCC;
        //Информация о заказах.
        public static List<Order> Orders = new();
        public static Stack<Order> FreeOrders = new();
        public static List<Order> RejectedOrders = new();
        public static List<Order> DelitedOrders = new();
        //Глубина просчёта альтернативных расписаний.
        private static int MaxSearchDepth;
        private static int ActualeSearchDepth;
        
        public static event EventHandler<OrderEventDescriptor> DeliteOrderEvent;

        public static event EventHandler<CourierEventDescriptor> NewCourierEvent;

        public static event EventHandler<CourierEventDescriptor> DeliteCourierEvent;

        public static void StartProgram()
        {
            CreateCouriersList();
            WaitCommand();
        }
        /// <summary>
        /// Перераспределяет заказы, которые были отправлены на перераспределение.
        /// </summary>
        public static void DestributeFreeOrders(object? sender, CourierEventDescriptor e)
        {
            Console.Write($"На перераспределение направились заказы:");
            foreach (var order in FreeOrders)
                Console.Write($"{order.Id} ");
            Console.WriteLine(".");
            while (FreeOrders.Count > 0)
            {
                var order = FreeOrders.Pop();
                Console.WriteLine($"Перераспределяется заказ №{order.Id}");
                order.Redestribute();
            }
        }
        /// <summary>
        /// Считает суммарную прибыль расписания.
        /// </summary>
        public static int FullProfit()
        {
            int profit = 0;
            foreach (var courier in Couriers)
                foreach (var order in courier.Orders)
                    profit += order.Profit;
            return profit;
        }
        /// <summary>
        /// Проверяет, удовлетворяет ли глубина рассчёта расписания ограничению.
        /// </summary>
        public static bool CheckSearchDepth()
        {
            if (ActualeSearchDepth <= MaxSearchDepth)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Переход на следующий уровень просчёта вариантов.
        /// </summary>
        public static void SearchDepthUp()
        {
            ActualeSearchDepth++;
        }
        /// <summary>
        /// Выход с уровня подсчёта вариантов.
        /// </summary>
        public static void SearchDepthDown()
        {
            ActualeSearchDepth--;
        }
        /// <summary>
        /// Начальная настройка программы.
        /// </summary>
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
        }
        /// <summary>
        /// Работа программы.
        /// </summary>
        private static void WaitCommand()
        {
            int orderNum = 1;
            SetSearchDepth();
            while (orderNum > 0)
            {
                Console.WriteLine($"Введите действие: Добавить заказ(1)/Удалить Заказ(2)/Добавить курьера(3)/Удалить курьера(4)/Изменить глубину поиска(5)/Закончить работу(6)");
                string command1 = Console.ReadLine();
                switch (command1)
                {
                    case "1":
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
                        break;
                    case "2":
                        //Console.WriteLine("Введите ID заказа, который хотите удалить");
                        //Company.DeliteOrder(int.Parse(Console.ReadLine()));
                        //GetInfo();
                        break;
                    case "3":
                        //Company.AddCourier();
                        //GetInfo();
                        break;
                    case "4":
                        //foreach (var cour in Company.Couriers)
                        //    Console.WriteLine($"{cour.Name} ({cour.CourierID})");
                        //Console.WriteLine("Введите ID курьера");
                        //Company.DeliteCourier(int.Parse(Console.ReadLine()));
                        //GetInfo();
                        break;
                    case "5":
                        SetSearchDepth();
                        break;
                    case "6":
                        orderNum = 0;
                        break;
                    default:
                        Console.WriteLine("Введите действие заново.");
                        break;
                }
            }
        }

        /// <summary>
        /// Показывает текущую информацию о курьерах и заказах.
        /// </summary>
        private static void GetInfo()
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
        /// <summary>
        /// Установка максимальной глубины рассчёта альтернативных расписаний.
        /// </summary>
        private static void SetSearchDepth()
        {
            Console.WriteLine("Введите глубину поиска");
            MaxSearchDepth = int.Parse(Console.ReadLine());
        }
    }
}
