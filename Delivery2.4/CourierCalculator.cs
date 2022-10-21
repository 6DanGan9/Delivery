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
        /// Возвращает заказ с отсортированным по профитности списком возможных вариантов прикрепления к курьерам.
        /// </summary>
        public static Order CalculateVariants(Order order)
        {
            for (int i = 0; i < Company.quantityC; i++)
            {
                if (CourierChecker.CheckCombination(order, Company.Couriers[i]))
                {
                    //Добавляет варианты для начальной координаты курьера.
                    order.Variants.Add( new Variant(Company.Couriers[i], ProfitOfOrder(order, Company.Couriers[i], Company.Couriers[i].Start), 0));
                    if (Company.Couriers[i].Orders.Count > 0)
                    {
                        //Добавляет варианты для конца каждого из заказов данного курьера.
                        for (int j = 1; j < Company.Couriers[i].Orders.Count - 1; j++)
                        {
                            order.Variants.Add(new Variant(Company.Couriers[i], ProfitOfOrder(order, Company.Couriers[i], Company.Couriers[i].Orders[j - 1].End), j));
                        }
                        //Проверяет, может ли быть взят заказ в хвост, если да, то добавляет вариант взятия в хвост.
                        if (CourierChecker.CheckCombinationFromEnd(order, Company.Couriers[i]))
                        {
                            order.Variants.Add(new Variant (Company.Couriers[i], ProfitOfOrder(order, Company.Couriers[i], Company.Couriers[i].Orders[^1].End), Company.Couriers[i].Orders.Count));
                        }
                    }
                }
            }
            //Сортирует все варианты.
            order.Variants.Sort();
            order.Variants.Reverse();
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
