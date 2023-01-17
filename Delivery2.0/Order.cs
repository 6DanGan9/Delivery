using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    internal abstract class Order
    {
        public int Id { get; protected set; }
        public Coord Start { get; protected set; }

        public Coord End { get; protected set; }

        public double Weigth { get; protected set; }

        public DateTime DeadLine { get; protected set; }

        private Queue<Variant> Variants = new();

        protected Variant ActualeVariant;

        private Stack<Variant> CheckedVariants = new();

        public double Distance { get { return Start.GetDistance(End); } }

        public int Coast { get { return GetOrderPrice(); } }

        public int Profit { get { return ActualeVariant != null ? ActualeVariant.Profit : 0 ; } }

        public TimeSpan Time { get; set; }

        public static event EventHandler<OrderEventDescriptor> NewOrderEvent;

        /// <summary>
        /// Считает стоимость заказа.
        /// </summary>
        protected int GetOrderPrice()
        {
            return (int)Math.Round(Distance * Company.PricePerDistance);
        }

        public void TakeVariants(IList<Variant> variants)
        {
            foreach (var variant in variants)
                Variants.Enqueue(variant);
        }

        internal void Destribute()
        {
            NewOrderEvent.Invoke(this, new OrderEventDescriptor { Order = this });
            var variants = new List<Variant>();
            while (Variants.Count > 0)
                variants.Add(Variants.Dequeue());
            if (variants.Count == 0)
            {
                Company.RejectedOrders.Add(this);
                return;
            }
            var profits = CalcProfitOfVariants(variants);
            variants[profits.IndexOf(profits.Max())].Courier.AttachingOrder(this, variants[profits.IndexOf(profits.Max())]);
        }
        internal void Redestribute()
        {
            NewOrderEvent.Invoke(this, new OrderEventDescriptor { Order = this });
            var variants = new List<Variant>();
            while (Variants.Count > 0)
                variants.Add(Variants.Dequeue());
            variants = RemoveActualeVariants(variants);
            variants = RemoveCheckedVariants(variants);
            if (variants.Count == 0)
            {
                Company.RejectedOrders.Add(this);
                return;
            }
            var profits = CalcProfitOfVariants(variants);
            variants[profits.IndexOf(profits.Max())].Courier.AttachingOrder(this, variants[profits.IndexOf(profits.Max())]);
        }

        private List<Variant> RemoveCheckedVariants(List<Variant> variants)
        {
            foreach (var checkedVariant in CheckedVariants)
            {
                foreach (var variant in variants)
                    if (checkedVariant.Is(variant))
                    {
                        variants.Remove(variant);
                        break;
                    }
            }
            return variants;
        }

        private List<Variant> RemoveActualeVariants(List<Variant> variants)
        {
            foreach (var variant in variants)
                if (ActualeVariant.Is(variant))
                {
                    variants.Remove(variant);
                    break;
                }
            return variants;
        }
        private List<int> CalcProfitOfVariants(List<Variant> variants)
        {
            Console.WriteLine($"Заказ {Id} начинает смотреть свои {variants.Count} вариантов");
            List<int> profits = new();
            foreach (var variant in variants)
            {
                var altSchedule = new AltSchedule();
                CheckedVariants.Push(variant);
                profits.Add(altSchedule.CalcProfitAltSchedule(this, variant));
                CheckedVariants.Pop();
            }
            Console.WriteLine($"Заказ {Id} заканчивает смотреть свои {variants.Count} вариантов");
            return profits;
        }

        public Order Copy()
        {
            if (this is OrderForDelivery)
                return new OrderForDelivery(Id, Start, End, DeadLine, Weigth, Time, ActualeVariant);
            else
                return new OrderForTaking(Id, Start, End, DeadLine, Weigth, Time, ActualeVariant);
        }

        public void SetActualeVariant(Variant variant, TimeSpan time)
        {
            ActualeVariant = variant;
            Time = time;
        }
    }
}
