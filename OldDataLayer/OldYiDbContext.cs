
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;


namespace OldDataLayer
{

    public class OldYiDbContext : DbContext
    {
        const string yiDbName = "yi.sqlite";
        const string yiDbNameEng = "yi - EnglishOnly.sqlite";
        const string yiDbSubFolder = "OldData";

        private string dbFullPath;
        public DbSet<GuaExp> GuaExp => Set<GuaExp>();

        public OldYiDbContext()
        {
            string localFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), yiDbSubFolder);

            if (!Directory.Exists(localFolderPath))
            {
                Directory.CreateDirectory(localFolderPath);
            }

            dbFullPath = Path.Combine(localFolderPath, yiDbName);
        }

        public OldYiDbContext(string dbPath)
        {
            this.dbFullPath = Path.Combine(dbPath, yiDbSubFolder, yiDbName); ;
        }

        private bool Delete()
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database connection
            optionsBuilder.UseSqlite($"Filename={dbFullPath}");
        }

        public List<string> GetTableNames()
        {
            var result = new List<string>();
            using (var connection = new SqliteConnection($"Data Source={dbFullPath}" ))
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

    [Table("guaexp")]
    public class GuaExp
    {
        public int Id { get; set; }
        public string Exp { get; set; }
    }


}