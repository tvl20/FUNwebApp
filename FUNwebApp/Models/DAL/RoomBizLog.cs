using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using KillerFUNwebApp1._0.Models;

namespace FUNwebApp.Models.DAL
{
    public class RoomBizLog
    {
        private IRoomRepo repo;
        public RoomBizLog(IRoomRepo _repo)
        {
            repo = _repo;
        }

        public string getRoomLayout(int roomID)
        {
            return repo.getRoomLayout(roomID);
        }

        public Point GetPlayerSpawnPoint(int roomID)
        {
            return repo.GetPlayerSpawnPoint(roomID);
        }

        public List<Enemy> GetEnemies(int roomID)
        {
            return repo.GetEnemies(roomID);
        }

        public List<Thing> GetObjects(int roomID)
        {
            return repo.GetObjects(roomID);
        }

        public Room GetRoom(int roomID)
        {
            return repo.GetRoom(roomID);
        }
    }
}