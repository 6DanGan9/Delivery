using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    /// <summary>
    /// Курьер на скутере.
    /// </summary>
    internal class ScuterCourier : Courier
    {
        public ScuterCourier(int num)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Курьер на скутере № {num + 1}";
            Start = CoordHelper.RandCoord();
            Speed = Company.DefaultScuterCurierSpeed;
            Capacity = Company.DefaultScuterCurierCapacity;
            Price = Company.DefaultScyterCurierPricePerDistance;
        }
    }
}
