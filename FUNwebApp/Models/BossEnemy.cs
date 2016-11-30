using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FUNwebApp.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public class BossEnemy : Enemy
    {
        public int SpecialAttackDamage { get; set; }
        public DamageSource SpecialAttackDamageSource { get; set; }
        public int SpecialAttackCooldown { get; set; }

        public BossEnemy(int x, int y, int specialATKdmg, DamageSource dmgType, int specialATKcooldown, int attack, int attackPointsPerATK, int attackPointsRegen)
        {
            X = x;
            Y = y;
            SpecialAttackDamage = specialATKdmg;
            SpecialAttackCooldown = specialATKcooldown;
            SpecialAttackDamageSource = dmgType;
            Attack = attack;
            AttackPointsPerAttack = attackPointsPerATK;
            AttackPointsRegen = attackPointsRegen;
        }
    }
}