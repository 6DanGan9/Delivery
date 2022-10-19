using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal static class CourierLogic
    {
        /// <summary>
        /// Проверяет, может ли выполнить курьер данный заказ.
        /// </summary>
        public static bool CheckCombination(OrderForTaking order, Courier courier)
        {
            return (courier.Capacity >= order.Weigth)
                && (courier.StartTime + courier.BusyTime + Calculator.TimeToWay(order, courier) < DateTime.Today + order.DeadLine)
                && (courier.StartTime + courier.BusyTime + Calculator.TimeToCompliteOrder(order, courier) < courier.EndTime);
        }
        /// <summary>
        /// Проверяет, может ли выполнить курьер данный заказ.
        /// </summary>
        public static bool CheckCombination(OrderForDelivery order, Courier courier)
        {
            return (courier.Capacity >= order.Weigth)
                && (courier.StartTime + courier.BusyTime + Calculator.TimeToCompliteOrder(order, courier) < DateTime.Today + order.DeadLine)
                && (courier.StartTime + courier.BusyTime + Calculator.TimeToCompliteOrder(order, courier) < courier.EndTime);
        }
    }
}
