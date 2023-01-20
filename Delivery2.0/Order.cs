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

        public double Distance { get { return Start.GetDistance(End); } }

        public int Coast { get { return GetOrderPrice(); } }

        public int Profit { get { return ActualeVariant != null ? ActualeVariant.Profit : 0; } }

        public TimeSpan Time { get; set; }

        private Queue<Variant> Variants = new();

        protected Variant ActualeVariant;

        public static event EventHandler<OrderEventDescriptor> NewOrderEvent;

        /// <summary>
        /// Считает стоимость заказа.
        /// </summary>
        protected int GetOrderPrice()
        {
            return (int)Math.Round(Distance * Company.PricePerDistance);
        }
        /// <summary>
        /// Получение вариантов возможнох позиций в расписании.
        /// </summary>
        public void TakeVariants(IList<Variant> variants)
        {
            foreach (var variant in variants)
                Variants.Enqueue(variant);
        }
        /// <summary>
        /// Распределение нового заказа.
        /// </summary>
        public void Destribute()
        {
            var variants = CollectingVariants();
            //Проверка, есть ли у заказа какие-то варианты, если нету, то заказ переходит в список непринятых.
            if (variants.Count == 0)
            {
                Company.RejectedOrders.Add(this);
                return;
            }
            //Подсчёт профитов расписаний при каждом из возможных вариантов расстановки.
            var profits = CalcProfitOfVariants(variants);
            //Применение варианта с наибольн=шим профитом для расписания.
            UseVariant(variants[profits.IndexOf(profits.Max())]);
        }
        public void Redestribute()
        {
            var variants = CollectingVariants();
            //Удаление последнего варианта расстановки, и варианта, который проверяет заказ в данный момент.
            RemoveActualeVariants(variants);
            //Проверка, есть ли у заказа какие-то варианты, если нету, то заказ переходит в список непринятых.
            if (variants.Count == 0)
            {
                Company.RejectedOrders.Add(this);
                return;
            }
            if(Company.CheckSearchDepth())
            {
            //Подсчёт профитов расписаний при каждом из возможных вариантов расстановки.
            var profits = CalcProfitOfVariants(variants);
            //Применение варианта с наибольн=шим профитом для расписания.
            UseVariant(variants[profits.IndexOf(profits.Max())]);
            }
            else
            {
                var variantss = variants.OrderByDescending(x => x.Profit);
                UseVariant(variantss.First());
            }
        }
        /// <summary>
        /// Установка нового актуального варианта.
        /// </summary>
        public void SetActualeVariant(Variant variant, TimeSpan time)
        {
            ActualeVariant = variant;
            Time = time;
        }
        /// <summary>
        /// Исполнение варианта.
        /// </summary>
        public void UseVariant(Variant variant)
        {
            variant.Courier.AttachingOrder(this, variant);
        }
        /// <summary>
        /// Сооздание копии заказа.
        /// </summary>
        public Order Copy()
        {
            if (this is OrderForDelivery)
                return new OrderForDelivery(Id, Start, End, DeadLine, Weigth, Time, ActualeVariant);
            else
                return new OrderForTaking(Id, Start, End, DeadLine, Weigth, Time, ActualeVariant);
        }
        /// <summary>
        /// Удаление варианта, на котором заказ стоял до перераспределения.
        /// </summary>
        private void RemoveActualeVariants(List<Variant> variants)
        {
            foreach (var variant in variants)
                if (ActualeVariant.Is(variant))
                {
                    variants.Remove(variant);
                    break;
                }
        }
        /// <summary>
        /// Считает профит расписания, при выборе каждого из вариантов, и возвращает списов крофитов, соответствующих вариантов.
        /// </summary>
        private List<int> CalcProfitOfVariants(List<Variant> variants)
        {
            Console.WriteLine($"Заказ {Id} начинает смотреть свои {variants.Count} вариантов");
            List<int> profits = new();
            //Считаем профит каждого варианта.
            foreach (var variant in variants)
            {
                var altSchedule = new AltSchedule();
                profits.Add(altSchedule.CalcProfitAltSchedule(this, variant));
            }
            Console.WriteLine($"Заказ {Id} заканчивает смотреть свои {variants.Count} вариантов");
            return profits;
        }
        /// <summary>
        /// Создание списка вариантов подстановки.
        /// </summary>
        /// <returns></returns>
        private List<Variant> CollectingVariants()
        {
            NewOrderEvent.Invoke(this, new OrderEventDescriptor { Order = this });
            var variants = new List<Variant>();
            while (Variants.Count > 0)
                variants.Add(Variants.Dequeue());
            return variants;
        }
    }
}
