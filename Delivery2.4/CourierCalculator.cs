using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal class CourierCalculator
    {
        /// <summary>
        /// Находит максимально возможный профит, который сможет принести курьер, если возьмёт заказ.
        /// </summary>
        public static void CalculateMaxProfit(OrderForTaking order, int numCourier)
        {
            //Проверяется, может ли курьер выполнить заказ.
            if (!CourierChecker.CheckCombination(order, Company.Couriers[numCourier]))
            {
                Company.Couriers[numCourier].Profit = 0;
                return;
            }
            //Создаётся и заполняется список координат, с который курьер может начать выполнение заказа.
            List<Coord> coords = new List<Coord>();
            coords.Add(Company.Couriers[numCourier].Start);
            foreach (var orderForCoord in Company.Couriers[numCourier].Orders)
            {
                coords.Add(orderForCoord.End);
            }
            //находится максимальный профит.
            int maxProfit = ProfitOfOrder(order, Company.Couriers[numCourier], coords[0]);
            int numberPrioritiCoord = 0;
            for (int i = 1; i < coords.Count - 1; i++)
            {
                if (ProfitOfOrder(order, Company.Couriers[numCourier], coords[i]) > maxProfit)
                {
                    maxProfit = ProfitOfOrder(order, Company.Couriers[numCourier], coords[i]);
                    numberPrioritiCoord = i;
                }
            } 
            if (CourierChecker.CheckCombinationFromEnd(order, Company.Couriers[numCourier]))
            {
                if (ProfitOfOrder(order, Company.Couriers[numCourier], coords[^1]) > maxProfit)
                {
                    maxProfit = ProfitOfOrder(order, Company.Couriers[numCourier], coords[^1]);
                    numberPrioritiCoord = coords.Count - 1;
                }
            }
            //Курьеру записывается номер координаты, начав с которой профит будет максимален.
            Company.Couriers[numCourier].NumberPrioritiCoord = numberPrioritiCoord;
            Company.Couriers[numCourier].Profit = maxProfit;
        }
        /// <summary>
        /// Находит максимально возможный профит, который сможет принести курьер, если возьмёт заказ.
        /// </summary>
        public static void CalculateMaxProfit(OrderForDelivery order, int numCourier)
        {
            //Проверяется, может ли курьер выполнить заказ.
            if (!CourierChecker.CheckCombination(order, Company.Couriers[numCourier]))
            {
                Company.Couriers[numCourier].Profit = 0;
                return;
            }
            //Создаётся и заполняется список координат, с который курьер может начать выполнение заказа.
            List<Coord> coords = new List<Coord>();
            coords.Add(Company.Couriers[numCourier].Start);
            foreach (var orderForCoord in Company.Couriers[numCourier].Orders)
            {
                coords.Add(orderForCoord.End);
            }
            //находится максимальный профит.
            int maxProfit = ProfitOfOrder(order, Company.Couriers[numCourier], coords[0]);
            int numberPrioritiCoord = 0;
            for (int i = 1; i < coords.Count - 1; i++)
            {
                if (ProfitOfOrder(order, Company.Couriers[numCourier], coords[i]) > maxProfit)
                {
                    maxProfit = ProfitOfOrder(order, Company.Couriers[numCourier], coords[i]);
                    numberPrioritiCoord = i;
                }
            } 
            if (CourierChecker.CheckCombinationFromEnd(order, Company.Couriers[numCourier]))
            {
                if (ProfitOfOrder(order, Company.Couriers[numCourier], coords[^1]) > maxProfit)
                {
                    maxProfit = ProfitOfOrder(order, Company.Couriers[numCourier], coords[^1]);
                    numberPrioritiCoord = coords.Count - 1;
                }
            }
            //Курьеру записывается номер координаты, начав с которой профит будет максимален.
            Company.Couriers[numCourier].NumberPrioritiCoord = numberPrioritiCoord;
            Company.Couriers[numCourier].Profit = maxProfit;
        }
        /// <summary>
        /// Считает профит для заказа от данной координаты, если его будет доставлять данный курьер.
        /// </summary>
        private static int ProfitOfOrder(Order order, Courier courier, Coord coord)
        {
            return (int)Math.Round(order.Coast - (order.Distance + CoordHelper.GetDistance(order.Start, coord)) * courier.Price);
        }
    }
}
