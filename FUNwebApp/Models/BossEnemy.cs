using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerFUNwebApp1._0.Models
{
    public class BossEnemy : Enemy
    {
        public int SpecialAttackDamage { get; set; }
        public int SpecialAttackCooldown { get; set; }
    }
}