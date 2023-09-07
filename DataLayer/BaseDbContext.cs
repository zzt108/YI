
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace DataLayer
{
    public class BaseDbContext : DbContext
    {
        protected string dbFullPath;

        public bool Delete()
        {
            try
            {
                File.Delete(dbFullPath);
            }
            catch (DirectoryNotFoundException)
            { return true; }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public void ExportToJson(string jsonFileName)
        {
            //using var dbContext = new YiDbContext();

            //dbContext.Database.Migrate();

            // Assuming you have a DbContext instance called "dbContext"
            var jsonData = new JObject();
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string[] array = { "Database", "View", "ChangeTracker", "Model" };
            foreach (var set in GetType().GetProperties())
            {
                if (array.Contains(set.Name))
                    continue;

                var entities = set.GetValue(this);
                try
                {
                    var json = JsonConvert.SerializeObject(entities, settings);
                    jsonData[set.Name] = JArray.Parse(json);
                }
                catch (JsonReaderException ex)
                {
                    Console.WriteLine(ex.ToString());
                    continue;
                }
            }

            var jsonString = jsonData.ToString();

            // Use the JSON string as needed
            Console.WriteLine(jsonString);
            string filePath = jsonFileName;

            File.WriteAllText(filePath, jsonString);
        }

        public List<string> GetTableNames()
        {
            var result = new List<string>();
            using (var connection = new SqliteConnection($"DataSource={dbFullPath}"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tableName = reader.GetString(0);
                            result.Add(tableName);
                            // Do something with the table name
                        }
                    }
                }
            }
            return result;
        }

    }
}