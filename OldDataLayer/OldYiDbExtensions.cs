
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
            Language language = null;
            if (oldText.Contains("Képjel")) {
                language = dbContext.Languages.First(l => l.Name == "Hungarian");
            }
            else if (oldText.Contains("Action:"))
            {
                language = dbContext.Languages.First(l => l.Name == "English");
            }

            mainText.Hexagram =  hexagram;
            mainText.Title = "Title";
            mainText.Summary = "Summary";
            mainText.Language = language;
            mainText.Lines = new List<LineText>();
            return mainText;
        }
    }
}