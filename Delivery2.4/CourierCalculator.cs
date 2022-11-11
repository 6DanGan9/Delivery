using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal static class CourierCalculator
    {
        /// <summary>
        /// Возвращает заказ с отсортированным по профитности списком возможных вариантов прикрепления к курьерам.
        /// </summary>
        public static Order CalculateVariants(Order order)
        {
            for (int i = 0; i < Company.quantityC; i++)
            {
                int quantityCoord = Company.Couriers[i].Orders.Count + 1;
                for (int j = 0; j < quantityCoord; j++)
                {
                    if (Company.Couriers[i].CheckCombination(order, j))
                    {
                        int profit;
                        if (j == 0)
                        {
                            profit = Company.Couriers[i].CalcutuateProfitOfOrder(order, Company.Couriers[i].Start);
                        }
                        else
                        {
                            profit = Company.Couriers[i].CalcutuateProfitOfOrder(order, Company.Couriers[i].Orders[j - 1].End);
                        }
                        Variant newVariant = new Variant(Company.Couriers[i], profit, j);
                        order.Variants.Add(newVariant);
                    }
                }
            }
            order.SortVariantsByProfit();
            return order;
        }
        /// <summary>
        /// Считает профит для заказа от данной координаты, если его будет доставлять данный курьер.
        /// </summary>
        public static int CalcutuateProfitOfOrder(this Courier courier, Order order,  Coord coord)
        {
            return (int)Math.Round(order.Coast - (order.Distance + CoordHelper.GetDistance(order.Start, coord)) * courier.Price);
        }
    }
}
