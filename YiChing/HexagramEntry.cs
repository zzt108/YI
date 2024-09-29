using System;
using Newtonsoft.Json;
//using System.Text.Json.Serialization;

namespace YiChing
{
    public class HexagramEntry
    {
        public string Question { get; set; }
        public int Hexagram { get; set; }
        public int ChangedHexagram { get; set; }
        public DateTime Date { get; set; }

        public HexagramEntry(string question, int hexagram, int changedHexagram)
        {
            Question = question;
            Hexagram = hexagram;
            Date = DateTime.Now; // Current date and time
            ChangedHexagram = changedHexagram;
        }

        [JsonIgnore]
        public string DisplayText => $"{Date:yy-MM-dd HH:mm} {Question}";
    }
}
