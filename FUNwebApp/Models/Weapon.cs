using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public class Weapon
    {
        public int WeaponDamage { get; set; }
        public int WeaponCrit { get; set; }
        public WeaponType WeaponType { get; set; }
        public string WeaponName { get; set; }
    }
}