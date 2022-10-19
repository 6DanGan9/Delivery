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
    internal class CourierProfit : IComparable
    {
        //Метод сортировки.
        public int CompareTo(object obj)
        {
            CourierProfit courier1 = obj as CourierProfit;
            if (courier1 != null)
                return Profit.CompareTo(courier1.Profit);
            else
                throw new Exception("Не то...");
        }

        public Courier Courier { get; set; }

        public int Profit { get; set; }
        //Координата, с который курьер должен будет начать, если возьмёт этот заказ.
        public int NumberPriorityCoord { get; set; }
    }
}
