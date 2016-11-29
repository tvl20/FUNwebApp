using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KillerFUNwebApp1._0.Models;

namespace KillerAppFUN2.DAL
{
    class WeaponBizLog
    {
        private IWeaponRepo repo;

        public WeaponBizLog(IWeaponRepo _repo)
        {
            repo = _repo;
        }

        public List<Weapon> getAllWeapons()
        {
            return repo.getAllWeapons();
        }

        public Weapon getWeaponByName(string name)
        {
            return repo.getWeaponByName(name);
        }

        public Weapon getWeaponByID(int id)
        {
            return repo.getWeaponByID(id);
        }
    }
}
