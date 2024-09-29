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
    }
}
