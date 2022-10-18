using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal class Company
    {
        public const double PricePerDistance = 200;

        public const double DefaultFootCurierSpeed = 7;
        public const double DefaultFootCurierCapacity = 4;
        public const double DefaultFootCurierPrisePerDistance = 40;

        public const double DefaultBikeCurierSpeed = 15;
        public const double DefaultBikeCurierCapacity = 6;
        public const double DefaultBikeCurierPrisePerDistance = 40;

        public const double DefaultScuterCurierSpeed = 30;
        public const double DefaultScuterCurierCapacity = 8;
        public const double DefaultScyterCurierPrisePerDistance = 40;

        public const double DefaultCarCurierSpeed = 25;
        public const double DefaultCarCurierCapacity = 60;
        public const double DefaultCarCurierPrisePerDistance = 40;

        public static List<Courier> CouriersList = new();
        public static int quantityC;
        public static Courier[] couriers = new Courier[quantityC];
    }
}
