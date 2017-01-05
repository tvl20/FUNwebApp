using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public class Trap : Thing
    {
        public int Damage { get; set; }

        public Trap(int damage, int x, int y)
        {
            Damage = damage;
            Type = ThingType.Trap;
            X = x;
            Y = y;
        }
    }
}