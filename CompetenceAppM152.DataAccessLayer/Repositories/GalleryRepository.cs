using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetenceAppM152.DataAccessLayer.Repositories
{
    public class GalleryRepository
    {
        public GalleryRepository()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=gallery;";
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM album";
                cmd.Prepare();
                MySqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
            }
        }
    }
}
