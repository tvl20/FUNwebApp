using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KillerFUNwebApp1._0.Models;

namespace KillerAppFUN2.DAL
{
    interface IWeaponRepo
    {
        List<Weapon> getAllWeapons();
        Weapon getWeaponByName(string name);
        Weapon getWeaponByID(int id);
    }
}
