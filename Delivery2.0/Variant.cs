using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    /// <summary>
    /// Класс для хранения каждого возможного варианта вставки заказа.
    /// </summary>
    internal class Variant
    {
        public Variant(Courier courier, int profit, int numberPriorityCoord)
        {
            Courier = courier;
            Profit = profit;
            NumberPriorityCoord = numberPriorityCoord;
        }

        public Courier Courier { get; set; }

        public int Profit { get; set; }
        //Номер координаты, с который курьер должен будет начать, если возьмёт этот заказ.
        public int NumberPriorityCoord { get; set; }
        /// <summary>
        /// Проверка, являются ли 2 варианта одинаковыми.
        /// </summary>
        public bool Is(Variant variant)
        {
            if (Courier == variant.Courier && NumberPriorityCoord == variant.NumberPriorityCoord && Profit == variant.Profit)
                return true;
            else
                return false;
        }
    }
}
