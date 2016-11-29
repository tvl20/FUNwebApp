using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace FUNwebApp.Models.DAL
{
    public class MSSQLroomRepo : IRoomRepo
    {
        public string getRoomLayout(int roomID)
        {
            throw new NotImplementedException();
        }

        public Point GetPlayerSpawnPoint(int roomID)
        {
            throw new NotImplementedException();
        }

        public List<Point> GetEnemySpawnPoints(int roomID)
        {
            throw new NotImplementedException();
        }

        public List<Point> GetObjectSpawnPoints(int roomID)
        {
            throw new NotImplementedException();
        }
    }
}