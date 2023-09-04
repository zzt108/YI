using Microsoft.EntityFrameworkCore;

namespace DataLayer
{

public class YiDbContext : DbContext
    {
        public DbSet<Language> Languages { get; set; }
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
    }

    public class Text
    {
        public int Id { get; set; }
        public int HexaGramId { get; set; }
    }

    public class Hexagram
    {
        public int Id { get; set; }
    }

    public class History
    {
        public int Id { get; set; }
        public int HexaGramId { get; set; }
        public string Question { get; set;}
    }


}