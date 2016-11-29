using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace FUNwebApp.Models.DAL
{
    public class MSSQLroomRepo : IRoomRepo
    {
        private readonly string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Teun\Source\Repos\FUN2killerapp\KillerAppFUN2\KillerAppFUN2\RPGdata.mdf;Integrated Security=True";

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

        public List<Point> GetEnemySpawnPoints(int roomID) //WIP
        {
            List<Point> enemySpawnPoints = new List<Point>();
            /*
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT SpawnX, SpawnY FROM EnemyRooms WHERE RoomID = @roomID", connection))
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
            */
            return enemySpawnPoints;
        }

        public List<Point> GetObjectSpawnPoints(int roomID)
        {
            throw new NotImplementedException();
        }
    }
}