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
                using (SqlCommand cmd = new SqlCommand("SELECT X, Y, SoortSpecialATK, DMGspecialATK, Attack, AttackPointsPerAttack, AttackPointsRegen, SpecialAttackCooldown FROM BossEnemies WHERE RoomID = @roomID", connection))
                {
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
                            int ATKpointsRegen = reader.GetInt32(6);
                            int SpecialATKcooldown = reader.GetInt32(7);

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

                            BossEnemy boss = new BossEnemy(x, y, DMGspecialATK, enemyDamageSource, Attack, SpecialATKcooldown, ATKpointsPerAttack, ATKpointsRegen);
                            enemies.Add(boss);
                        }
                    }
                }

                //select all the HumanEnemies WIP
                using (SqlCommand cmd = new SqlCommand("SELECT X, Y, CritChance, Attack, AttackPointsPerAttack, AttackPointsRegen FROM BossEnemies WHERE RoomID = @roomID", connection))
                {
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
                            int ATKpointsRegen = reader.GetInt32(6);
                            int SpecialATKcooldown = reader.GetInt32(7);

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

                            BossEnemy boss = new BossEnemy(x, y, DMGspecialATK, enemyDamageSource, Attack, SpecialATKcooldown, ATKpointsPerAttack, ATKpointsRegen);
                        }
                    }
                }
            }
            
            return enemies;
        }

        public List<Thing> GetObjects(int roomID)
        {
            throw new NotImplementedException();
        }
    }
}