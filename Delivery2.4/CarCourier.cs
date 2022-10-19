using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    internal class CarCourier : Courier
    {
        public CarCourier(int num)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Курьер на машине № {num + 1}";
            Start = CoordHelper.RandCoord();
            Speed = Company.DefaultCarCurierSpeed;
            Capacity = Company.DefaultCarCurierCapacity;
            Price = Company.DefaultCarCurierPricePerDistance;
        }
    }
}
