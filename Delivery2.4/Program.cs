using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Company.StartProgram();
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
                        var order = (Order) orderD;
                        OrderDestributor.Distributoin(order);
                        orderNum++;
                    }
                    else if (command2 == "2")
                    {
                        var orderT = OrderForTaking.NewOrder(orderNum);
                        var order = (Order)orderT;
                        OrderDestributor.Distributoin(order);
                        orderNum++;
                    }
                    Company.DestributeFreeOrders();
                    GetInfo();
                }
                else if (command1 == "2")
                {
                    Console.WriteLine("Введите ID заказа, который хотите удалить");
                    Company.DeliteOrder(int.Parse(Console.ReadLine()));
                    GetInfo();
                }
                else if (command1 == "3")
                {
                    Company.AddCourier();
                    GetInfo();
                }
                else if (command1 == "4")
                {

                    foreach (var cour in Company.Couriers)
                        Console.WriteLine($"{cour.Name} ({cour.CourierID})");
                    Console.WriteLine("Введите ID курьера");
                    Company.DeliteCourier(int.Parse(Console.ReadLine()));
                    GetInfo();
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
        private static void GetInfo()
        {
            int fullProfit = 0;
            Console.WriteLine("==============================");
            for (int i = 0; i < Company.QuantityC; i++)
            {
                if (Company.Couriers[i].Orders.Count > 0)
                {
                    Console.Write($"{Company.Couriers[i].Name} будет выполнять заказ(ы):");
                    for (int j = 0; j < Company.Couriers[i].Orders.Count; j++)
                    {
                        fullProfit += Company.Couriers[i].Orders[j].Profit;
                        Console.Write($" {Company.Couriers[i].Orders[j].Id} ({Company.Couriers[i].Orders[j].Profit})");
                    }
                    Console.Write($" Суммарное время:{Company.Couriers[i].BusyTime}");
                    Console.WriteLine(".");
                }
            }
            if (Company.RejectedOrders.Count > 0)
            {
                Console.Write($"Непринятые заказы:");
                for (int i = 0; i < Company.RejectedOrders.Count; i++)
                {
                    Console.Write($" {Company.RejectedOrders[i].Id}");
                }
                Console.WriteLine(".");
            }
            if (Company.DelitedOrders.Count > 0)
            {
                Console.Write($"Удалённые заказы:");
                for (int i = 0; i < Company.DelitedOrders.Count; i++)
                {
                    Console.Write($" {Company.DelitedOrders[i].Id}");
                }
                Console.WriteLine(".");
            }
            Console.WriteLine($"Суммарная прибыль: {fullProfit}.");
            Console.WriteLine("==============================");
        }
    }
}