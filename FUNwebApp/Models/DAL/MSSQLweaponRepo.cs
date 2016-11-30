using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KillerFUNwebApp1._0.Models;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerAppFUN2.DAL
{
    class MSSQLweaponRepo : IWeaponRepo
    {
        private readonly string conn = @"Data Source=DESKTOP-9K8HK1F;Initial Catalog=FUNwebKillerApp;Integrated Security=True";

        public List<Weapon> getAllWeapons()
        {
            List<Weapon> weaponList = new List<Weapon>();
            using(SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Weapons", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ID = reader.GetInt32(0);
                            int DMG = reader.GetInt32(1);
                            int CRT = reader.GetInt32(2);
                            WeaponType TYPE = WeaponType.MonsterSlayer;
                            string NAME = reader.GetString(4);

                            string type = reader.GetString(3);
                            switch (type)
                            {
                                case "HumanSlayer":
                                    TYPE = WeaponType.HumanSlayer;
                                    break;
                                case "MonsterSlayer":
                                    TYPE = WeaponType.MonsterSlayer;
                                    break;
                            }
                            weaponList.Add(new Weapon(ID, DMG, CRT, TYPE, NAME));
                        }
                    }
                }
            }
            return weaponList;
        }

        public Weapon getWeaponByName(string name)
        {
            Weapon weapon = null;
            using(SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT WeaponID, WeaponDMG, WeaponCRT, WeaponType FROM Weapons WHERE WeaponName=@weaponName", connection))
                {
                    cmd.Parameters.Add("@weaponName", SqlDbType.VarChar).Value = name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ID = reader.GetInt32(0);
                            int DMG = reader.GetInt32(1);
                            int CRT = reader.GetInt32(2);
                            WeaponType TYPE = WeaponType.MonsterSlayer; //default type is 'MonsterSlayer'

                            string type = reader.GetString(3);
                            switch (type)
                            {
                                case "HumanSlayer":
                                    TYPE = WeaponType.HumanSlayer;
                                    break;
                                case "MonsterSlayer":
                                    TYPE = WeaponType.MonsterSlayer;
                                    break;
                            }
                            weapon = new Weapon(ID, DMG, CRT, TYPE, name);
                        }
                    }
                }


                //string query = ";";
                //SqlCommand cmd = new SqlCommand(query, connection);

                
            }
            return weapon;
        }

        public Weapon getWeaponByID(int id)
        {
            Weapon weapon = null;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT WeaponName, WeaponDMG, WeaponCRT, WeaponType FROM Weapons WHERE WeaponID=@weaponID", connection))
                {
                    cmd.Parameters.Add("@weaponID", SqlDbType.VarChar).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            int DMG = reader.GetInt32(1);
                            int CRT = reader.GetInt32(2);
                            WeaponType TYPE = WeaponType.MonsterSlayer; //default type is 'MonsterSlayer'

                            string type = reader.GetString(3);
                            switch (type)
                            {
                                case "HumanSlayer":
                                    TYPE = WeaponType.HumanSlayer;
                                    break;
                                case "MonsterSlayer":
                                    TYPE = WeaponType.MonsterSlayer;
                                    break;
                            }
                            weapon = new Weapon(id, DMG, CRT, TYPE, name);
                        }
                    }
                }


                //string query = ";";
                //SqlCommand cmd = new SqlCommand(query, connection);


            }
            return weapon;
        }
    }
}
