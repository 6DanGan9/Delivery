namespace Delivery2._4
{
    internal class Program
    {
        /// <summary>
        /// Показывает текущую информацию о курьерах и заказах.
        /// </summary>
        private static void GetInfo()
        {
            int fullProfit = 0;
            Console.WriteLine("==============================");
            for (int i = 0; i < Company.quantityC; i++)
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
                Console.Write($" Непринятые заказы:");
                for (int i = 0; i < Company.RejectedOrders.Count; i++)
                {
                    Console.Write($" {Company.RejectedOrders[i].Id}");
                }
                Console.WriteLine(".");
            }
            Console.WriteLine($"Суммарная прибыль: {fullProfit}.");
            Console.WriteLine("==============================");
        }
        static void Main(string[] args)
        {
            //Добавляет заданное кол-во пеших курьеров.
            Console.WriteLine("Введите количество пеших курьеров.");
            int quantityFC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityFC; i++)
            {
                Company.CouriersList.Add(new FootCourier(i));
            }
            //Добавляет заданное кол-во курьеров на велосипедах.
            Console.WriteLine("Введите количество курьеров на велосипедах.");
            int quantityBC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityBC; i++)
            {
                Company.CouriersList.Add(new BikeCourier(i));
            }
            //Добавляет заданное кол-во курьеров на скутерах.
            Console.WriteLine("Введите количество курьеров на скутерах.");
            int quantitySC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantitySC; i++)
            {
                Company.CouriersList.Add(new ScuterCourier(i));
            }
            //Добавляет заданное кол-во курьеров на машинах.
            Console.WriteLine("Введите количество курьеров на машинах.");
            int quantityCC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityCC; i++)
            {
                Company.CouriersList.Add(new CarCourier(i));
            }
            //Считает общее кол-во курьеров и переделывает список в массив.
            Company.quantityC = quantityFC + quantityBC + quantitySC + quantityCC;
            Company.Couriers = Company.CouriersList.ToArray();
            int orderNum = 1;
            while (orderNum >= 0)
            {
                Console.WriteLine($"Введите действие: Добавить заказ/Закончить работу");
                string command1 = Console.ReadLine();
                if (command1 == "Добавить заказ")
                {
                    Console.WriteLine($"Введите тип заказа: Доставить/Забрать");
                    string command2 = Console.ReadLine();
                    if (command2 == "Доставить")
                    {
                        OrderForDelivery order = OrderForDelivery.NewOrder(orderNum);
                        OrderDestributor.Distributoin(order);
                        orderNum++;
                    }
                    else if (command2 == "Забрать")
                    {
                        OrderForTaking order = OrderForTaking.NewOrder(orderNum);
                        OrderDestributor.Distributoin(order);
                        orderNum++;
                    }
                    while (Company.FreeOrders.Count > 0)
                    {
                        Order order1 = Company.FreeOrders[^1];
                        Company.FreeOrders.RemoveAt(Company.FreeOrders.Count - 1);
                        if (order1 is OrderForDelivery orderD)
                        {
                            OrderDestributor.Distributoin(orderD);
                        }
                        if (order1 is OrderForTaking orderT)
                        {
                            OrderDestributor.Distributoin(orderT);
                        }
                    }
                    GetInfo();
                }
                else if (command1 == "Закончить работу")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введите действие заново.");
                }
            }
        }
    }
}