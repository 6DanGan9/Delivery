using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Типичный заказ.
    /// </summary>
    internal abstract class Order
    {
        public int Id { get; protected set; }
        public Coord Start { get; protected set; }

        public Coord End { get; protected set; }

        public double Weigth { get; protected set; }

        public DateTime DeadLine { get; protected set; }

        public List<Variant> Variants = new();

        private Variant ActualeVariant;

        public double Distance { get { return Start.GetDistance(End); } }

        public int Coast { get { return GetOrderPrice(); } }

        public int Profit { get; set; }

        public TimeSpan Time { get; set; }

        /// <summary>
        /// Считает стоимость заказа.
        /// </summary>
        protected int GetOrderPrice()
        {
            return (int)Math.Round(Distance * Company.PricePerDistance);
        }
        /// <summary>
        /// Сортирует все варианты по профитности.
        /// </summary>
        public void SortVariantsByProfit()
        {
            var variants = Variants.OrderByDescending(x => x.Profit);
            Variants = new();
            foreach (var variant in variants)
                Variants.Add(variant);
        }
        public void SetActualeVariant(Variant variant)
        {
            ActualeVariant = variant;
        }
        public bool CheckRelevanceOfPosition()
        {
            int i = 0;
            this.CalculateVariants();
            while (Variants[i].Profit > ActualeVariant.Profit)
            {
                if (!((Variants[i].Courier == ActualeVariant.Courier) && (Variants[i].NumberPriorityCoord >= ActualeVariant.NumberPriorityCoord)))
                {
                    if (Variants[i].Courier.CanAttachingOrder(Variants[i].NumberPriorityCoord, Variants[i].Profit))
                    {
                        ActualeVariant.Courier.DismissOrder(Id);
                        ActualeVariant = Variants[i];
                        Profit = ActualeVariant.Profit;
                        Variants[i].Courier.AttachingOrder(this, Variants[i].NumberPriorityCoord);
                        Company.DestributeFreeOrders();
                        return true;
                    }
                }
                i++;
            }
            Variants.Clear();
            return false;
        }
    }
}
