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
        public static bool CheckCombination(OrderForTaking order, Courier courier)
        {
            return (courier.Capacity >= order.Weigth)
                && (courier.StartTime + TimeCalculator.TimeToWay(order, courier) < order.DeadLine)
                && (courier.StartTime + TimeCalculator.TimeToCompliteOrder(order, courier) < courier.EndTime);
        }
        /// <summary>
        /// Проверяет, может ли курьер взять заказ.
        /// </summary>
        public static bool CheckCombination(OrderForDelivery order, Courier courier)
        {
            return (courier.Capacity >= order.Weigth)
                && (courier.StartTime + TimeCalculator.TimeToCompliteOrder(order, courier) < order.DeadLine)
                && (courier.StartTime + TimeCalculator.TimeToCompliteOrder(order, courier) < courier.EndTime);
        }
        /// <summary>
        /// Проверяет, может ли курьер взять заказ в конец списка.
        /// </summary>
        public static bool CheckCombinationFromEnd(OrderForTaking order, Courier courier)
        {
            return (courier.StartTime + courier.BusyTime + TimeCalculator.TimeToWay(order, courier) < order.DeadLine)
                && (courier.StartTime + courier.BusyTime + TimeCalculator.TimeToCompliteOrder(order, courier) < courier.EndTime);
        }
        /// <summary>
        /// Проверяет, может ли курьер взять заказ в конец списка.
        /// </summary>
        public static bool CheckCombinationFromEnd(OrderForDelivery order, Courier courier)
        {
            return (courier.StartTime + courier.BusyTime + TimeCalculator.TimeToCompliteOrder(order, courier) < order.DeadLine)
                && (courier.StartTime + courier.BusyTime + TimeCalculator.TimeToCompliteOrder(order, courier) < courier.EndTime);
        }
    }
}
