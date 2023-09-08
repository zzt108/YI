
using DataLayer;
using Microsoft.EntityFrameworkCore;
using OldDataLayer;
using System;
using System.Text.RegularExpressions;

namespace OldDataLayer
{
    // Extension method for Hexagram
    public static class OldYiDbExtensions
    {
        public static Hexagram FromOldText(this Hexagram hexagram, YiDbContext dbContext, string oldText)
        {
            int firstNumber = int.Parse(Regex.Match(oldText, @"\d+").Value);
            hexagram.Value = firstNumber;

            var mainTexts = OldYiDbContext.ParseHunEngHexagram(oldText);
            foreach (string mText in mainTexts)
            {
                var mainText = new MainText().FromOldText(dbContext, hexagram, mText.Trim());
                hexagram.Texts.Add(mainText);
            }
            return hexagram;
        }

        public static MainText FromOldText(this MainText mainText, YiDbContext dbContext,  Hexagram hexagram, string oldText)
        {
            List<string> parts = null;
            string title = string.Empty;
            string summary = string.Empty;
            mainText.Lines = new List<LineText>();

            Language language = null;
            if (oldText.Contains("Képjel")) {
                language = dbContext.Languages.First(l => l.Name == "Hungarian");
                parts = OldYiDbContext.ParseHunHexagram(oldText);
                string[] lines = parts[1].Split(new[] { '\n' }, 2);
                title = lines[0];
                summary = lines[1];
            }
            else if (oldText.Contains("Action:"))
            {
                language = dbContext.Languages.First(l => l.Name == "English");
                parts = OldYiDbContext.ParseEngHexagram(oldText);
                string[] lines = parts[0].Split(new[] { '\n' }, 2);
                title = lines[0];
                summary = lines[1];
            }

            foreach (string lineText in parts.Skip(parts.Count-6))
            {
                var lt1 = new LineText { MainText = mainText, Text = lineText };
                mainText.Lines.Add(lt1);
            }

            mainText.Hexagram =  hexagram;
            mainText.Title = title;
            mainText.Summary = summary;
            mainText.Language = language;
            return mainText;
        }
    }
}