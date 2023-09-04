using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace DataLayerMaui
{
    // All the code in this file is included in all platforms.
    public class YiDbContext : DbContext
    {
        public DbSet<Language> Languages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Define the SQLite database file path
            // string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydatabase.db3");
            //Android
            string yiDbName = "yidb.sqlite";
            string dbPath = "Empty";
#if __ANDROID__
    // Android-specific path
    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), yiDbName);
#elif WINDOWS_UWP
    // Windows-specific path
    dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, yiDbName);
#endif
            // Configure the database connection
            optionsBuilder.UseSqlite($"Filename={dbPath}");
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
        public string Question { get; set; }
    }
}