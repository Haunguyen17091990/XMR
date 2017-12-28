using MySql.Data.MySqlClient;
using Research.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Research.Implement
{
    public class Repositories : IRepositories
    {
        //public string ConnectionString { get; set; }

        //public Repositories(string connectionString)
        //{
        //    this.ConnectionString = connectionString;
        //}

        //private MySqlConnection GetConnection()
        //{
        //    return new MySqlConnection(ConnectionString);
        //}

        private readonly IDbConnection _conn;

        private MySqlConnection _mySqlConn
            {
                get { return (MySqlConnection)_conn; }
            }

        public Repositories(IDbConnection conn)
        {
            _conn = conn;
        }

        public List<T> ReadAll<T>(string sql) where T : new()
        {
            List<T> results = new List<T>();
            var properties = typeof(T).GetProperties();
            try
            {
                _mySqlConn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, _mySqlConn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = Activator.CreateInstance<T>();
                        foreach (var property in typeof(T).GetProperties())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                        results.Add(item);
                    }
                }
            }
             finally
            {
                _mySqlConn.Dispose();
            }

            return results;

        }
    }
}
