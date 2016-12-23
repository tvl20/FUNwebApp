using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KillerFUNwebApp1._0.Models;

namespace FUNwebApp.Models.DAL
{
    public interface IRoomRepo
    {
        string getRoomLayout(int roomID);
        Point GetPlayerSpawnPoint(int roomID);
        List<Enemy> GetEnemies(int roomID);
        List<Thing> GetObjects(int roomID);
        Room GetRoom(int roomID);
    }
}
