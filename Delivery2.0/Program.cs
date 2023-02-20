using Excel;

namespace Delivery.UE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Карта 15:15, начало рабочего дня курьеров с 8:00, окончание в 24:00(3 смены по 8 часов).");
            Company.StartProgram();
        }
    }
}