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
        /// Проверяет, может ли курьер взять заказ с заданной координаты.
        /// </summary>
        public static bool CheckCombination(this Courier courier, Order order, int numberOfCheckedCoord)
        {
            if (courier.Capacity < order.Weigth)
                return false;
            Coord coordForCheck;
            TimeSpan busyTime = TimeSpan.FromMinutes(0);
            if (numberOfCheckedCoord == 0)
                coordForCheck = courier.Start;
            else
                coordForCheck = courier.Orders[numberOfCheckedCoord - 1].End;
            for (int i = 0; i < numberOfCheckedCoord; i++)
            {
                busyTime += courier.Orders[i].Time;
            }
            if (courier.StartTime + busyTime + TimeCalculator.TimeToCompliteOrder(order, courier, coordForCheck) > courier.EndTime)
                return false;
            if (order is OrderForDelivery)
            {
                return (courier.StartTime + busyTime + TimeCalculator.TimeToCompliteOrder(order, courier, coordForCheck) < order.DeadLine);
            }
            else
            {
                return (courier.StartTime + busyTime + TimeCalculator.TimeToWay(order, courier, coordForCheck) < order.DeadLine);
            }
        }
    }
}