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
        /// Возвращает заказ с отсортированным по профитности массивом курьеров.
        /// </summary>
        public static OrderForTaking CalculateMaxProfit(OrderForTaking order)
        {
            //Перебирает всех курьеров.
            for (int i = 0; i < Company.quantityC; i++)
            {
                //Проверяет, может ли курьер выполнить работу.
                if (CourierChecker.CheckCombination(order, Company.Couriers[i]))
                {
                    //Добавляет вариант доставки с начальной координаты.
                    CourierProfit variant = new();
                    variant.Courier = Company.Couriers[i];
                    variant.Profit = ProfitOfOrder(order, Company.Couriers[i], Company.Couriers[i].Start);
                    variant.NumberPriorityCoord = 0;
                    order.Couriers.Add(variant);
                    if (Company.Couriers[i].Orders.Count > 0)
                    {
                        for (int j = 1; j < Company.Couriers[i].Orders.Count - 1; j++)
                        {
                            //Добавляет варианты для конца каждого из заказов данного курьера.
                            CourierProfit variantCF = new();
                            variantCF.Courier = Company.Couriers[i];
                            variantCF.Profit = ProfitOfOrder(order, Company.Couriers[i], Company.Couriers[i].Orders[j - 1].End);
                            variantCF.NumberPriorityCoord = j;
                            order.Couriers.Add(variantCF);
                        }
                        //Проверяет, может ли быть взят заказ в хвост, если да, то добавляет вариант взятия в хвост.
                        if (CourierChecker.CheckCombinationFromEnd(order, Company.Couriers[i]))
                        {
                            CourierProfit variantCF = new();
                            variantCF.Courier = Company.Couriers[i];
                            variantCF.Profit = ProfitOfOrder(order, Company.Couriers[i], Company.Couriers[i].Orders[^1].End);
                            variantCF.NumberPriorityCoord = Company.Couriers[i].Orders.Count;
                            order.Couriers.Add(variantCF);
                        }
                    }
                }
            }
            //Сортирует все варианты.
            order.Couriers.Sort();
            order.Couriers.Reverse();
            return order;
        }
        /// <summary>
        /// Возвращает заказ с отсортированным по профитности массивом курьеров.
        /// </summary>
        public static OrderForDelivery CalculateMaxProfit(OrderForDelivery order)
        {
            //Перебирает всех курьеров.
            for (int i = 0; i < Company.quantityC; i++)
            {
                //Проверяет, может ли курьер выполнить работу.
                if (CourierChecker.CheckCombination(order, Company.Couriers[i]))
                {
                    //Добавляет вариант доставки с начальной координаты.
                    CourierProfit variant = new();
                    variant.Courier = Company.Couriers[i];
                    variant.Profit = ProfitOfOrder(order, Company.Couriers[i], Company.Couriers[i].Start);
                    variant.NumberPriorityCoord = 0;
                    order.Couriers.Add(variant);
                    if (Company.Couriers[i].Orders.Count > 0)
                    {
                        for (int j = 1; j < Company.Couriers[i].Orders.Count - 1; j++)
                        {
                            //Добавляет варианты для конца каждого из заказов данного курьера.
                            CourierProfit variantCF = new();
                            variantCF.Courier = Company.Couriers[i];
                            variantCF.Profit = ProfitOfOrder(order, Company.Couriers[i], Company.Couriers[i].Orders[j - 1].End);
                            variantCF.NumberPriorityCoord = j;
                            order.Couriers.Add(variantCF);
                        }
                        //Проверяет, может ли быть взят заказ в хвост, если да, то добавляет вариант взятия в хвост.
                        if (CourierChecker.CheckCombinationFromEnd(order, Company.Couriers[i]))
                        {
                            CourierProfit variantCF = new();
                            variantCF.Courier = Company.Couriers[i];
                            variantCF.Profit = ProfitOfOrder(order, Company.Couriers[i], Company.Couriers[i].Orders[^1].End);
                            variantCF.NumberPriorityCoord = Company.Couriers[i].Orders.Count;
                            order.Couriers.Add(variantCF);
                        }
                    }
                }
            }
            //Сортирует все варианты.
            order.Couriers.Sort();
            order.Couriers.Reverse();
            return order;
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
