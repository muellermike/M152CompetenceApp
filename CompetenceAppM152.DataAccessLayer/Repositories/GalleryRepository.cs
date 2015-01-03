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
            MySqlConnection conn = new MySqlConnection();
            try
            {
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

            }
            catch (Exception e)
            {
            }

            conn.Close();

            return outGalleries;
        }

        public bool InsertNewGallery(Gallery gallery)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = _connectionString;
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO gallery VALUES ('{0}', '{1}', '{2}', {3})";
                cmd.CommandText = string.Format(cmd.CommandText, gallery.Identifier, gallery.Name, gallery.Description, "NULL");
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }

        public List<Picture> GetPicturesByGalleryID(Guid identifier)
        {
            List<Picture> outPictures = new List<Picture>();
            MySqlConnection conn = new MySqlConnection();
            Picture picture;
            try
            {
                conn.ConnectionString = _connectionString;
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT p.PK AS PicturePK, p.Title, p.Description, p.Path FROM picture p WHERE GalleryFK = '{0}'";
                cmd.CommandText = string.Format(cmd.CommandText, identifier.ToString());
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.IsDBNull(2))
                    {
                        picture = new Picture(new Guid(reader.GetString("PicturePK")), reader.GetString("Title"), string.Empty, reader.GetString("Path"), identifier);
                    }
                    else
                    {
                        picture = new Picture(new Guid(reader.GetString("PicturePK")), reader.GetString("Title"), reader.GetString("Description"), reader.GetString("Path"), identifier);
                    }
                    outPictures.Add(picture);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
            }

            return outPictures;
        }

        public Gallery GetGalleryByID(Guid identifier)
        {
            Gallery outGallery = new Gallery();
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = _connectionString;
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT g.PK AS GalleryPK, g.Name, g.Description, (SELECT p.Path FROM picture p WHERE p.PK = g.titlepictureFK) AS Path, (SELECT p.PK FROM picture p WHERE p.PK = g.titlepictureFK) AS TitlePicturePK FROM gallery g WHERE g.PK = '{0}'";
                cmd.CommandText = string.Format(cmd.CommandText, identifier.ToString());
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (reader.IsDBNull(4))
                {
                    outGallery = new Gallery(new Guid(reader.GetString("GalleryPK")), reader.GetString("Name"), reader.GetString("Description"));
                }
                else
                {
                    outGallery = new Gallery(new Guid(reader.GetString("GalleryPK")), reader.GetString("Name"), reader.GetString("Description"), new Picture(new Guid(reader.GetString("TitlePicturePK")), reader.GetString("Path")));
                }

                conn.Close();
            }
            catch (Exception e)
            {
            }
            return outGallery;
        }

        public bool DeleteGallery(Guid identifier)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = _connectionString;
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM picture WHERE GalleryFK = @GalleryKey";
                cmd.Parameters.AddWithValue("@GalleryKey", identifier.ToString());
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "DELETE FROM gallery WHERE PK = @GalleryKey";
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }

        public bool SaveTitlePicture(Picture titlePicture)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = _connectionString;
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO picture (PK, Title, Description, Path, GalleryFK) VALUES (@PicturePK, @Title, @Desc, @Path, @GalleryFK)";
                cmd.Parameters.AddWithValue("@PicturePK", titlePicture.Identifier.ToString());
                cmd.Parameters.AddWithValue("@Title", titlePicture.Title);
                cmd.Parameters.AddWithValue("@Desc", titlePicture.Description);
                cmd.Parameters.AddWithValue("@Path", titlePicture.Path);
                cmd.Parameters.AddWithValue("@GalleryFK", titlePicture.GalleryFK.ToString());
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE gallery SET TitlepictureFK = @PicturePK WHERE PK = @GalleryFK";
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }

    }
}
