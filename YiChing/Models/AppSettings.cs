namespace YiChing.Models
{
    public class AppSettings
    {
        // Add any application-wide settings here
        public string DefaultLanguage { get; set; } = "English";
        public string AnswerLanguage { get; set; } = "English";
        public bool IsDarkModeEnabled { get; set; } = false;
        
        // Constructor with default settings
        public AppSettings()
        {
            // Initialize default settings
        }
    }
}