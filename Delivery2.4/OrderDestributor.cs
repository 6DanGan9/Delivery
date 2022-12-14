using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Распределитель заказов.
    /// </summary>
    internal static class OrderDestributor
    {
        /// <summary>
        /// Распределяет заказ.
        /// </summary>
        public static void Distributoin(Order order)
        {
            order.CalculateVariants();
            //Ищет курьера, который примет заказ, если такой не находится, то закидывает заказ в список непринятых.
            for (int i = 0; i < order.Variants.Count; i++)
            {
                order.Profit = order.Variants[i].Profit;
                if (order.Variants[i].Profit <= 0)
                {
                    order.SetActualeVariant(Company.NullVariant);
                    Company.RejectedOrders.Add(order);
                    Company.Orders.Remove(order);
                    break;
                }
                if (Company.Couriers[order.Variants[i].Courier.CourierID].CanAttachingOrder(order.Variants[i].NumberPriorityCoord, order.Variants[i].Profit))
                {
                    order.SetActualeVariant(order.Variants[i]);
                    Company.Couriers[order.Variants[i].Courier.CourierID].AttachingOrder(order, order.Variants[i].NumberPriorityCoord);
                    break;
                }
                if (i == order.Variants.Count - 1)
                {
                    order.SetActualeVariant(Company.NullVariant);
                    Company.RejectedOrders.Add(order);
                    Company.Orders.Remove(order);
                }
            }
            order.Variants.Clear();
        }
    }
}
