using System;
using Newtonsoft.Json;
//using System.Text.Json.Serialization;

namespace YiChing
{
    public class HexagramEntry
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime Date { get; set; }

        public HexagramEntry(string question, string answer)
        {
            Question = question;
            Answer = answer;
            Date = DateTime.Now; // Current date and time
        }

        [JsonIgnore]
        public string DisplayText => $"{Date:yy-MM-dd HH:mm} {Question}";
    }
}
