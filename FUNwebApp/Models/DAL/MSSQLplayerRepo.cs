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
                using (SqlCommand cmd = new SqlCommand("SELECT p.PlayerName, p.Class, p.CurrentRoomID, p.X, p.Y, p.Lvl, p.MaxHP, p.HP, p.Attack, p.AttackPPA, p.AttackPReg, " +
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
                            int x = reader.GetInt32(3);
                            int y = reader.GetInt32(4);
                            int lvl = reader.GetInt32(5);
                            int maxHP = reader.GetInt32(6);
                            int hp = reader.GetInt32(7);
                            int attack = reader.GetInt32(8);
                            int attackPointsPerAttack = reader.GetInt32(9);
                            int attackPointsRegen = reader.GetInt32(10);
                            int movePointsPerMove = reader.GetInt32(11);
                            int defence = reader.GetInt32(12);
                            int weaponID = reader.GetInt32(13);
                            int weaponDMG = reader.GetInt32(14);
                            int weaponCRT = reader.GetInt32(15);
                            WeaponType weaponType = WeaponType.MonsterSlayer;   //default weapontype is 'MonsterSlayer'
                            string weaponName = reader.GetString(17);
                            int xp = reader.GetInt32(18);


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

                            string type = reader.GetString(16);
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
                            Player p = new Player(playerName, playerClass, lvl, hp, maxHP, attack, attackPointsPerAttack, attackPointsRegen, defence, movePointsPerMove, x, y, currentRoom, xp, w);
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
                using (SqlCommand cmd = new SqlCommand("SELECT Class, CurrentRoomID, X, Y, Lvl, MaxHP, HP, Attack, AttackPPA, AttackPReg, " +
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
                            int x = reader.GetInt32(2);
                            int y = reader.GetInt32(3);
                            int lvl = reader.GetInt32(4);
                            int maxHP = reader.GetInt32(5);
                            int hp = reader.GetInt32(6);
                            int attack = reader.GetInt32(7);
                            int attackPointsPerAttack = reader.GetInt32(8);
                            int attackPointsRegen = reader.GetInt32(9);
                            int movePointsPerMove = reader.GetInt32(10);
                            int defence = reader.GetInt32(11);
                            int weaponID = reader.GetInt32(12);
                            int weaponDMG = reader.GetInt32(13);
                            int weaponCRT = reader.GetInt32(14);
                            WeaponType weaponType = WeaponType.MonsterSlayer;   //default weapontype is 'MonsterSlayer'
                            string weaponName = reader.GetString(16);
                            int xp = reader.GetInt32(17);


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

                            string type = reader.GetString(16);
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
                            p = new Player(playerName, playerClass, lvl, hp, maxHP, attack, attackPointsPerAttack, attackPointsRegen, defence, movePointsPerMove, x, y, currentRoom, xp, w);
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
                using (SqlCommand cmd = new SqlCommand("UPDATE Players SET CurrentWeapon=@weaponID, Lvl=@playerLvl, XP=@xp, X=@playerX, Y=@playerY, MaxHP=@playerMaxHP, HP=@playerHP, Defence=@playerDefence WHERE PlayerName=@playerName", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@playerName", SqlDbType.Int).Value = p.Name;
                    cmd.Parameters.Add("@weaponID", SqlDbType.Int).Value = p.CurrentWeapon.ID;
                    cmd.Parameters.Add("@playerLvl", SqlDbType.Int).Value = p.Level;
                    cmd.Parameters.Add("@xp", SqlDbType.Int).Value = p.XP;
                    cmd.Parameters.Add("@playerX", SqlDbType.Int).Value = p.X;
                    cmd.Parameters.Add("@playerY", SqlDbType.Int).Value = p.Y;
                    cmd.Parameters.Add("@playerMaxHP", SqlDbType.Int).Value = p.MaxHealth;
                    cmd.Parameters.Add("@playerHP", SqlDbType.Int).Value = p.Health;
                    cmd.Parameters.Add("@playerDefence", SqlDbType.Int).Value = p.Defence;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
