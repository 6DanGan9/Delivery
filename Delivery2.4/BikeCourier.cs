using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Курьер на велосипеде.
    /// </summary>
    internal class BikeCourier : Courier
    {
        public BikeCourier(int num)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Курьер на велосипеде № {num + 1}";
            Start = CoordHelper.RandCoord();
            Speed = Company.DefaultBikeCurierSpeed;
            Capacity = Company.DefaultBikeCurierCapacity;
            Price = Company.DefaultBikeCurierPricePerDistance;
        }
    }
}
