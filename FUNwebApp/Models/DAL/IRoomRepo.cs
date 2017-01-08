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
        Location GetLocation(int locationID);
        Point GetPlayerSpawnPoint(int roomID);
        List<MonsterEnemy> GetMonsterEnemies(int roomID);
        List<HumanEnemy> GetHumanEnemies(int roomID);
        List<BossEnemy> GetBossEnemies(int roomID);
        List<Trap> GetTraps(int roomID);
        List<WeaponOnGround> GetWeaponOnGrounds(int roomID);
        int getPreviousRoomID(int roomID);
        Room GetRoom(int roomID);
    }
}
