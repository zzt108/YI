using Newtonsoft.Json;
namespace YiChing;
public class JsonHandler
{
    private readonly string filePath;

    public JsonHandler()
    {
        // Get the local application data path and create a sub-folder if necessary
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string myAppFolder = Path.Combine(appDataPath, "Yi");

        // Create the directory if it doesn't exist
        if (!Directory.Exists(myAppFolder))
        {
            Directory.CreateDirectory(myAppFolder);
        }

        // Set the path for the JSON file
        filePath = Path.Combine(myAppFolder, "hexagramEntries.json");
    }

    public void SaveEntry(HexagramEntry entry)
    {
        List<HexagramEntry> entries;

        // Check if the file exists. If yes, read the existing entries.
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            entries = JsonConvert.DeserializeObject<List<HexagramEntry>>(json) ?? new List<HexagramEntry>();
        }
        else
        {
            entries = new List<HexagramEntry>();
        }

        // Add the new entry to the list
        entries.Add(entry);

        // Sort the entries by date descending
        entries = entries.OrderByDescending(e => e.Date).ToList();

        // Serialize the sorted list back to JSON
        var newJson = JsonConvert.SerializeObject(entries, Formatting.Indented);
        File.WriteAllText(filePath, newJson);
    }
}