using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._4
{
    /// <summary>
    /// Координатное местоположение.
    /// </summary>
    public class Coord
    {
        /// <summary>
        /// X
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y
        /// </summary>
        public int Y { get; set; }

        public Coord()
        {
        }
        /// <summary>
        /// Создает новый объект локаци
        /// </summary>
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Представление координаты ввиде строки.
        /// </summary>
        public string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
