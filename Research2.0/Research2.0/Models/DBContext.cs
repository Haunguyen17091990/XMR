using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchWeb.Models
{
    public class DBContext
    {
        public string ConnectionString { get; set; }

        public DBContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<NewsRes> GetAllNewsRes()
        {
            List<NewsRes> list = new List<NewsRes>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from news order by CreatedDate desc limit 500", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NewsRes()
                        {
                            NewsID = reader.GetInt32("NewsID"),
                            Title = reader.GetString("title"),
                            TypeID = reader.GetInt32("TypeID"),
                            MenuID = reader.GetInt32("MenuID"),
                            SourceID = reader.GetInt32("SourceID"),
                            ImageTH = reader.GetString("ImageTH"),
                            ImageFull = reader.GetString("ImageFull"),
                            ShortContent = reader.GetString("ShortContent"),
                            Content = reader.GetString("Content"),
                            ViewNumber = reader.GetInt32("ViewNumber"),
                            CreatedDate = reader.GetDateTime("CreatedDate")
                        });
                    }
                }
            }
            return list;
        }
    }
}
