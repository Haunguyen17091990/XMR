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
            connstring = @"server=localhost;port=3306;database=60sb59c3_taolao;user=root;password=123456";
        }
        public void Insert(List<NewsRes> lstIns)
        {
            foreach (NewsRes item in lstIns)
            {
                using (MySqlConnection connMysql = new MySqlConnection(connstring))
                {
                    using (MySqlCommand cmdd = connMysql.CreateCommand())
                    {
                        if (connMysql.State == ConnectionState.Open)
                        {
                            connMysql.Close();
                        }
                        connMysql.Open();
                        cmdd.CommandText = "INSERT INTO News(Title,TypeID,MenuID,SourceID,ImageTH,ImageFull,ShortContent,Content,ViewNumber,CreatedDate)";
                        cmdd.CommandText +="VALUES("+item.Title+","+item.TypeID+","+item.MenuID+","+item.SourceID+","+item.ImageTH+","+item.ImageFull+","+item.ShortContent+","+item.Content+","+item.ViewNumber+","+DateTime.Today.ToString("dd/MM/YYYY")+")";
                        cmdd.CommandType = System.Data.CommandType.Text;
                        cmdd.Connection = connMysql;
                        connMysql.Close();
                    }
                }
            }
        }
    }
}
