﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FUNwebApp.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public class HumanEnemy : Enemy
    {
        public int CritChance { get; set; }
        public DamageSource DamageSource { get; set; }

        public HumanEnemy(int x, int y, DamageSource enemyDamageSource, int Attack, int ATKpointsPerAttack, int ATKpointsRegen, int CritChance, int Defence, int MovePointsPerMove, int Health, int Level)
        {
            X = x;
            Y = y;
            DamageSource = enemyDamageSource;
            this.Attack = Attack;
            AttackPointsPerAttack = ATKpointsPerAttack;
            AttackPointsRegen = ATKpointsRegen;
            this.CritChance = CritChance;
            this.Defence = Defence;
            this.MovePointsPerMove = MovePointsPerMove;
            this.Health = Health;
            this.Level = Level;
        }
    }
}