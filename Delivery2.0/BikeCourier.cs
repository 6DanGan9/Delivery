using Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    /// <summary>
    /// Курьер на велосипеде.
    /// </summary>
    internal class BikeCourier : Courier
    {
        private static int ExcelLine = 2;
        public BikeCourier(int num)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Курьер на велосипеде № {num + 1}";
            Console.WriteLine("Введите координаты местонахождения курьера через пробел(x y).");
            Start = CoordHelper.NewCoord(Console.ReadLine());
            Speed = Company.DefaultBikeCurierSpeed;
            Capacity = Company.DefaultBikeCurierCapacity;
            Price = Company.DefaultBikeCurierPricePerDistance;
        }
        public BikeCourier(int num, Coord start)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Курьер на велосипеде № {num + 1}";
            Start = start;
            Speed = Company.DefaultBikeCurierSpeed;
            Capacity = Company.DefaultBikeCurierCapacity;
            Price = Company.DefaultBikeCurierPricePerDistance;
        }
        public static BikeCourier NewCourierFromExcel(int num)
        {
            var excel = new ExcelHelper();
            excel.Open("Couriers");
            if (excel.Get(ExcelLine, 2) == "")
            {
                excel.Close();
                Console.WriteLine("В файле закончились куриеры, задайте координаты курьера вручную.");
                return new BikeCourier(num);
            }
            Coord start = new(Convert.ToInt32(excel.Get(ExcelLine, 4)), Convert.ToInt32(excel.Get(ExcelLine, 5)));
            excel.Close();
            ExcelLine++;
            return new BikeCourier(num, start);
        }
    }
}
