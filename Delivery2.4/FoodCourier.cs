using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal class FootCourier : Courier
    {
        public FootCourier(string num)
        {
            Name = $"Пеший курьер № {num}";
            Start = CoordHelper.RandCoord();
            StartTime = CourierScheduleStart();
            EndTime = CourierScheduleEnd();
            Speed = Company.DefaultFootCurierSpeed;
            Capacity = Company.DefaultFootCurierCapacity;
            Price = Company.DefaultFootCurierPrisePerDistance;
        }
    }
}
