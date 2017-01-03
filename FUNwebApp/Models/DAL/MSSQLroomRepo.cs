using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using FUNwebApp.Models.Enums;
using KillerFUNwebApp1._0.Models;
using KillerFUNwebApp1._0.Models.Enums;

namespace FUNwebApp.Models.DAL
{
    public class MSSQLroomRepo : IRoomRepo
    {
        private readonly string conn = @"Data Source=DESKTOP-9K8HK1F;Initial Catalog=FUNwebKillerApp;Integrated Security=True";

        public string getRoomLayout(int roomID)
        {
            string layout = "";
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT RoomLayOut FROM Rooms WHERE RoomID = @roomID", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@roomID", SqlDbType.Int).Value = roomID;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            layout = reader.GetString(0);
                        }
                    }
                }
            }
            return layout;
        }

        public Point GetPlayerSpawnPoint(int roomID)
        {
            Point spawnPoint = new Point();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT PlayerSpawnX, PlayerSpawnY FROM Rooms WHERE RoomID = @roomID", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@roomID", SqlDbType.Int).Value = roomID;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int x = reader.GetInt32(0);
                            int y = reader.GetInt32(1);
                            spawnPoint = new Point(x, y);
                        }
                    }
                }
            }
            return spawnPoint;
        }

        public List<Enemy> GetEnemies(int roomID) //WIP
        {
            List<Enemy> enemies = new List<Enemy>();
            
            
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                //get the boss enemies
                using (SqlCommand cmd = new SqlCommand("SELECT X, Y, SoortSpecialATK, DMGspecialATK, Attack, AttackPointsPerAttack, SpecialATKCooldown, MovePointsPerMove, Defence, Health, Levels FROM BossEnemies WHERE RoomID = @roomID", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@roomID", SqlDbType.Int).Value = roomID;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int x = reader.GetInt32(0);
                            int y = reader.GetInt32(1);
                            DamageSource enemyDamageSource = DamageSource.Physical; //default damage type
                            int DMGspecialATK = reader.GetInt32(3);
                            int Attack = reader.GetInt32(4);
                            int ATKpointsPerAttack = reader.GetInt32(5);
                            double ATKpointsRegen = Math.Round((Convert.ToDouble(ATKpointsPerAttack) / 5) + 1);
                            int SpecialATKcooldown = reader.GetInt32(6);
                            int MovePointsPerMove = reader.GetInt32(7);
                            int Defence = reader.GetInt32(8);
                            int Health = reader.GetInt32(9);
                            int Levels = reader.GetInt32(10);

                            string damageSource = reader.GetString(2);
                            switch (damageSource)
                            {
                                case "Cold":
                                    enemyDamageSource = DamageSource.Cold;
                                    break;
                                case "Fire":
                                    enemyDamageSource = DamageSource.Fire;
                                    break;
                                case "Physical":
                                    enemyDamageSource = DamageSource.Physical;
                                    break;
                            }

                            BossEnemy boss = new BossEnemy(x, y, DMGspecialATK, enemyDamageSource, Attack, SpecialATKcooldown, ATKpointsPerAttack, Convert.ToInt32(ATKpointsRegen), Defence, MovePointsPerMove, Health, Levels);
                            enemies.Add(boss);
                        }
                    }
                }

                //select all the HumanEnemies WIP
                using (SqlCommand cmd = new SqlCommand("SELECT X, Y, CritChance, Attack, AttackPointsPerAttack, AttackPointsRegen, MovePointsPerMove, Defence, Health, Levels FROM HumanEnemies WHERE RoomID = @roomID", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@roomID", SqlDbType.Int).Value = roomID;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int x = reader.GetInt32(0);
                            int y = reader.GetInt32(1);
                            int critChance = reader.GetInt32(2);
                            DamageSource enemyDamageSource = DamageSource.Physical; //default damage type
                            int Attack = reader.GetInt32(4);
                            int ATKpointsPerAttack = reader.GetInt32(5);
                            int ATKpointsRegen = reader.GetInt32(6);
                            int MovePointsPerMove = reader.GetInt32(7);
                            int Defence = reader.GetInt32(8);
                            int Health = reader.GetInt32(9);
                            int Levels = reader.GetInt32(10);


                            /*
                             * For later expansion.
                            string damageSource = reader.GetString(7);
                            switch (damageSource)
                            {
                                case "Cold":
                                    enemyDamageSource = DamageSource.Cold;
                                    break;
                                case "Fire":
                                    enemyDamageSource = DamageSource.Fire;
                                    break;
                                case "Physical":
                                    enemyDamageSource = DamageSource.Physical;
                                    break;
                            }
                            */

                            HumanEnemy Human = new HumanEnemy(x, y, enemyDamageSource, Attack, ATKpointsPerAttack, ATKpointsRegen, critChance, Defence, MovePointsPerMove, Health, Levels);
                            enemies.Add(Human);
                        }
                    }
                }

                //get all the monsterEnemies
                using (SqlCommand cmd = new SqlCommand("SELECT X, Y, TrueDMG, Attack, AttackPointsPerAttack, AttackPointsRegen, MovePointsPerMove, Defence, Health, Levels FROM MonsterEnemies WHERE RoomID = @roomID", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@roomID", SqlDbType.Int).Value = roomID;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int x = reader.GetInt32(0);
                            int y = reader.GetInt32(1);
                            int TrueDMG = reader.GetInt32(2);
                            int Attack = reader.GetInt32(3);
                            int ATKpointsPerAttack = reader.GetInt32(4);
                            int ATKpointsRegen = reader.GetInt32(5);
                            int MovePointsPerMove = reader.GetInt32(6);
                            int Defence = reader.GetInt32(7);
                            int Health = reader.GetInt32(8);
                            int Levels = reader.GetInt32(9);



                            MonsterEnemy Monster = new MonsterEnemy(x, y, Attack, ATKpointsPerAttack, ATKpointsRegen, TrueDMG, Defence, MovePointsPerMove, Health, Levels);
                            enemies.Add(Monster);
                        }
                    }
                }
            }
            
            return enemies;
        }

        public List<Thing> GetObjects(int roomID)
        {
            List<Thing> returnList = new List<Thing>();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Soort, WeaponID, X, Y, DMG FROM Dingen WHERE RoomID = @roomID", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@roomID", SqlDbType.Int).Value = roomID;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int weaponID = reader.GetInt32(1);
                            int x = reader.GetInt32(2);
                            int y = reader.GetInt32(3);
                            ThingType thingType = ThingType.Trap; //default
                            int dmg = reader.GetInt32(4);

                            string type = reader.GetString(0);
                            switch (type)
                            {
                                case "Trap":
                                    thingType = ThingType.Trap;
                                    returnList.Add(new Trap(dmg));
                                    break;
                                case "Fire":
                                    thingType = ThingType.WeaponOnGround;
                                    returnList.Add(new WeaponOnGround(weaponID));
                                    break;
                            }
                        }
                    }
                }
            }
            return returnList;
        }

        public Room GetRoom(int roomID)
        {
            Room returnRoom = new Room();
            returnRoom.Enemies = this.GetEnemies(roomID);
            returnRoom.RoomLayout = this.getRoomLayout(roomID);
            returnRoom.Things = this.GetObjects(roomID);
            returnRoom.RoomID = roomID;

            returnRoom.LocationID = 0; //default location is 0
            returnRoom.NextRoomID = roomID; //default next room is the current room
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT LocationID, NextRoomID FROM Rooms WHERE RoomID = @roomID", connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@roomID", SqlDbType.Int).Value = roomID;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnRoom.LocationID = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                            {
                                returnRoom.NextRoomID = reader.GetInt32(1);
                            }
                        }
                    }
                }
            }
            
            return returnRoom;
        }
    }
}