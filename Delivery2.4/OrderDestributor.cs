using System;
using System.Collections.Generic;
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
            for (int i = 0; i < Company.quantityC; i++)
            {
                CourierCalculator.CalculateMaxProfit(order, i);
            }
            Courier[] couriers = new Courier[Company.quantityC];
            Array.Sort(couriers);
        }
        /// <summary>
        /// Распределяет заказ.
        /// </summary>
        public static void Distributoin(OrderForTaking order)
        {

        }
    }
}
