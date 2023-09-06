//#define WINDOWS_UWP
using System;
using System.IO;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataTools
{
    public class Tests
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
        public void CreateData()
        {
            // Configure the database connection
            //            using var dbContext = new YiDbContext(@"c:\Git\Yi\");
            using var dbContext = new YiDbContext();
            if (!dbContext.Delete()) Assert.Fail("Delete unsuccessfull");

            dbContext.Database.Migrate();

            //List<object> sampleData = new List<object>();
            //sampleData.Add(new Language { Id = 1, LanguageName = "English" });
            //sampleData.Add(new Language { Id = 2, LanguageName = "Spanish" });
            //sampleData.Add(new Language { Id = 3, LanguageName = "French" });

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
            var q2 = new Question { Text = "What is your favorite food?" , BaseHexagram = h1, ChangedHexagram = h2 };
            var q3 = new Question { Text = "What is your favorite movie?" };
            dbContext.Questions.Add(q1);
            dbContext.Questions.Add(q2);
            dbContext.Questions.Add(q3);

            dbContext.SaveChanges();
/*
            List<SampleData> GenerateSampleData()
            {
                List<SampleData> sampleData = new List<SampleData>();


                // Generate sample data for LineTexts


                // Generate sample data for Hexagrams
                sampleData.Add(new SampleData { HexagramId = 1, HexagramName = "Hexagram 1" });
                sampleData.Add(new SampleData { HexagramId = 2, HexagramName = "Hexagram 2" });
                sampleData.Add(new SampleData { HexagramId = 3, HexagramName = "Hexagram 3" });

                // Generate sample data for Questions

                return sampleData;
            }
*/
        }
    }
}