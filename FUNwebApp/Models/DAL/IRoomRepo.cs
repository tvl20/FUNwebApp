using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNwebApp.Models.DAL
{
    public interface IRoomRepo
    {
        string getRoomLayout(int roomID);
        Point GetPlayerSpawnPoint(int roomID);
        List<Point> GetEnemySpawnPoints(int roomID);
        List<Point> GetObjectSpawnPoints(int roomID);
    }
}
