using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerFUNwebApp1._0.Models
{
    public class Location
    {
        public string Naam { get; set; }
        public int AmountOfRooms { get; set; }
        public List<Room> Rooms { get; set; }

        public string getRoomLayout(int _roomNumber)
        {
            throw new NotImplementedException();
        }
    }
}