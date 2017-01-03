using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerFUNwebApp1._0.Models
{
    public class MonsterEnemy : Enemy
    {
        public int TrueDamage { get; set; }

        public MonsterEnemy(int x, int y, int Attack, int ATKpointsPerAttack, int ATKpointsRegen, int trueDMG, int Defence, int MovePointsPerMove, int Health, int Level)
        {
            X = x;
            Y = y;
            this.Attack = Attack;
            AttackPointsPerAttack = ATKpointsPerAttack;
            AttackPointsRegen = ATKpointsRegen;
            TrueDamage = trueDMG;
            this.Defence = Defence;
            this.MovePointsPerMove = MovePointsPerMove;
            this.Health = Health;
            this.Level = Level;
        }
    }
}