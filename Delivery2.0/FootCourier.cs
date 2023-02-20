using Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.UE
{
    /// <summary>
    /// Пеший курьер.
    /// </summary>
    internal class FootCourier : Courier
    {
        private static int ExcelLine = 2;
        public FootCourier(int num)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Пеший курьер № {num + 1}";
            Console.WriteLine("Введите координаты местонахождения курьера через пробел(x y).");
            Start = CoordHelper.NewCoord(Console.ReadLine());
            Speed = Company.DefaultFootCurierSpeed;
            Capacity = Company.DefaultFootCurierCapacity;
            Price = Company.DefaultFootCurierPricePerDistance;
        }

        public FootCourier(int num, Coord start)
        {
            CourierID = Company.CouriersList.Count;
            Name = $"Пеший курьер № {num + 1}";
            Start = start;
            Speed = Company.DefaultFootCurierSpeed;
            Capacity = Company.DefaultFootCurierCapacity;
            Price = Company.DefaultFootCurierPricePerDistance;
        }

        public static FootCourier NewCourierFromExcel(int num)
        {
            var excel = new ExcelHelper();
            excel.Open("Couriers");
            if (excel.Get(ExcelLine, 2) == "")
            {
                Console.WriteLine("В файле закончились куриеры, задайте координаты курьера вручную.");
                return new FootCourier(num);
            }
            Coord start = new(Convert.ToInt32(excel.Get(ExcelLine, 2)), Convert.ToInt32(excel.Get(ExcelLine, 3)));
            excel.Close();
            ExcelLine++;
            return new FootCourier(num, start); 
        }
    }
}
