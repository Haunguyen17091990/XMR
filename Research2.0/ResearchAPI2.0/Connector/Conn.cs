using MySql.Data.MySqlClient;
using ResearchAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchAPI.Connector
{
    public class Conn
    {
        private string connstring;
        public Conn()
        {
            connstring = @"server=localhost;port=3306;database=60sb59c3_taolao;user=root;password=123456;CharSet=utf8;";
        }
        public void Insert(List<NewsRes> lstIns)
        {
            string str = "";
            foreach (NewsRes item in lstIns)
            {
                str += string.Format("INSERT INTO News(Title,TypeID,ImageTH,ImageFull,ShortContent,Content,CreatedDate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}',NOW());", item.Title, item.TypeID, item.ImageTH, item.ImageFull, item.ShortContent, item.Content);
            }
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
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
                    cmdd.ExecuteNonQuery();
                    connMysql.Close();
                }
            }
        }
    }
}
