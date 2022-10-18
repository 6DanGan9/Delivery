using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal class ScuterCourier : Courier
    {
        public ScuterCourier(string num)
        {
            Name = $"Курьер на скутере № {num}";
            Start = CoordHelper.RandCoord();
            StartTime = CourierScheduleStart();
            EndTime = CourierScheduleEnd();
            Speed = Company.DefaultFootCurierSpeed;
            Capacity = Company.DefaultFootCurierCapacity;
            Price = Company.DefaultFootCurierPrisePerDistance;
        }
    }
}
