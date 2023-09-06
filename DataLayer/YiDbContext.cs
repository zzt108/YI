
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
using System;
using System.IO;


namespace DataLayer
{

public class YiDbContext : DbContext
    {
        string yiDbName = "yidb.sqlite";

        private string dbFullPath;
        public YiDbContext()
        {
            string localFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            dbFullPath = Path.Combine(localFolderPath, yiDbName);

        }

        public YiDbContext(string dbPath)
        {
            dbPath = Path.Combine(dbPath, yiDbName);
            this.dbFullPath = dbPath;
        }
        public DbSet<Language> Languages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Define the SQLite database file path
            // string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydatabase.db3");
            //Android

            // Configure the database connection
            optionsBuilder.UseSqlite($"Filename={dbFullPath}");
        }
    }

    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LineText
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class Text
    {
        public int Id { get; set; }
        public int HexaGramId { get; set; }
    }

    public class Hexagram
    {
        public int Id { get; set; }
    }

    public class History
    {
        public int Id { get; set; }
        public int HexaGramId { get; set; }
        public string Question { get; set;}
    }


}