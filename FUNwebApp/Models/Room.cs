using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerFUNwebApp1._0.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomLayout { get; set; }
        public int NextRoomID { get; set; }
        public int LocaionID { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<Thing> Things { get; set; }

        public void getEnemies()
        {
            throw new NotImplementedException();
        }
    }
}