using Excel;
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
        private static int ExcelLine = 2;
        public ScuterCourier(int num)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Курьер на скутере № {num + 1}";
            Console.WriteLine("Введите координаты местонахождения курьера через пробел(x y).");
            Start = CoordHelper.NewCoord(Console.ReadLine());
            Speed = Company.DefaultScuterCurierSpeed;
            Capacity = Company.DefaultScuterCurierCapacity;
            Price = Company.DefaultScyterCurierPricePerDistance;
        }
        public ScuterCourier(int num, Coord start)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Курьер на скутере № {num + 1}";
            Console.WriteLine("Введите координаты местонахождения курьера через пробел(x y).");
            Start = start;
            Speed = Company.DefaultScuterCurierSpeed;
            Capacity = Company.DefaultScuterCurierCapacity;
            Price = Company.DefaultScyterCurierPricePerDistance;
        }
        public static ScuterCourier NewCourierFromExcel(int num)
        {
            var excel = new ExcelHelper();
            excel.Open("Couriers");
            if (excel.Get(ExcelLine, 2) == "")
            {
                Console.WriteLine("В файле закончились куриеры, задайте координаты курьера вручную.");
                return new ScuterCourier(num);
            }
            Coord start = new(Convert.ToInt32(excel.Get(ExcelLine, 6)), Convert.ToInt32(excel.Get(ExcelLine, 7)));
            excel.Close();
            ExcelLine++;
            return new ScuterCourier(num, start);
        }
    }
}
