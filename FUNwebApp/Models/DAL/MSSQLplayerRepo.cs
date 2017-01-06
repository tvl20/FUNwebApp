using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KillerFUNwebApp1._0.Models;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerAppFUN2.DAL
{
    class MSSQLplayerRepo : IPlayerRepo
    {
        private readonly string conn = @"Data Source=DESKTOP-9K8HK1F;Initial Catalog=FUNwebKillerApp;Integrated Security=True";

        public void addPlayer(Player p)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("newPlayer", connection))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@playerName", SqlDbType.VarChar).Value = p.Name;
                    cmd.Parameters.Add("@Class", SqlDbType.VarChar).Value = p.Class;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void deletePlayer(string name)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Players WHERE PlayerName = @name", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Player> getAllPlayers()
        {
            List<Player> playerList = new List<Player>();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT p.PlayerName, p.Class, p.CurrentRoomID, p.Lvl, p.MaxHP, p.HP, p.Attack, p.AttackPPA, p.AttackPReg, " +
                                                       "p.MovePPM, p.Defence, w.WeaponID, w.WeaponDMG, w.WeaponCRT, w.WeaponType, w.WeaponName, p.XP FROM Players p " +
                                                       "INNER JOIN Weapons w ON p.CurrentWeaponID = w.WeaponID"))
                {
                    cmd.Connection = connection;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string playerName = reader.GetString(0);
                            Class playerClass = KillerFUNwebApp1._0.Models.Enums.Class.Knight;  //default playerClass is 'Knight'
                            int currentRoom = reader.GetInt32(2);
                            int lvl = reader.GetInt32(3);
                            int maxHP = reader.GetInt32(4);
                            int hp = reader.GetInt32(5);
                            int attack = reader.GetInt32(6);
                            int attackPointsPerAttack = reader.GetInt32(7);
                            int attackPointsRegen = reader.GetInt32(8);
                            int movePointsPerMove = reader.GetInt32(9);
                            int defence = reader.GetInt32(10);
                            int weaponID = reader.GetInt32(11);
                            int weaponDMG = reader.GetInt32(12);
                            int weaponCRT = reader.GetInt32(13);
                            WeaponType weaponType = WeaponType.MonsterSlayer;   //default weapontype is 'MonsterSlayer'
                            string weaponName = reader.GetString(15);
                            int xp = reader.GetInt32(16);


                            string Class = reader.GetString(1);
                            switch (Class)
                            {
                                case "Paladin":
                                    playerClass = KillerFUNwebApp1._0.Models.Enums.Class.Paladin;
                                    break;
                                case "Crusader":
                                    playerClass = KillerFUNwebApp1._0.Models.Enums.Class.Crusader;
                                    break;
                                case "Knight":
                                    playerClass = KillerFUNwebApp1._0.Models.Enums.Class.Knight;
                                    break;
                                case "Barbarian":
                                    playerClass = KillerFUNwebApp1._0.Models.Enums.Class.Barbarian;
                                    break;
                            }

                            string type = reader.GetString(14);
                            switch (type)
                            {
                                case "HumanSlayer":
                                    weaponType = WeaponType.HumanSlayer;
                                    break;
                                case "MonsterSlayer":
                                    weaponType = WeaponType.MonsterSlayer;
                                    break;
                            }


                            Weapon w = new Weapon(weaponID, weaponDMG, weaponCRT, weaponType, weaponName);
                            Player p = new Player(playerName, playerClass, lvl, hp, maxHP, attack, attackPointsPerAttack, attackPointsRegen, defence, movePointsPerMove, currentRoom, xp, w);
                            playerList.Add(p);
                        }
                    }
                }
            }
            return playerList;
        }

        public Player getPlayer(string playerName)
        {
            Player p = null;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Class, CurrentRoomID, Lvl, MaxHP, HP, Attack, AttackPPA, AttackPReg, " +
                                                       "MovePPM, Defence, WeaponID, WeaponDMG, WeaponCRT, WeaponType, WeaponName, XP FROM Players " +
                                                       "INNER JOIN Weapons ON Players.CurrentWeaponID = Weapons.WeaponID WHERE PlayerName = @playerName", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@playerName", SqlDbType.VarChar).Value = playerName;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Class playerClass = KillerFUNwebApp1._0.Models.Enums.Class.Knight;  //default playerClass is 'Knight'
                            int currentRoom = reader.GetInt32(1);
                            int lvl = reader.GetInt32(2);
                            int maxHP = reader.GetInt32(3);
                            int hp = reader.GetInt32(4);
                            int attack = reader.GetInt32(5);
                            int attackPointsPerAttack = reader.GetInt32(6);
                            int attackPointsRegen = reader.GetInt32(7);
                            int movePointsPerMove = reader.GetInt32(8);
                            int defence = reader.GetInt32(9);
                            int weaponID = reader.GetInt32(10);
                            int weaponDMG = reader.GetInt32(11);
                            int weaponCRT = reader.GetInt32(12);
                            WeaponType weaponType = WeaponType.MonsterSlayer;   //default weapontype is 'MonsterSlayer'
                            string weaponName = reader.GetString(14);
                            int xp = reader.GetInt32(15);


                            string Class = reader.GetString(0);
                            switch (Class)
                            {
                                case "Paladin":
                                    playerClass = KillerFUNwebApp1._0.Models.Enums.Class.Paladin;
                                    break;
                                case "Crusader":
                                    playerClass = KillerFUNwebApp1._0.Models.Enums.Class.Crusader;
                                    break;
                                case "Knight":
                                    playerClass = KillerFUNwebApp1._0.Models.Enums.Class.Knight;
                                    break;
                                case "Barbarian":
                                    playerClass = KillerFUNwebApp1._0.Models.Enums.Class.Barbarian;
                                    break;
                            }

                            string type = reader.GetString(13);
                            switch (type)
                            {
                                case "HumanSlayer":
                                    weaponType = WeaponType.HumanSlayer;
                                    break;
                                case "MonsterSlayer":
                                    weaponType = WeaponType.MonsterSlayer;
                                    break;
                            }


                            Weapon w = new Weapon(weaponID, weaponDMG, weaponCRT, weaponType, weaponName);
                            p = new Player(playerName, playerClass, lvl, hp, maxHP, attack, attackPointsPerAttack, attackPointsRegen, defence, movePointsPerMove, currentRoom, xp, w);
                        }
                    }
                }
            }
            return p;
        }

        public void updatePlayer(Player p)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Players SET CurrentRoomID=@roomID, CurrentWeaponID=@weaponID, Lvl=@playerLvl, XP=@xp, MaxHP=@playerMaxHP, HP=@playerHP, Defence=@playerDefence WHERE PlayerName=@playerName", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@roomID", SqlDbType.Int).Value = p.CurrentRoomID;
                    cmd.Parameters.Add("@playerName", SqlDbType.VarChar).Value = p.Name;
                    cmd.Parameters.Add("@weaponID", SqlDbType.Int).Value = p.CurrentWeapon.ID;
                    cmd.Parameters.Add("@playerLvl", SqlDbType.Int).Value = p.Level;
                    cmd.Parameters.Add("@xp", SqlDbType.Int).Value = p.XP;
                    cmd.Parameters.Add("@playerMaxHP", SqlDbType.Int).Value = p.MaxHealth;
                    cmd.Parameters.Add("@playerHP", SqlDbType.Int).Value = p.Health;
                    cmd.Parameters.Add("@playerDefence", SqlDbType.Int).Value = p.Defence;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
