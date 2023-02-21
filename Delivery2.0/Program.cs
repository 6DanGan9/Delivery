using Excel;

namespace Delivery.UE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Размеры карты: 15:15.");
            Console.WriteLine("Курьеры работают в 3 смены (8ч-16ч / 12ч-20ч / 16ч-24ч)");
            Company.StartProgram();
        }
    }
}