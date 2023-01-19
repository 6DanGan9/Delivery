using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    internal abstract class Courier
    {
        public int CourierID { get; protected set; }

        public string Name { get; protected set; }

        public Coord Start { get; protected set; }

        public double Capacity { get; protected set; }

        public double Speed { get; protected set; }

        public double Price { get; protected set; }

        public DateTime StartTime { get { return CourierScheduleStart(); } }

        public DateTime EndTime { get { return CourierScheduleEnd(); } }

        public List<Order> Orders = new();

        private List<int> LockedVariants = new();

        public TimeSpan BusyTime { get { return CalculateBusyTime(); } }

        public event EventHandler<CourierEventDescriptor> DismissFreeOrder;

        /// <summary>
        /// Считает суммарное время, которое курьер потратит на выполнение всех заказов в его списке заказов.
        /// </summary>
        protected TimeSpan CalculateBusyTime()
        {
            TimeSpan time = TimeSpan.FromMinutes(0);
            if ((Orders != null) && (Orders.Count > 0))
            {
                foreach (Order order in Orders)
                {
                    time += order.Time;
                }
                return time;
            }
            else
            {
                return time;
            }
        }
        /// <summary>
        /// Задаёт время начала работы курьера.
        /// </summary>
        protected DateTime CourierScheduleStart()
        {
            DateTime dateTime = DateTime.Today;
            switch (CourierID % 3)
            {
                case 0:
                    TimeSpan time1 = TimeSpan.FromHours(8);
                    dateTime += time1;
                    break;
                case 1:
                    TimeSpan time2 = TimeSpan.FromHours(12);
                    dateTime += time2;
                    break;
                case 2:
                    TimeSpan time3 = TimeSpan.FromHours(16);
                    dateTime += time3;
                    break;
            }
            return dateTime;
        }
        /// <summary>
        /// Задаёт время конца работы курьера.
        /// </summary>
        protected DateTime CourierScheduleEnd()
        {
            DateTime dateTime = DateTime.Today;
            switch (CourierID % 3)
            {
                case 0:
                    TimeSpan time1 = TimeSpan.FromHours(16);
                    dateTime += time1;
                    break;
                case 1:
                    TimeSpan time2 = TimeSpan.FromHours(20);
                    dateTime += time2;
                    break;
                case 2:
                    TimeSpan time3 = TimeSpan.FromHours(24);
                    dateTime += time3;
                    break;
            }
            return dateTime;
        }
        /// <summary>
        /// Проверяет, может ли курьер выполнить данный заказ.
        /// </summary>
        private bool CanCarry(Order order, int numberOfCheckedCoord)
        {
            //Проверяется грузоподъёмность.
            if (Capacity < order.Weigth)
                return false;
            Coord coordForCheck;
            TimeSpan busyTime = TimeSpan.FromMinutes(0);
            if (numberOfCheckedCoord == 0)
                coordForCheck = Start;
            else
                coordForCheck = Orders[numberOfCheckedCoord - 1].End;
            //Считается время, которое курьер потрадтит на заказы, до этого варианта подстановки.
            for (int i = 0; i < numberOfCheckedCoord; i++)
            {
                busyTime += Orders[i].Time;
            }
            //Считает, успеет ли курьер выполнить заказ до конца рабочего дня, и уложиться в ограничение по времени самого заказа.
            if (StartTime + busyTime + TimeCalculator.TimeToCompliteOrder(order, this, coordForCheck) > EndTime)
                return false;
            if (order is OrderForDelivery)
            {
                return (StartTime + busyTime + TimeCalculator.TimeToCompliteOrder(order, this, coordForCheck) < order.DeadLine);
            }
            else
            {
                return (StartTime + busyTime + TimeCalculator.TimeToWay(order, this, coordForCheck) < order.DeadLine);
            }
        }
        /// <summary>
        /// Обработка заказа, и передача ему возможных вариантов подстановки.
        /// </summary>
        private void NewOrderEventComeEventHandler(object? sender, OrderEventDescriptor e)
        {
            List<Coord> coords = new();
            List<Variant> variants = new();
            var order = e.Order;
            //Сбор координат, с которых курьер может начать выполнение заказа.
            coords.Add(Start);
            foreach (var ord in Orders)
                coords.Add(ord.End);
            Console.WriteLine($"Курьер {Name}: Получил событие появления Заказа: {order.Id}");
            //Сбор списка вариантов, которые курьер может предложить заказу.
            for (int i = 0; i <= Orders.Count; i++)
            {
                if (CanCarry(order, i))
                {
                    int profit = (int)Math.Round(order.Coast - (coords[i].GetAllDistance(order)) * Price);
                    var variant = new Variant(this, profit, i);
                    variants.Add(variant);
                }
            }
            //Удаление вариантов с отрицательным профитом и вариантов, которые заблокированы.
            if (variants != null)
                foreach (var variant in variants.ToList())
                {
                    if (variant.Profit < 0)
                    {
                        variants.Remove(variant);
                        continue;
                    }
                    if (LockedVariants.Count > 0 && variant.NumberPriorityCoord <= LockedVariants.Max())
                        variants.Remove(variant);
                }
            //Передача вариантов заказу.
            order.TakeVariants(variants);
        }
        /// <summary>
        /// Все заказы начиная с того, вместо которого хочет встать заказ, переходят в список свободных заказов, а заказ встаёт на их место.
        /// </summary>
        public void AttachingOrder(Order order, Variant variant)
        {
            LockVariant(variant);
            //Подсчёт количества заказов, которые нужно направить на перераспределение.
            int quantityOrders = (Orders.Count - variant.NumberPriorityCoord);
            //Передача заказорв на перераспределение.
            for (int i = 0; i < quantityOrders; i++)
            {
                Company.FreeOrders.Push(Orders[^1]);
                Orders.RemoveAt(Orders.Count - 1);
            }
            //Подсчём времени на выполнение заказа.
            TimeSpan time;
            if (variant.NumberPriorityCoord != 0)
                time = TimeCalculator.TimeToCompliteOrder(order, this, Orders[^1].End);
            else
                time = TimeCalculator.TimeToCompliteOrder(order, this, Start);
            //Установка нового актуального варианта заказа.
            order.SetActualeVariant(variant, time);
            Console.WriteLine($"Kyp {Name} добавляет заказ {order.Id} на {variant.NumberPriorityCoord} место");
            //Добавление заказа в список выполняемых.
            Orders.Add(order);
            //Сообщение о том, что какие-то заказ были направлены на перераспределение.
            if (quantityOrders != 0)
                DismissFreeOrder.Invoke(this, new CourierEventDescriptor { Courier = this });
            UnlockVariant(variant);
        }
        /// <summary>
        /// Изначальная инициальзация курьера.
        /// </summary>
        public void Intilize()
        {
            Order.NewOrderEvent += NewOrderEventComeEventHandler;
            DismissFreeOrder += Company.DestributeFreeOrders;
        }
        /// <summary>
        /// Блокирует варианты.
        /// </summary>
        private void LockVariant(Variant variant)
        {
            LockedVariants.Add(variant.NumberPriorityCoord);
        }
        /// <summary>
        /// Снимает блокировку варианта.
        /// </summary>
        private void UnlockVariant(Variant variant)
        {
            LockedVariants.Remove(variant.NumberPriorityCoord);
        }
    }
}
