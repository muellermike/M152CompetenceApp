using CompetenceAppM152.Common.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetenceAppM152.Server.DataAccessLayer.Repositories
{
    public class GalleryRepository
    {
        private string _connectionString;

        public GalleryRepository()
        {
            _connectionString = "server=localhost;uid=root;pwd=;database=gallery;";
        }

        public List<Gallery> GetGalleries()
        {
            List<Gallery> outGalleries = new List<Gallery>();
            Gallery gallery;
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = _connectionString;
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT g.PK AS GalleryPK, g.Name, g.Description, (SELECT p.Path FROM picture p WHERE p.PK = g.titlepictureFK) AS Path, (SELECT p.PK FROM picture p WHERE p.PK = g.titlepictureFK) AS PicturePK FROM gallery g";
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.IsDBNull(4))
                    {
                        gallery = new Gallery(new Guid(reader.GetString("GalleryPK")), reader.GetString("Name"), reader.GetString("Description"));
                    }
                    else
                    {
                        gallery = new Gallery(new Guid(reader.GetString("GalleryPK")), reader.GetString("Name"), reader.GetString("Description"), new Picture(new Guid(reader.GetString("PicturePK")), reader.GetString("Path")));
                    }
                    outGalleries.Add(gallery);
                }

                conn.Close();
            }
            catch (Exception e)
            {
            }
            return outGalleries;
        }
    }
}
