using Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    /// <summary>
    /// Курьер на машине.
    /// </summary>
    internal class CarCourier : Courier
    {
        private static int ExcelLine = 2;
        public CarCourier(int num)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Курьер на машине № {num + 1}";
            Console.WriteLine("Введите координаты местонахождения курьера через пробел(x y).");
            Start = CoordHelper.NewCoord(Console.ReadLine());
            Speed = Company.DefaultCarCurierSpeed;
            Capacity = Company.DefaultCarCurierCapacity;
            Price = Company.DefaultCarCurierPricePerDistance;
        }
        public CarCourier(int num, Coord start)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Курьер на машине № {num + 1}";
            Console.WriteLine("Введите координаты местонахождения курьера через пробел(x y).");
            Start = start;
            Speed = Company.DefaultCarCurierSpeed;
            Capacity = Company.DefaultCarCurierCapacity;
            Price = Company.DefaultCarCurierPricePerDistance;
        }
        public static CarCourier NewCourierFromExcel(int num)
        {
            var excel = new ExcelHelper();
            excel.Open("Couriers");
            if (excel.Get(ExcelLine, 2) == "")
            {
                Console.WriteLine("В файле закончились куриеры, задайте координаты курьера вручную.");
                return new CarCourier(num);
            }
            Coord start = new(Convert.ToInt32(excel.Get(ExcelLine, 8)), Convert.ToInt32(excel.Get(ExcelLine, 9)));
            excel.Close();
            ExcelLine++;
            return new CarCourier(num, start);
        }
    }
}
