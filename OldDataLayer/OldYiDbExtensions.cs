
using DataLayer;
using OldDataLayer;
using System;
using System.Text.RegularExpressions;

namespace OldDataLayer
{
    // Extension method for Hexagram
    public static class OldYiDbExtensions
    {
        public static Hexagram FromOldText(this Hexagram hexagram, string oldText)
        {
            int firstNumber = int.Parse(Regex.Match(oldText, @"\d+").Value);
            hexagram.Value = firstNumber;
            return hexagram;
        }

        public static MainText FromOldText(this MainText mainText, YiDbContext dbContext,  Hexagram hexagram, string oldText)
        {
            List<string> parts;
            string title = string.Empty;
            string summary = string.Empty;

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


            mainText.Hexagram =  hexagram;
            mainText.Title = title;
            mainText.Summary = summary;
            mainText.Language = language;
            mainText.Lines = new List<LineText>();
            return mainText;
        }
    }
}