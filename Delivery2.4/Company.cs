using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
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

        public static List<Courier> CouriersList = new();
        public static int QuantityC;
        public static Courier[] Couriers = new Courier[QuantityC];
        public static List<Order> Orders = new();
        public static Queue<Order> FreeOrders = new();
        public static List<Order> RejectedOrders = new();
        public static List<Order> DelitedOrders = new();

        private static int quantityFC;
        private static int quantityBC;
        private static int quantitySC;
        private static int quantityCC;

        public static int quantityStep;

        public static int CalcFullProfit()
        {
            int fullProfit = 0;
            foreach(var courier in Couriers)
                foreach(var order in courier.Orders)
                    fullProfit += order.Profit;
            return fullProfit;
        }

        public static void StartProgram()
        {
            CreateCouriersList();
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
        }

        public static void DeliteCourier(int courierID)
        {
            Courier courier = Couriers[courierID];
            courier.Delite();
            CouriersList.Remove(courier);
            QuantityC--;
            Courier[] couriers = new Courier[QuantityC];
            int j = 0;
            for (int i = 0; i < QuantityC + 1; i++)
            {
                
                if (Couriers[i] != courier)
                {
                    Couriers[i].ResetID(j);
                    couriers[j] = Couriers[i];
                    j++;
                }
            }
            Couriers = new Courier[QuantityC];
            Couriers = couriers;
            DestributeFreeOrders();
            CheckAllOrderForRelevanceOfPosition();
        }
        public static void DestributeFreeOrders()
        {
            while (FreeOrders.Count > 0)
            {
                Order orderForRedestribute = FreeOrders.Dequeue();
                OrderDestributor.Distributoin(orderForRedestribute);
            }
        }

        public static void AddCourier()
        {
            Console.WriteLine("Укажите тип нового курьера: Пеший(1)/ На велосипеде(2)/ На скутере(3)/ На машине(4)");
            CreateNewCourier(int.Parse(Console.ReadLine()));
            ReDistributeAllOrders();
            CheckAllOrderForRelevanceOfPosition();
        }

        private static void ReDistributeAllOrders()
        {
            foreach(var courier in Couriers)
            {
                foreach(var order in courier.Orders)
                    FreeOrders.Enqueue(order);
                courier.Orders.Clear();
            }
            foreach(var order in RejectedOrders)
                FreeOrders.Enqueue(order);
            RejectedOrders.Clear();
            DestributeFreeOrders();
        }

        private static void CreateNewCourier(int tip)
        {
            switch(tip)
            {
                case 1:
                    AttahcingCourier(new FootCourier(quantityFC));
                    quantityFC++;
                    break;
                case 2:
                    AttahcingCourier(new BikeCourier(quantityBC));
                    quantityBC++;
                    break;
                case 3:
                    AttahcingCourier(new ScuterCourier(quantitySC));
                    quantitySC++;
                    break;
                case 4:
                    AttahcingCourier(new CarCourier(quantityCC));
                    quantityCC++;
                    break;
            }
        }

        private static void AttahcingCourier(Courier courier)
        {
            CouriersList.Add(courier);
            QuantityC++;
            Courier[] couriers = new Courier[QuantityC];
            for (int i = 0; i < QuantityC - 1; i++)
                couriers[i] = Couriers[i];
            couriers[QuantityC - 1] = courier;
            Couriers = new Courier[QuantityC];
            Couriers = couriers;
        }

        internal static void DeliteOrder(int id)
        {
            foreach(var courier in Couriers)
            {
                foreach(Order order in courier.Orders)
                    if (order.Id == id)
                    {
                        Orders.Remove(order);
                        courier.DelitOrder(id);
                        break;
                    }
            }
            DestributeFreeOrders();
            CheckAllOrderForRelevanceOfPosition();
        }

        public static void CheckAllOrderForRelevanceOfPosition()
        {
            List<int> exeptionCheckers = new();
            int exeptionChecker = 0;
            bool change = false;
            while (change == false)
            {
                change = true;
                foreach(var order in Orders)
                {
                    if (order.CheckRelevanceOfPosition())
                    {
                        change = false;
                        exeptionChecker = CalcFullProfit();
                        break;
                    }
                }
                if (!exeptionCheckers.Contains(exeptionChecker))
                    exeptionCheckers.Add(exeptionChecker);
                else
                {
                    var maxProfit = exeptionCheckers.Max();
                    var quantityNeededSteps = exeptionCheckers.IndexOf(maxProfit) - exeptionCheckers.IndexOf(exeptionChecker);
                    for (int i = 0; i < quantityNeededSteps; i++)
                        foreach (var order in Orders)
                            if (order.CheckRelevanceOfPosition())
                                break;
                    change = true;
                }
            }
        }
    }
}
