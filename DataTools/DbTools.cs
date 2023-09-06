//#define WINDOWS_UWP
using System;
using System.IO;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OldDataLayer;
using FluentAssertions;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DataTools
{
    public class DbTools
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateOpen_Database()
        {
            // Configure the database connection
            //            using var dbContext = new YiDbContext(@"c:\Git\Yi\");
            using var dbContext = new YiDbContext();
            dbContext.Database.Migrate();
        }
        [Test]
        public void OpenOldDatabase()
        {
            // Configure the database connection
            using var dbContext = new OldYiDbContext(@"c:\Git\Yi\");
            dbContext.Database.OpenConnection();
            var tables = dbContext.GetTableNames();
            tables.Count.Should().BeGreaterThan(0);
            var records = dbContext.GuaExp.ToList();
            records.Count.Should().BeGreaterThan(0);
        }


        [Test]
        public void CreateData()
        {
            // Configure the database connection
            //            using var dbContext = new YiDbContext(@"c:\Git\Yi\");
            using var dbContext = new YiDbContext();
            if (!dbContext.Delete()) Assert.Fail("Delete unsuccessfull");

            dbContext.Database.Migrate();

            Language langEng = new Language { Name = "English" };
            dbContext.Languages.Add(langEng);
            Language langHun = new Language { Name = "Hungarian" };
            dbContext.Languages.Add(langHun);

            MainText text1 = new MainText { Language = langEng, Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit" };
            dbContext.Texts.Add(text1);
            MainText text2 = new MainText { Language = langEng, Text = "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua" };
            dbContext.Texts.Add(text2);
            MainText text3 = new MainText { Language = langEng, Text = "Ut enim ad minim veniam, quis nostrud exercitation ullamco" };
            dbContext.Texts.Add(text3);

            var lt1 = new LineText { MainText = text1, Text = "Lorem ipsum dolor sit amet" };
            var lt2 = new LineText { MainText = text2, Text = "consectetur adipiscing elit" };
            var lt3 = new LineText { MainText = text3, Text = "sed do eiusmod tempor incididunt" };
            dbContext.LineTexts.Add(lt1);
            dbContext.LineTexts.Add(lt2);
            dbContext.LineTexts.Add(lt3);

            var h1 = new Hexagram { Value = 1, Name = "Hexagram 1" };
            dbContext.Hexagrams.Add(h1);
            var h2 = new Hexagram { Value = 2, Name = "Hexagram 2" };
            dbContext.Hexagrams.Add(h2);
            var h3 = new Hexagram { Value = 3, Name = "Hexagram 3" };
            dbContext.Hexagrams.Add(h3);

            var q1 = new Question { Text = "What is your favorite color?", BaseHexagram = h1 };
            var q2 = new Question { Text = "What is your favorite food?", BaseHexagram = h1, ChangedHexagram = h2 };
            var q3 = new Question { Text = "What is your favorite movie?" };
            dbContext.Questions.Add(q1);
            dbContext.Questions.Add(q2);
            dbContext.Questions.Add(q3);

            dbContext.SaveChanges();
        }

        [Test]
        public void ExportToJson()
        {
            using var dbContext = new YiDbContext();

            dbContext.Database.Migrate();

            // Assuming you have a DbContext instance called "dbContext"
            var jsonData = new JObject();
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string[] array = { "Database", "View", "ChangeTracker", "Model" };
            foreach (var set in dbContext.GetType().GetProperties())
            {
                if (array.Contains(set.Name))
                    continue;

                var entities = set.GetValue(dbContext);
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
            string filePath = @"c:\git\yi\export.json";

            File.WriteAllText(filePath, jsonString);
        }
    }
}