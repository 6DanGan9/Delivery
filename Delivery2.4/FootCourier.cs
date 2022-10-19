using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Пеший курьер.
    /// </summary>
    internal class FootCourier : Courier
    {
        public FootCourier(int num)
        {
            Name = $"Пеший курьер № {num + 1}";
            Start = CoordHelper.RandCoord();
            Speed = Company.DefaultFootCurierSpeed;
            Capacity = Company.DefaultFootCurierCapacity;
            Price = Company.DefaultFootCurierPricePerDistance;
        }
    }
}
