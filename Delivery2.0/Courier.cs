using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Delivery2._0
{
    internal class Courier
    {
        static Random rnd = new();
        public Courier(string tip, int speed, int capacity, int price, Coord coord, int startTime, int endTime)
        {
            this.tip = tip;
            this.speed = speed;
            this.capacity = capacity;
            this.price = price;
            coords.Add(coord);
            this.startTime = startTime;
            this.endTime = endTime;
        }
        public string tip { get; }
        public int numberMin { get; set; }
        public List<Order> orders = new();
        public List<Coord> coords = new();
        public int speed { get; }
        public int capacity { get; }
        public int price { get; }
        public int startTime { get; }
        public int endTime { get; }
        public int profit { get; set; }
        public static Coord RandCoord()
        {
            Coord coord = new();
            coord.x = rnd.Next(1, 16);
            coord.y = rnd.Next(1, 16);
            return coord;
        }
        public static void NewFC(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                string tip = $"Пеший курьер № {i + 1}";
                Coord coord = RandCoord();
                CourierLogic.couriersList.Add(new Courier(tip, 7, 4, 40, coord, CourierScheduleStart(), CourierScheduleEnd()));
            }
        }
        public static void NewBC(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                string tip = $"Курьер на велосипеде № {i + 1}";
                Coord coord = RandCoord();
                CourierLogic.couriersList.Add(new Courier(tip, 15, 6, 60, coord, CourierScheduleStart(), CourierScheduleEnd()));
            }
        }
        public static void NewSC(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                string tip = $"Курьер на скутере № {i + 1}";
                Coord coord = RandCoord();
                CourierLogic.couriersList.Add(new Courier(tip, 30, 8, 100, coord, CourierScheduleStart(), CourierScheduleEnd()));
            }
        }
        public static void NewCC(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                string tip = $"Курьер на машине № {i + 1}";
                Coord coord = RandCoord();
                CourierLogic.couriersList.Add(new Courier(tip, 25, 60, 100, coord, CourierScheduleStart(), CourierScheduleEnd()));
            }
        }
        private static int CourierScheduleStart()
        {
            int start = 0;
            switch (CourierLogic.couriersList.Count % 3)
            {
                case 0:
                    break;
                case 1:
                    start = 240;
                    break;
                case 2:
                    start = 480;
                    break;
            }
            return start;
        }
        private static int CourierScheduleEnd()
        {
            int end = 0;
            switch (CourierLogic.couriersList.Count % 3)
            {
                case 0:
                    end = 480;
                    break;
                case 1:
                    end = 720;
                    break;
                case 2:
                    end = 960;
                    break;
            }
            return end;
        }
        public static int BusyTime(Courier courier)
        {
            int time = 0;
            if (courier.orders.Count > 0)
            {
                for (int i = 0; i < courier.orders.Count; i++)
                {
                    time += courier.orders[i].time;
                }
            }
            return time;
        }
    }
}
