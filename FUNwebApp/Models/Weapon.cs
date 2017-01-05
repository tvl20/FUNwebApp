using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public class Weapon
    {
        public int ID { get; set; }
        public int WeaponDamage { get; set; }
        public int WeaponCrit { get; set; }
        public WeaponType WeaponType { get; set; }
        public string WeaponName { get; set; }

        public Weapon(int id, int weaponDamage, int WeaponCrit, WeaponType weaponType, string weaponName)
        {
            ID = id;
            WeaponDamage = weaponDamage;
            this.WeaponCrit = WeaponCrit;
            this.WeaponType = weaponType;
            WeaponName = weaponName;
        }

        public Weapon() { }

        public override string ToString()
        {
            return WeaponName;
        }
    }
}