﻿using System;
using Newtonsoft.Json;
//using System.Text.Json.Serialization;

namespace YiChing
{
    public class HexagramEntry
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime Date { get; set; }
        public int CurrentHexagram { get; set; }
        public int NewHexagram { get; set; }
    
        public HexagramEntry(string question, string answer, int currentHexagram = 0, int newHexagram = 0)
        {
            Question = question;
            Answer = answer;
            Date = DateTime.Now;
            CurrentHexagram = currentHexagram;
            NewHexagram = newHexagram;
        }

        [JsonIgnore]
        public string DisplayText => $"{Date:yy-MM-dd HH:mm} {Question}";
    }
}
