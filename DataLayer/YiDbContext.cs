
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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

    }

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
        public string Text { get; set; }
        public List<LineText> Lines { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }

    public class Hexagram
    {
        [Key]
        public int Value { get; set; }
        public string Name { get; set; }
        public List<MainText> Texts { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set;}

        //todo : hexagram is nullable
        public Hexagram? BaseHexagram { get; set; }
        public Hexagram? ChangedHexagram { get; set; }
    }


}