using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._0
{
    internal class OrderDestributor
    {
        public static List<Order> freeOrders = new();
        public static List<Order> rejectedOrders = new ();
        public static void Destribution(Order order)
        {
            int numberCourier = 0;
            Courier[] couriers = new Courier[CourierLogic.quantityC];
            for (int i = 0; i < CourierLogic.quantityC; i++)
            {
                couriers[i] = CourierLogic.couriers[i];
            }
            int[] profits = new int[CourierLogic.quantityC];
            for (int i = 0; i < CourierLogic.quantityC; i++)
            {
                profits[i] = CourierLogic.CheckCourier(order, i);
            }
            Array.Sort(profits, couriers);
            Array.Reverse(profits);
            Array.Reverse(couriers);
            if (profits[0] <= 0)
            {
                rejectedOrders.Add(order);
                return;
            }
            for (int i = 0; i < CourierLogic.quantityC; i++)
            {
                for (int j = 0; j < couriers.Length; j++)
                {
                    if (couriers[i].tip == CourierLogic.couriers[j].tip)
                    {
                        numberCourier = j;
                        break;
                    }
                }
                order.profit = profits[i];
                if (CourierLogic.AttachingOrder(order, numberCourier))
                {
                    break;
                }
                if (i == CourierLogic.quantityC - 1)
                {
                    rejectedOrders.Add(order);
                }
            }
        }
    }
}
