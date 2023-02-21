using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    internal class AltSchedule
    {
        private List<Order>[] OrigSchedule = new List<Order>[Company.QuantityC];
        private Queue<Order> OrigRejectedOrders = new();
        private Queue<Order> OrigFreeOrders = new();
        /// <summary>
        /// Считает профит расписания, если заказ исполюзует данный вариант.
        /// </summary>
        public int CalcProfitAltSchedule(Order order, Variant variant)
        {
            Company.SearchDepthUp();
            //Сохраняем расписание
            SaveOriginalSchedule();
            //Исполняем выбранный вариант.
            order.UseVariant(variant);
            if (Company.FreeOrders.Count > 0)
                Company.RedestributeFreeOrders();
            //Считаем профит полученного расписания.
            int profit = Company.FullProfit();
            //Восстанавливаем расписание.
            ResetSchedule();
            Company.SearchDepthDown();
            return profit;
        }
        /// <summary>
        /// Сохраняет изначальное расписание.
        /// </summary>
        private void SaveOriginalSchedule()
        {
            for(int i = 0; i < Company.Couriers.Length; i++)
            {
                OrigSchedule[i] = new();
                for (int j = 0; j < Company.Couriers[i].Orders.Count; j++)
                {
                    OrigSchedule[i].Add(Company.Couriers[i].Orders[j].Copy());
                }
            }
            foreach (var ord in Company.RejectedOrders)
                OrigRejectedOrders.Enqueue(ord.Copy());
            foreach (var ord in Company.FreeOrders)
                OrigFreeOrders.Enqueue(ord.Copy());
        }
        /// <summary>
        /// Востанавливает расписание в изначальный вид.
        /// </summary>
        private void ResetSchedule()
        {
            for (int i = 0; i < OrigSchedule.Length; i++)
            {
                Company.Couriers[i].Orders.Clear();
                for (int j = 0; j < OrigSchedule[i].Count; j++)
                {
                    Company.Couriers[i].Orders.Add(OrigSchedule[i][j].Copy());
                }
            }
            Company.RejectedOrders.Clear();
            while (OrigRejectedOrders.Count > 0)
            {
                Company.RejectedOrders.Add(OrigRejectedOrders.Dequeue());
            }
            Company.FreeOrders.Clear();
            while (OrigFreeOrders.Count > 0)
            {
                Company.FreeOrders.Push(OrigFreeOrders.Dequeue());
            }
        }
    }
}
