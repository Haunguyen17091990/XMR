using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
                MySqlCommand cmd = new MySqlCommand("select NewsID,title,TypeID,MenuID,SourceID,ImageTH,ImageFull,ShortContent,Content,ViewNumber,CreatedDate from news order by CreatedDate desc limit 500", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NewsRes()
                        {
                            NewsID = !reader.IsDBNull(0)?reader.GetInt32("NewsID"):0,
                            Title = !reader.IsDBNull(1) ? reader.GetString("title"):"",
                            TypeID = !reader.IsDBNull(2) ? reader.GetInt32("TypeID"):0,
                            MenuID = !reader.IsDBNull(3) ? reader.GetInt32("MenuID"):0,
                            SourceID = !reader.IsDBNull(4) ? reader.GetInt32("SourceID"):0,
                            ImageTH = !reader.IsDBNull(5) ? reader.GetString("ImageTH"):"",
                            ImageFull = !reader.IsDBNull(6) ? reader.GetString("ImageFull"):"",
                            ShortContent = !reader.IsDBNull(7) ? reader.GetString("ShortContent"):"",
                            Content = !reader.IsDBNull(8) ? reader.GetString("Content"):"",
                            ViewNumber = !reader.IsDBNull(9) ? reader.GetInt32("ViewNumber"):0,
                            CreatedDate = !reader.IsDBNull(10) ? reader.GetDateTime("CreatedDate"):DateTime.Today
                        });
                    }
                }
            }
            return list;
        }

        public int Update(int NewsID)
        {
            int result = 0;
            string str = string.Format("update news set ViewNumber = ViewNumber + 1 where NewsID ={0}", NewsID);
            using (MySqlConnection connMysql = GetConnection())
            {
                using (MySqlCommand cmdd = connMysql.CreateCommand())
                {
                    if (connMysql.State == ConnectionState.Open)
                    {
                        connMysql.Close();
                    }
                    connMysql.Open();
                    cmdd.CommandText = str;
                    cmdd.CommandType = System.Data.CommandType.Text;
                    cmdd.Connection = connMysql;
                    result = cmdd.ExecuteNonQuery();
                    connMysql.Close();
                }
            }
            return result;
        }
    }
}
