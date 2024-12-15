using Microsoft.Extensions.Configuration;

namespace YiChing;

public partial class CvConfig : ContentView
{
    private readonly IConfiguration _configuration;
    private readonly MainPage _mainPage;

    public Settings? Settings { get; set; }
    public Settings? Defaults { get; set; }

    public CvConfig(MainPage page, IConfiguration configuration)
    {
        InitializeComponent();
        _mainPage = page;
        _configuration = configuration;
        btnReturn.Clicked += btnReturn_Click;
        btnReset.Clicked += btnReset_Click;
        #pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        myPicker.SelectedIndexChanged += Picker_SelectedIndexChanged;
        #pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        LoadSettings();
        BindingContext = this;
        myPicker.SelectedItem = Settings?.AnswerLanguage ?? "English"; // Default to "English" if null
    }
    private void LoadSettings()
    {
        //Defaults = _configuration?.GetRequiredSection("Settings")?.Get<Settings>();
        Settings = new Settings(Defaults);
    }

    private void btnReturn_Click(object? sender, EventArgs e)
    {
        Settings?.SaveValues();
        _mainPage.Content = _mainPage.CVHexagram;
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedItem = picker.SelectedItem;
        txtAnswerLanguage.Text = selectedItem?.ToString();
        
        if (Settings != null) // Ensure Settings is not null before accessing
        {
            Settings.AnswerLanguage = selectedItem?.ToString();
        }
    }
    
    private void btnReset_Click(object? sender, EventArgs e)
    {
        // Létrehozunk egy ideiglenes Settings példányt az alapértelmezett értékekkel
        var defaultSettings = new Settings();
        
        // Egyenként beállítjuk az értékeket, hogy a PropertyChanged események kiváltódjanak
        if (Settings != null)
        {
            Settings.AnswerLanguage = defaultSettings.AnswerLanguage;
            Settings.QuestionPrefix = defaultSettings.QuestionPrefix;
            Settings.AnswerPrefix = defaultSettings.AnswerPrefix;
            Settings.TranslationRequest = defaultSettings.TranslationRequest;
            Settings.StepsHeader = defaultSettings.StepsHeader;
            Settings.OutputFormatHeader = defaultSettings.OutputFormatHeader;
            Settings.NotesHeader = defaultSettings.NotesHeader;
            Settings.KeyThree = defaultSettings.KeyThree;
            
            // Frissítjük a Picker értékét
            myPicker.SelectedItem = Settings.AnswerLanguage;
            
            // Elmentjük az alapértelmezett értékeket
            Settings.SaveValues();
        }
    }
    }