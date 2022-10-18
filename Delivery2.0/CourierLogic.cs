using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._0
{
    internal class CourierLogic
    {
        public static List<Courier> couriersList = new();
        public static int quantityC;
        public static Courier[] couriers = new Courier[quantityC];
        public static int CheckCourier(Order order, int number)
        {
            if (couriers[number].capacity < order.mass)
            {
                return 0;
            }
            int Variants = 1;
            for (int i = 0; i < couriers[number].orders.Count; i++)
            {
                couriers[number].coords.Add(couriers[number].orders[i].coord2);
                Variants++;
            }
            double[] distances = new double[Variants];
            for (int i = 0; i < Variants; i++)
            {
                distances[i] = References.Distace(couriers[number].coords[i], order.coord1);
            }
            double minDistance = 9999999;
            for (int i = 0; i < Variants; i++)
            {
                if (minDistance > distances[i])
                {
                    minDistance = distances[i];
                }
            }
            couriers[number].numberMin = Array.IndexOf(distances, minDistance);
            if (couriers[number].numberMin == couriers[number].orders.Count)
            {
                order.time = References.TimeToOrder(order, couriers[number], distances[couriers[number].numberMin]);
                if ((couriers[number].startTime + Courier.BusyTime(couriers[number]) + order.time > order.deadline)
                || (couriers[number].startTime + Courier.BusyTime(couriers[number]) + order.time > couriers[number].endTime))
                {
                    distances[couriers[number].numberMin] = 9999999;
                    minDistance = 9999999;
                    for (int i = 0; i < Variants; i++)
                    {
                        if (minDistance > distances[i])
                        {
                            minDistance = distances[i];
                        }
                    }
                    couriers[number].numberMin = Array.IndexOf(distances, minDistance);
                }
            }
            Coord coord = couriers[number].coords[0];
            couriers[number].coords.Clear();
            couriers[number].coords.Add(coord);
            if ((minDistance == 9999999) || (References.Profit(order, couriers[number], minDistance) < 0))
            {
                return 0;
            }
            return References.Profit(order, couriers[number], minDistance);
        }
        public static void ArrayCouriers()
        {
            couriers = couriersList.ToArray();
        }
        public static bool AttachingOrder(Order order, int number)
        {
            if ((couriers[number].orders.Count > couriers[number].numberMin)
                && (order.profit <= couriers[number].orders[couriers[number].numberMin].profit))
            {
                return false;
            }
            int quantityOrders = (couriers[number].orders.Count - couriers[number].numberMin);
            for (int i = 0; i < quantityOrders; i++)
            {
                OrderDestributor.freeOrders.Add(couriers[number].orders[^1]);
                couriers[number].orders.RemoveAt(couriers[number].orders.Count - 1);
            }
            order.time = References.TimeToOrder(order, couriers[number], References.Distace(order.coord1, couriers[number].coords[0]));
            couriers[number].orders.Add(order);
            return true;
        }
    }
}
