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
        public static void Distributoin(OrderForDelivery order)
        {
            order = CourierCalculator.CalculateMaxProfit(order);
            //Ищет курьера, который примет заказ, если такой не находится, то закидывает заказ в список непринятых.
            foreach (var courier in order.Couriers)
            {
                order.Profit = courier.Profit;
                if (courier.Profit <= 0)
                {
                    Company.RejectedOrders.Add(order);
                    break;
                }
                if (Company.Couriers[courier.Courier.CourierID].AttachingOrder(order, courier.NumberPriorityCoord, courier.Profit))
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Распределяет заказ.
        /// </summary>
        public static void Distributoin(OrderForTaking order)
        {
            order = CourierCalculator.CalculateMaxProfit(order);
            //Ищет курьера, который примет заказ, если такой не находится, то закидывает заказ в список непринятых.
            for (int i = 0; i < order.Couriers.Count; i++)
            {
                order.Profit = order.Couriers[i].Profit;
                if (order.Profit <= 0)
                {
                    Company.RejectedOrders.Add(order);
                    break;
                }
                if (Company.Couriers[order.Couriers[i].Courier.CourierID].AttachingOrder(order, order.Couriers[i].NumberPriorityCoord, order.Couriers[i].Profit))
                {
                    break;
                }
                if (i == order.Couriers.Count)
                {
                    Company.RejectedOrders.Add(order);
                }
            }
        }
    }
}
