namespace Delivery2._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество пеших курьеров.");
            int quantityFC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityFC; i++)
            {
                Company.CouriersList.Add(new FootCourier(i));
            }
            Console.WriteLine("Введите количество курьеров на велосипедах.");
            int quantityBC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityBC; i++)
            {
                Company.CouriersList.Add(new BikeCourier(i));
            }
            Console.WriteLine("Введите количество курьеров на скутерах.");
            int quantitySC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantitySC; i++)
            {
                Company.CouriersList.Add(new ScuterCourier(i));
            }
            Console.WriteLine("Введите количество курьеров на машинах.");
            int quantityCC = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantityCC; i++)
            {
                Company.CouriersList.Add(new CarCourier(i));
            }
            Company.quantityC = quantityFC + quantityBC + quantitySC + quantityCC;
            Company.Couriers = Company.CouriersList.ToArray();
            var order1 = new OrderForDelivery(CoordHelper.RandCoord(), CoordHelper.RandCoord(), "12:00", 3);
            var order2 = new OrderForTaking(CoordHelper.RandCoord(), CoordHelper.RandCoord(), "12:00", 7);
            foreach (var courier in Company.CouriersList)
            {

            }
        }
    }
}