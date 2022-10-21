using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Класс для хранения каждого возможного варианта вставки заказа.
    /// </summary>
    internal class Variant : IComparable
    {
        //Метод сортировки.
        public int CompareTo(object obj)
        {
            Variant variant = obj as Variant;
            if (variant != null)
                return Profit.CompareTo(variant.Profit);
            else
                throw new Exception("Не то...");
        }

        public Variant(Courier courier, int profit, int numberPriorityCoord)
        {
            Courier = courier;
            Profit = profit;
            NumberPriorityCoord = numberPriorityCoord;
        }

        public Courier Courier { get; set; }

        public int Profit { get; set; }
        //Координата, с который курьер должен будет начать, если возьмёт этот заказ.
        public int NumberPriorityCoord { get; set; }
    }
}
