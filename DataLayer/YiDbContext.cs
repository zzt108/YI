
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;


namespace DataLayer
{

public class YiDbContext : DbContext
    {
        string yiDbName = "yidb.sqlite";

        private string dbFullPath;
        
        public DbSet<Language> Languages => Set<Language>();
        //public DbSet<LineText> LineTexts { get; set; }
        //public DbSet<Text> Texts { get; set; }
        //public DbSet<Hexagram> Hexagrams { get; set; }
        //public DbSet<Question> Questions { get; set; }
        
        public YiDbContext()
        {
            string localFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            dbFullPath = Path.Combine(localFolderPath, yiDbName);
        }

        public YiDbContext(string dbPath)
        {
            this.dbFullPath = Path.Combine(dbPath, yiDbName); ;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Define the SQLite database file path
            // string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydatabase.db3");
            //Android

            // Configure the database connection
            optionsBuilder.UseSqlite($"Filename={dbFullPath}");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Text>().ToTable("Texts")
        //        .HasOne<Language>(l => l.Language);
        //    modelBuilder.Entity<Hexagram>().ToTable("Hexagrams")
        //        .HasMany(t => t.Texts);
        //    modelBuilder.Entity<Question>().ToTable("Questions")
        //        .HasOne(q => q.BaseHexagram)
        //        .WithMany(h => h.Questions);
        //}
    }

    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
//        public List<Text> Texts { get; set; }
    }

    public class LineText
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class Text
    {
        public int Id { get; set; }
        public List<LineText> Lines { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }

    public class Hexagram
    {
        public int Id { get; set; }
        public List<Text> Texts { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set;}

        //todo : hexagram is nullable
        public Hexagram BaseHexagram { get; set; }
        public Hexagram ChangedHexagram { get; set; }
    }


}