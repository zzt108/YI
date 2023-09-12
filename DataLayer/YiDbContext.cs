using Microsoft.EntityFrameworkCore;
using System;

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DataLayer
{

    public class YiDbContext : BaseDbContext
    {
        const string yiDbName = "yidb.sqlite";
        const string yiDbSubFolder = "YiChing";

        public DbSet<Language> Languages => Set<Language>();
        public DbSet<LineText> LineTexts => Set<LineText>();
        public DbSet<MainText> Texts => Set<MainText>();
        public DbSet<Hexagram> Hexagrams => Set<Hexagram>();
        public DbSet<Question> Questions => Set<Question>();

        public YiDbContext()
        {
            string localFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), yiDbSubFolder);

            if (!Directory.Exists(localFolderPath))
            {
                Directory.CreateDirectory(localFolderPath);
            }

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
    }

    public class LineText
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public virtual MainText MainText { get; set; }
    }

    public class MainText
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public List<LineText> Lines { get; set; }

        //        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public Hexagram Hexagram { get; set; }
    }

    public class Hexagram
    {
        public Hexagram()
        {
            Texts = new List<MainText>();
        }

        public Hexagram(int value):this()
        {
            this.Value = value;
        }

        [Key]
        public int Value { get; set; }
        public virtual List<MainText> Texts { get; set; }

    }

    public class Question
    {
        public int Id { get; set; }
        public DateTime Date {get;set;}
        public string Text { get; set; }

        public Hexagram? BaseHexagram { get; set; }
        public Hexagram? ChangedHexagram { get; set; }
    }


}