using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace YiChing.Configuration
{
    public static class DefaultTexts
    {
        public const string DEFAULT_ANSWER_LANGUAGE = "English";
        public const string DEFAULT_QUESTION_PREFIX = "Question to I Ching:";
        public const string DEFAULT_ANSWER_PREFIX = "I Ching answered:";
        public const string DEFAULT_TRANSLATION_REQUEST = "When finished translate everything to";
        public const string DEFAULT_STEPS_HEADER = "# Steps\n\n1. Translate the hexagrams and question into English.\n2. Provide an interpretation of the main hexagram and how the changing lines influence its meaning.\n3. Explain how the changing hexagram provides additional insight or guidance.";
        public const string DEFAULT_OUTPUT_FORMAT_HEADER = "# Output Format\n\nProvide a paragraph in the requested translation that includes the translated question, hexagrams, and a detailed interpretation of the I Ching reading.\n\nFinally summarise the answer to the reading";
        public const string DEFAULT_NOTES_HEADER = "# Notes\n\n" +
            "- Pay attention to the meanings of both hexagrams and how the changing lines transition the reading from the main to the changing hexagram.\n\n" +
            "- Ensure the interpretation reflects the philosophical concepts of the I Ching, Dzogchen, and Taoism in the context of the question asked.";
        public const string DEFAULT_MESSAGE = "Default Message";
        public const string DEFAULT_URLS = "[\"https://chat.deepseek.com\",\"https://claude.ai/new\",\"https://chatgpt.com/\"]";
    }

    public class Settings : IDisposable, INotifyPropertyChanged
    {
        private string answerLanguage;
        private string questionPrefix;
        private string answerPrefix;
        private string translationRequest;
        private string stepsHeader;
        private string outputFormatHeader;
        private string notesHeader;
        private ObservableCollection<string> savedUrls;
        private string selectedUrl;
    
        public event PropertyChangedEventHandler? PropertyChanged;
    
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Constructor

        public void Dispose()
        {
            SaveValues();
        }
        public Settings(Settings? defaults) : this()
        {
            LoadValues(defaults);
        }

        public Settings()
        {
            answerLanguage = DefaultTexts.DEFAULT_ANSWER_LANGUAGE;
            questionPrefix = DefaultTexts.DEFAULT_QUESTION_PREFIX;
            answerPrefix = DefaultTexts.DEFAULT_ANSWER_PREFIX;
            translationRequest = DefaultTexts.DEFAULT_TRANSLATION_REQUEST;
            stepsHeader = DefaultTexts.DEFAULT_STEPS_HEADER;
            outputFormatHeader = DefaultTexts.DEFAULT_OUTPUT_FORMAT_HEADER;
            notesHeader = DefaultTexts.DEFAULT_NOTES_HEADER;
            savedUrls = new ObservableCollection<string>(
                JsonSerializer.Deserialize<string[]>(DefaultTexts.DEFAULT_URLS) ?? Array.Empty<string>()
            );
            selectedUrl = savedUrls.FirstOrDefault() ?? string.Empty;
        }
        #endregion

        public string AnswerLanguage
        {
            get => answerLanguage;
            set
            {
                if (answerLanguage != value)
                {
                    answerLanguage = value;
                    OnPropertyChanged(nameof(AnswerLanguage));
                }
            }
        }
        
        public string QuestionPrefix
        {
            get => questionPrefix;
            set
            {
                if (questionPrefix != value)
                {
                    questionPrefix = value;
                    OnPropertyChanged(nameof(QuestionPrefix));
                }
            }
        }
        
        public string AnswerPrefix
        {
            get => answerPrefix;
            set
            {
                if (answerPrefix != value)
                {
                    answerPrefix = value;
                    OnPropertyChanged(nameof(AnswerPrefix));
                }
            }
        }
        
        public string TranslationRequest
        {
            get => translationRequest;
            set
            {
                if (translationRequest != value)
                {
                    translationRequest = value;
                    OnPropertyChanged(nameof(TranslationRequest));
                }
            }
        }
        
        public string StepsHeader
        {
            get => stepsHeader;
            set
            {
                if (stepsHeader != value)
                {
                    stepsHeader = value;
                    OnPropertyChanged(nameof(StepsHeader));
                }
            }
        }
        
        public string OutputFormatHeader
        {
            get => outputFormatHeader;
            set
            {
                if (outputFormatHeader != value)
                {
                    outputFormatHeader = value;
                    OnPropertyChanged(nameof(OutputFormatHeader));
                }
            }
        }
        
        public string NotesHeader
        {
            get => notesHeader;
            set
            {
                if (notesHeader != value)
                {
                    notesHeader = value;
                    OnPropertyChanged(nameof(NotesHeader));
                }
            }
        }
        
        public ObservableCollection<string> SavedUrls
        {
            get => savedUrls;
            set
            {
                if (savedUrls != value)
                {
                    savedUrls = value;
                    OnPropertyChanged(nameof(SavedUrls));
                }
            }
        }
        
        public string SelectedUrl
        {
            get => selectedUrl;
            set
            {
                if (selectedUrl != value)
                {
                    selectedUrl = value;
                    OnPropertyChanged(nameof(SelectedUrl));
                }
            }
        }

        #region Methods

        public void LoadValues(Settings? defaults)
        {
            AnswerLanguage = Preferences.Default.Get(nameof(AnswerLanguage), defaults?.AnswerLanguage ?? DefaultTexts.DEFAULT_ANSWER_LANGUAGE);
            QuestionPrefix = Preferences.Default.Get(nameof(QuestionPrefix), defaults?.QuestionPrefix ?? DefaultTexts.DEFAULT_QUESTION_PREFIX);
            AnswerPrefix = Preferences.Default.Get(nameof(AnswerPrefix), defaults?.AnswerPrefix ?? DefaultTexts.DEFAULT_ANSWER_PREFIX);
            TranslationRequest = Preferences.Default.Get(nameof(TranslationRequest), defaults?.TranslationRequest ?? DefaultTexts.DEFAULT_TRANSLATION_REQUEST);
            StepsHeader = Preferences.Default.Get(nameof(StepsHeader), defaults?.StepsHeader ?? DefaultTexts.DEFAULT_STEPS_HEADER);
            OutputFormatHeader = Preferences.Default.Get(nameof(OutputFormatHeader), defaults?.OutputFormatHeader ?? DefaultTexts.DEFAULT_OUTPUT_FORMAT_HEADER);
            NotesHeader = Preferences.Default.Get(nameof(NotesHeader), defaults?.NotesHeader ?? DefaultTexts.DEFAULT_NOTES_HEADER);
            
            var urlsJson = Preferences.Default.Get(nameof(SavedUrls), DefaultTexts.DEFAULT_URLS);
            SavedUrls = new ObservableCollection<string>(
                JsonSerializer.Deserialize<string[]>(urlsJson) ?? Array.Empty<string>()
            );
            SelectedUrl = Preferences.Default.Get(nameof(SelectedUrl), string.Empty);
        }
        
        public void SaveValues()
        {
            Preferences.Default.Set(nameof(Settings.AnswerLanguage), answerLanguage);
            Preferences.Default.Set(nameof(Settings.QuestionPrefix), questionPrefix);
            Preferences.Default.Set(nameof(Settings.AnswerPrefix), answerPrefix);
            Preferences.Default.Set(nameof(Settings.TranslationRequest), translationRequest);
            Preferences.Default.Set(nameof(Settings.StepsHeader), stepsHeader);
            Preferences.Default.Set(nameof(Settings.OutputFormatHeader), outputFormatHeader);
            Preferences.Default.Set(nameof(Settings.NotesHeader), notesHeader);
            
            var urlsJson = JsonSerializer.Serialize(SavedUrls.ToArray());
            Preferences.Default.Set(nameof(SavedUrls), urlsJson);
            Preferences.Default.Set(nameof(SelectedUrl), SelectedUrl);
        }
        #endregion
    }
}