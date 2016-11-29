using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

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

        public List<Point> GetEnemySpawnPoints(int roomID)
        {
            return repo.GetEnemySpawnPoints(roomID);
        }

        public List<Point> GetObjectSpawnPoints(int roomID)
        {
            return repo.GetObjectSpawnPoints(roomID);
        }
    }
}