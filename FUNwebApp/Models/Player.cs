using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using KillerAppFUN2.DAL;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public class Player : Entity
    {
        public Weapon CurrentWeapon { get; set; }
        public int CurrentRoomID { get; set; }
        public Point LocationPoint { get; set; }
        public string Name { get; set; }
        public Class Class { get; set; }
        public int MaxHealth { get; set; }
        public int XP { get; set; }

        private PlayerBizLog playerRepoBizLog = new PlayerBizLog(new MSSQLplayerRepo());
        private WeaponBizLog weaponRepoBizLog = new WeaponBizLog(new MSSQLweaponRepo());

        public void levelUp(Stat _Stat)
        {
            Level++;
            switch (_Stat)
            {
                case Stat.MaxHealth:
                    MaxHealth = MaxHealth + 5;
                    break;
                case Stat.Attack:
                    Attack++;
                    break;
                case Stat.Defence:
                    Defence++;
                    break;
            }
        }

        public Player(string name, Class playerClass, int levelStat, int healthStat, int maxHealthStat, int attackStat, int attackPointsPerAttackStat, int attackPointsRegenStat, int defenceStat, int movePointsPerMoveStat, Point locaionPoint, int currentRoomID, int xp, Weapon weapon)
        {
            XP = xp;
            Name = name;
            Class = playerClass;
            Level = levelStat;
            Health = healthStat;
            MaxHealth = maxHealthStat;
            Attack = attackStat;
            AttackPointsPerAttack = attackPointsPerAttackStat;
            AttackPointsRegen = attackPointsRegenStat;
            Defence = defenceStat;
            MovePointsPerMove = movePointsPerMoveStat;
            LocationPoint = locaionPoint;
            CurrentRoomID = currentRoomID;
            CurrentWeapon = weapon;
        }
        public Player(string name, Class playerClass, int levelStat, int healthStat, int maxHealthStat, int attackStat, int attackPointsPerAttackStat, int attackPointsRegenStat, int defenceStat, int movePointsPerMoveStat, Point locaionPoint, int currentRoomID, int xp, int weaponID)
        {
            XP = xp;
            Name = name;
            Class = playerClass;
            Level = levelStat;
            Health = healthStat;
            MaxHealth = maxHealthStat;
            Attack = attackStat;
            AttackPointsPerAttack = attackPointsPerAttackStat;
            AttackPointsRegen = attackPointsRegenStat;
            Defence = defenceStat;
            MovePointsPerMove = movePointsPerMoveStat;
            LocationPoint = locaionPoint;
            CurrentRoomID = currentRoomID;
            CurrentWeapon = weaponRepoBizLog.getWeaponByID(weaponID);
        }
        public Player(string name, Class playerClass, int levelStat, int healthStat, int maxHealthStat, int attackStat, int attackPointsPerAttackStat, int attackPointsRegenStat, int defenceStat, int movePointsPerMoveStat, Point locaionPoint, int currentRoomID, int xp, string weaponName)
        {
            XP = xp;
            Name = name;
            Class = playerClass;
            Level = levelStat;
            Health = healthStat;
            MaxHealth = maxHealthStat;
            Attack = attackStat;
            AttackPointsPerAttack = attackPointsPerAttackStat;
            AttackPointsRegen = attackPointsRegenStat;
            Defence = defenceStat;
            MovePointsPerMove = movePointsPerMoveStat;
            LocationPoint = locaionPoint;
            CurrentRoomID = currentRoomID;
            CurrentWeapon = weaponRepoBizLog.getWeaponByName(weaponName);
        }

        public Player()
        {
            //this is so that it is possible to make a player object with 'custom' properties
        }
    }
}