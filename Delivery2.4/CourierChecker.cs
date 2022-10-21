using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal static class CourierChecker
    {
        /// <summary>
        /// Проверяет, может ли курьер взять заказ.
        /// </summary>
        public static bool CheckCombination(Order order, Courier courier)
        {
            if (order is OrderForDelivery)
            {
                return (courier.Capacity >= order.Weigth)
                && (courier.StartTime + TimeCalculator.TimeToCompliteOrder(order, courier) < order.DeadLine)
                && (courier.StartTime + TimeCalculator.TimeToCompliteOrder(order, courier) < courier.EndTime);
            }
            else
            {
                return (courier.Capacity >= order.Weigth)
                && (courier.StartTime + TimeCalculator.TimeToWay(order, courier) < order.DeadLine)
                && (courier.StartTime + TimeCalculator.TimeToCompliteOrder(order, courier) < courier.EndTime);
            }
        }
        /// <summary>
        /// Проверяет, может ли курьер взять заказ в конец списка.
        /// </summary>
        public static bool CheckCombinationFromEnd(Order order, Courier courier)
        {
            if (order is OrderForDelivery)
            {
                return (courier.StartTime + courier.BusyTime + TimeCalculator.TimeToCompliteOrder(order, courier) < order.DeadLine)
                && (courier.StartTime + courier.BusyTime + TimeCalculator.TimeToCompliteOrder(order, courier) < courier.EndTime);
            }
            else
            {
                return (courier.StartTime + courier.BusyTime + TimeCalculator.TimeToWay(order, courier) < order.DeadLine)
                && (courier.StartTime + courier.BusyTime + TimeCalculator.TimeToCompliteOrder(order, courier) < courier.EndTime);
            }
        }
    }
}