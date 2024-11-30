using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace YiChing
{
    public interface IJsonHandler
    {
        void SaveEntry(HexagramEntry entry);
        List<HexagramEntry> ReadHexagramEntriesFromJson();
        HexagramEntry? GetHexagramDetails(string displayText);
    }

    public class JsonHandler : IJsonHandler
    {
        private readonly string _filePath;
        private readonly ILogger<JsonHandler> _logger;
        private readonly IConfiguration _configuration;
        private readonly int _retentionMonths;

        public JsonHandler(ILogger<JsonHandler> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string myAppFolder = Path.Combine(appDataPath, "Yi");

            try
            {
                if (!Directory.Exists(myAppFolder))
                {
                    Directory.CreateDirectory(myAppFolder);
                }
                _filePath = Path.Combine(myAppFolder, "hexagramEntries.json");
                _retentionMonths = _configuration.GetValue<int>("RetentionMonths", 2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing JsonHandler");
                throw;
            }
        }

        private List<HexagramEntry> RemoveDuplicateEntries(List<HexagramEntry> entries)
        {
            return entries
                .GroupBy(e => e.DisplayText)
                .Select(g => g.OrderByDescending(e => e.Date).First())
                .ToList();
        }


        public void SaveEntry(HexagramEntry entry)
        {
            try
            {
                List<HexagramEntry> entries = ReadHexagramEntriesFromJson();

                entries = RemoveDuplicateEntries(entries);

                // Remove entries older than retention period
                DateTime thresholdDate = DateTime.UtcNow.AddMonths(-_retentionMonths);
                entries = entries.Where(e => e.Date >= thresholdDate).ToList();

                bool entryExists = entries.Any(e => e.Question == entry.Question && e.Answer == entry.Answer);
                if (!entryExists)
                {
                    entries.Add(entry);
                    entries = entries.OrderByDescending(e => e.Date).ToList();

                    var newJson = JsonConvert.SerializeObject(entries, Formatting.Indented);
                    File.WriteAllText(_filePath, newJson);
                    _logger.LogInformation("Successfully saved new hexagram entry");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving hexagram entry");
                throw;
            }
        }

        public List<HexagramEntry> ReadHexagramEntriesFromJson()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<HexagramEntry>>(json) ?? new List<HexagramEntry>();
            }
            return new List<HexagramEntry>();
        }

        public HexagramEntry? GetHexagramDetails(string displayText)
        {
            var entries = ReadHexagramEntriesFromJson();
            var entry = entries.FirstOrDefault(e => e.DisplayText == displayText);
            return entry;
        }
    }
}