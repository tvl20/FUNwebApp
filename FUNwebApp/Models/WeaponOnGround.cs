using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public class WeaponOnGround : Thing
    {
        public int WeaponID { get; set; }

        public WeaponOnGround(int weaponID, int x, int y)
        {
            WeaponID = weaponID;
            Type = ThingType.WeaponOnGround;
            X = x;
            Y = y;
        }
    }
}