using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FUNwebApp.Models.Enums;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public abstract class Entity
    {
        public int Level { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int AttackPoints { get; set; }
        public int AttackPointsPerAttack { get; set; }
        public int AttackPointsRegen { get; set; }
        public int Defence { get; set; }
        public int MovePointsPerMove { get; set; }
        public int MovePoints { get; set; }
    }
}