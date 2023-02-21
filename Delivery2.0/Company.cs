using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
        public static Queue<Order> CanceledOrders = new();
        public static List<Order> RejectedOrders = new();
        public static List<Order> DeletedOrders = new();
        //Глубина просчёта альтернативных расписаний.
        private static int MaxSearchDepth;
        private static int ActualeSearchDepth;
        public const int SearchDepthOfTryRedestribute = 0;

        private static bool CreateFromExcel = false;

        public static void StartProgram()
        {
            Console.WriteLine("Если хотите считывать курьеров и заказы из Excel нажмите (+).");
            if (Console.ReadLine() == "+")
                CreateFromExcel = true;
            CreateCouriersList();
            WaitCommand();
        }
        /// <summary>
        /// Перераспределяет заказы, которые были отправлены на перераспределение.
        /// </summary>
        public static void RedestributeFreeOrders()
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
        /// Перераспределяет заказы, которые были отправлены на перераспределение.
        /// </summary>
        public static void RedestributeFreeOrders(object? sender, CourierEventDescriptor e)
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
        internal static void DestributeCenseledOrders(object? sender, CourierEventDescriptor e)
        {
            while (CanceledOrders.Count > 0)
            {
                var order = CanceledOrders.Dequeue();
                order.Destribute();
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
            if (ActualeSearchDepth < MaxSearchDepth)
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
                if (CreateFromExcel)
                    CouriersList.Add(FootCourier.NewCourierFromExcel(i));
                else
                    CouriersList.Add(new FootCourier(i));
            }
            //Добавляет заданное кол-во курьеров на велосипедах.
            Console.WriteLine("Введите количество курьеров на велосипедах.");
            quantityBC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityBC; i++)
            {
                if (CreateFromExcel)
                    CouriersList.Add(BikeCourier.NewCourierFromExcel(i));
                else
                    CouriersList.Add(new BikeCourier(i));
            }
            //Добавляет заданное кол-во курьеров на скутерах.
            Console.WriteLine("Введите количество курьеров на скутерах.");
            quantitySC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantitySC; i++)
            {
                if (CreateFromExcel)
                    CouriersList.Add(ScuterCourier.NewCourierFromExcel(i));
                else
                    CouriersList.Add(new ScuterCourier(i));
            }
            //Добавляет заданное кол-во курьеров на машинах.
            Console.WriteLine("Введите количество курьеров на машинах.");
            quantityCC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityCC; i++)
            {
                if (CreateFromExcel)
                    CouriersList.Add(CarCourier.NewCourierFromExcel(i));
                else
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
                Console.WriteLine($"Введите действие: Добавить заказ(1)/Удалить Заказ(2)/Добавить курьера(3)/Удалить курьера(4)/Изменить глубину поиска(5)/Прекратить(Продолжить) считывание из Excel(6)/Закончить работу(7)");
                string command1 = Console.ReadLine();
                switch (command1)
                {
                    case "1":
                        Console.WriteLine($"Введите тип заказа: Доставить(1)/Забрать(2)");
                        string command2 = Console.ReadLine();
                        if (command2 == "1")
                        {
                            OrderForDelivery orderD;
                            if (CreateFromExcel)
                                orderD = OrderForDelivery.NewOrderFromExcel(orderNum);
                            else
                            {
                                Console.WriteLine("Если хотите создать случайный заказ нажмите (+).");
                                if (Console.ReadLine() == "+")
                                    orderD = OrderForDelivery.NewRandomOrder(orderNum);
                                else
                                    orderD = OrderForDelivery.NewOrder(orderNum);
                            }
                            var order = (Order)orderD;
                            order.Destribute();
                            orderNum++;
                        }
                        else if (command2 == "2")
                        {
                            OrderForTaking orderT;
                            if (CreateFromExcel)
                                orderT = OrderForTaking.NewOrderFromExcel(orderNum);
                            else
                            {
                                Console.WriteLine("Если хотите создать случайный заказ нажмите (+).");
                                if (Console.ReadLine() == "+")
                                    orderT = OrderForTaking.NewRandomOrder(orderNum);
                                else
                                    orderT = OrderForTaking.NewOrder(orderNum);
                            }
                            var order = (Order)orderT;
                            Orders.Add(order);
                            order.Destribute();
                            orderNum++;
                        }
                        EndCommand();
                        break;
                    case "2":
                        Console.WriteLine("Введите ID заказа, который хотите удалить");
                        DeliteOrder(int.Parse(Console.ReadLine()));
                        EndCommand();
                        break;
                    case "3":
                        CreateNewCourier();
                        TryRedestributeLastOrders();
                        EndCommand();
                        break;
                    case "4":
                        foreach (var cour in Couriers)
                            Console.WriteLine($"{cour.Name} (ID:{cour.CourierID})");
                        Console.WriteLine("Введите ID курьера которого хотите удалить.");
                        var id = int.Parse(Console.ReadLine());
                        bool correct = false;
                        foreach (var courier in Couriers)
                            if (courier.CourierID == id)
                            {
                                correct = true;
                                break;
                            }
                        if (correct)
                            DeliteCourier(id);
                        else
                            Console.WriteLine("ID введён некорректно.");
                        GetInfo();
                        break;
                    case "5":
                        SetSearchDepth();
                        break;
                    case "6":
                        CreateFromExcel = !CreateFromExcel;
                        break;
                    case "7":
                        orderNum = 0;
                        break;
                    default:
                        Console.WriteLine("Введите действие заново.");
                        break;
                }
            }
        }
        /// <summary>
        /// Все заказы пробуют перераспределиться.
        /// </summary>
        private static void TryRedestributeLastOrders()
        {
            //Сохраняем изначальную глубину просчёта, затем ставим глубину на константовое значение глубины просчёта при попытке перераспредидении всех заказов.
            var maxSearchDepth = MaxSearchDepth;
            MaxSearchDepth = SearchDepthOfTryRedestribute;
            bool end = false;
            //Пробуем перераспределить заказы, если заказ перераспределяется, то начинаем перебор сначала с новыми условиями.
            while (!end)
            {
                int countLastOrders = 0;
                foreach (var courier in Couriers)
                {
                    if (courier.Orders.Count > 0)
                        countLastOrders++;
                }
                int i = 0;
                foreach (var courier in Couriers)
                {
                    if (courier.Orders.Count > 0)
                        if (courier.Orders.Last().TryRedestribute())
                            break;
                        else
                            i++;
                }
                if (i == countLastOrders)
                    end = true;
            }
            //Возвращяем изначальную глубину просчёта.
            MaxSearchDepth = maxSearchDepth;
        }
        /// <summary>
        /// Попытка восстановить отказанные заказы и вывод информации.
        /// </summary>
        private static void EndCommand()
        {
            var rejectedOrders = new Queue<Order>();
            foreach (var order in RejectedOrders)
                rejectedOrders.Enqueue(order);
            RejectedOrders.Clear();
            while (rejectedOrders.Count > 0)
                rejectedOrders.Dequeue().Destribute();
            GetInfo();
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
            if (DeletedOrders.Count > 0)
            {
                Console.Write($"Удалённые заказы:");
                for (int i = 0; i < DeletedOrders.Count; i++)
                {
                    Console.Write($" {DeletedOrders[i].Id}");
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
        /// <summary>
        /// Удаление заказа по его ID.
        /// </summary>
        private static void DeliteOrder(int id)
        {
            foreach (var order in RejectedOrders)
            {
                if (order.Id == id)
                {
                    Orders.Remove(order);
                    DeletedOrders.Add(order);
                    RejectedOrders.Remove(order);
                    return;
                }
            }
            foreach (var order in Orders)
            {
                if (order.Id == id)
                {
                    Orders.Remove(order);
                    order.ActualeVariant.Courier.DeleteOrder(order);
                    return;
                }
            }
        }
        /// <summary>
        /// Удаление курьера по его ID
        /// </summary>
        private static void DeliteCourier(int id)
        {
            Courier delCourier = null;
            Queue<Courier> couriers = new();
            foreach (var courier in Couriers)
                couriers.Enqueue(courier);
            QuantityC--;
            Couriers = new Courier[QuantityC];
            int i = 0;
            while (couriers.Count > 0)
            {
                var courier = couriers.Dequeue();
                if (courier.CourierID != id)
                {
                    Couriers[i] = courier;
                    i++;
                }
                else
                    delCourier = courier;
            }
            delCourier.CancelAllOrders();
        }
        /// <summary>
        /// Создание нового курьера.
        /// </summary>
        private static void CreateNewCourier()
        {
            Console.WriteLine("Выберете тип курьера: Пеший(1)/На велосипеде(2)/На скутере(3)/На машине(4)");
            var command = Console.ReadLine();
            Courier newCourier = null;
            switch (command)
            {
                case "1":
                    if (CreateFromExcel)
                        newCourier = FootCourier.NewCourierFromExcel(quantityFC++);
                    else
                        newCourier = new FootCourier(quantityFC++);
                    break;
                case "2":
                    if (CreateFromExcel)
                        newCourier = BikeCourier.NewCourierFromExcel(quantityBC++);
                    else
                        newCourier = new BikeCourier(quantityBC++);
                    break;
                case "3":
                    if (CreateFromExcel)
                        newCourier = ScuterCourier.NewCourierFromExcel(quantitySC++);
                    else
                        newCourier = new ScuterCourier(quantitySC++);
                    break;
                case "4":
                    if (CreateFromExcel)
                        newCourier = CarCourier.NewCourierFromExcel(quantityCC++);
                    else
                        newCourier = new CarCourier(quantityCC++);
                    break;
                default:
                    Console.WriteLine("Команда введена некоректно.");
                    break;
            }
            CouriersList.Add(newCourier);
            newCourier.Intilize();
            AddCourier(newCourier);
        }
        /// <summary>
        /// Добавление нового курьера в массив курьеров.
        /// </summary>
        private static void AddCourier(Courier newCourier)
        {
            Queue<Courier> couriers = new();
            foreach (var courier in Couriers)
                couriers.Enqueue(courier);
            QuantityC++;
            Couriers = new Courier[QuantityC];
            int i = 0;
            while (couriers.Count > 0)
            {
                var courier = couriers.Dequeue();
                Couriers[i] = courier;
                i++;
            }
            Couriers[i] = newCourier;
        }
    }
}
