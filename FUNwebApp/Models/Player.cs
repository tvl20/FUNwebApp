using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public class Player : Entity
    {
        public Weapon CurrentWeapon { get; set; }
        public int CurrentRoomID { get; set; }
        public string Naam { get; set; }
        public Class Class { get; set; }

        public Stat LevelStat { get; set; }
        public Stat HealthStat { get; set; }
        public Stat MaxHealthStat { get; set; }
        public Stat AttackStat { get; set; }
        public Stat AttackPointsPerMoveStat { get; set; }
        public Stat AttackPointsRegenStat { get; set; }
        public Stat DefenceStat { get; set; }
        public Stat MovePointsPerMoveStat { get; set; }

        public void levelUp(Stat _Stat)
        {
            throw new NotImplementedException();
        }
    }
}