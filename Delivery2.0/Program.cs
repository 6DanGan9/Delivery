namespace Delivery2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int orderNum = 1;
            Console.WriteLine("Введите количество пеших курьеров.");
            int quantityFC = int.Parse(Console.ReadLine());
            Courier.NewFC(quantityFC);
            Console.WriteLine("Введите количество курьеров на велосипедах.");
            int quantityBC = int.Parse(Console.ReadLine());
            Courier.NewBC(quantityBC);
            Console.WriteLine("Введите количество курьеров на скутерах.");
            int quantitySC = int.Parse(Console.ReadLine());
            Courier.NewSC(quantitySC);
            Console.WriteLine("Введите количество курьеров на машинах.");
            int quantityCC = int.Parse(Console.ReadLine());
            Courier.NewCC(quantityCC);
            CourierLogic.ArrayCouriers();
            CourierLogic.quantityC = quantityFC + quantityBC + quantitySC + quantityCC;
            while (orderNum >= 0)
            {
                Console.WriteLine($"Введите действие: Добавить заказ/Закончить работу");
                string command = Console.ReadLine();
                if (command == "Добавить заказ")
                {
                    Order order = Order.NewOrder(orderNum);
                    orderNum++;
                    OrderDestributor.Destribution(order);
                    while (OrderDestributor.freeOrders.Count > 0)
                    {
                        Order order1 = OrderDestributor.freeOrders[^1];
                        OrderDestributor.freeOrders.RemoveAt(OrderDestributor.freeOrders.Count - 1);
                        OrderDestributor.Destribution(order1);
                    }
                    References.Result();
                }
                else if (command == "Закончить работу")
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