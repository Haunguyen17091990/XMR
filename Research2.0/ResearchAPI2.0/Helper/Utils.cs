using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchAPI.Helper
{
    public class UtilHelpers
    { 
        public IConfiguration Configuration { get; }

        public static string GetConnectionString(string strKeyName)
        { 
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            var connectionStringConfig = builder.Build();

            return connectionStringConfig.GetConnectionString(
                    strKeyName);
        }

        public static void WriteToFile(string text, Exception objEx = null)
        {
            string path = "D:\\ResearchAPI.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                if (objEx != null)
                { 
                    writer.WriteLine("objEx.Message : " + objEx.Message);
                    writer.WriteLine("objEx.StackTrace : " + objEx.StackTrace);
                }
                writer.Close();
            }
        }
    } 
}
