﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerFUNwebApp1._0.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public int NextRoomID { get; set; }
        public int LocationID { get; set; }
        public int PlayerSpawnX { get; set; }
        public int PlayerSpawnY { get; set; }
        public List<HumanEnemy> HumanEnemies { get; set; }
        public List<MonsterEnemy> MonsterEnemies { get; set; }
        public List<BossEnemy> BossEnemies { get; set; }
        public List<Trap> Traps = new List<Trap>();
        public List<WeaponOnGround> WeaponOnGrounds = new List<WeaponOnGround>();
    }
}