// TimBot DB page

using System.IO;
using System;
using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class DB
    {
        public static string GetConnectionString()
        {
            string sFName = "/Orders.db";
            string sPrefix = "Data Source=";
            string sReturn = sPrefix + Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + sFName;
            string sPath = Directory.GetCurrentDirectory();
            string[] subs = sPath.Split(Path.DirectorySeparatorChar);
            for (int n = subs.Length - 1; n > 1; n--)
            {
                // skip first empty path
                string sResult = "";
                for (int nsubs = 1; nsubs < n; nsubs++)
                {
                    sResult += Path.DirectorySeparatorChar + subs[nsubs];
                }
                string[] aFiles = Directory.GetFiles(sResult, "README.md", System.IO.SearchOption.TopDirectoryOnly);
                if (aFiles.Length > 0)
                {
                    sReturn = sPrefix + sResult + sFName;
                    break;
                }
            }

            using (var connection = new SqliteConnection(sReturn))
            {
                connection.Open();

                var commandCreateTable = connection.CreateCommand();
                commandCreateTable.CommandText =
                    @"
                        CREATE TABLE IF NOT EXISTS orders (
                            cafe_location TEXT ,
                            item TEXT,
                            size jTEXT,
                            wiped_cream TEXT,
                            delivery TEXT,
                            location TEXT,
                            payment TEXT,
                            code TEXT
                        );
                        ";
                commandCreateTable.ExecuteNonQuery();
            }

            return sReturn;

            //string fileName = "Orders.db";
            //string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            //// Search for the Orders.db file in the hierarchy up to the root
            //string directory = Directory.GetCurrentDirectory();
            //while (!File.Exists(Path.Combine(directory, fileName)) && !string.IsNullOrEmpty(directory))
            //{
            //    directory = Path.GetDirectoryName(directory);
            //}

            //if (string.IsNullOrEmpty(directory))
            //{
            //    // If Orders.db is not found, create it on the desktop
            //    directory = desktopDirectory;
            //}

            //return $"Data Source={Path.Combine(directory, fileName)}";

        }
    }
}


