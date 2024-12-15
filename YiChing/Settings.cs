using System;
using System.Linq;

namespace YiChing
{
    public class Settings : IDisposable
    {
        string answerLanguage;
        string keyTwo;
        string questionPrefix;
        string answerPrefix;
        string interpretationRequest;
        string translationRequest;

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
            answerLanguage = "English";
            keyTwo = "Value Two";
            questionPrefix = "Question to I Ching:";
            answerPrefix = "I Ching answered:";
            interpretationRequest = "Would you please interpret?";
            translationRequest = "Please translate to";
            KeyThree = new NestedSettings
            {
                Message = "This is a message from the settings"
            };
        }
        #endregion

        public string AnswerLanguage { get => answerLanguage; set => answerLanguage = value; }
        public string KeyTwo { get => keyTwo; set => keyTwo = value; }
        public string QuestionPrefix { get => questionPrefix; set => questionPrefix = value; }
        public string AnswerPrefix { get => answerPrefix; set => answerPrefix = value; }
        public string InterpretationRequest { get => interpretationRequest; set => interpretationRequest = value; }
        public string TranslationRequest { get => translationRequest; set => translationRequest = value; }
        public NestedSettings KeyThree { get; set; }

        #region Methods

        public void LoadValues(Settings? defaults)
        {
            AnswerLanguage = Preferences.Default.Get(nameof(AnswerLanguage), defaults?.AnswerLanguage ?? "English");
            KeyTwo = Preferences.Default.Get(nameof(KeyTwo), defaults?.KeyTwo ?? "Value Two");
            QuestionPrefix = Preferences.Default.Get(nameof(QuestionPrefix), defaults?.QuestionPrefix ?? "Question to I Ching:");
            AnswerPrefix = Preferences.Default.Get(nameof(AnswerPrefix), defaults?.AnswerPrefix ?? "I Ching answered:");
            InterpretationRequest = Preferences.Default.Get(nameof(InterpretationRequest), defaults?.InterpretationRequest ?? "Would you please interpret?");
            TranslationRequest = Preferences.Default.Get(nameof(TranslationRequest), defaults?.TranslationRequest ?? "Please translate to");
            KeyThree = new NestedSettings
            {
                Message = Preferences.Default.Get(nameof(KeyThree.Message), defaults?.KeyThree?.Message ?? "Default Message")
            };
        }
        public void SaveValues()
        {
            Preferences.Default.Set(nameof(Settings.AnswerLanguage), answerLanguage);
            Preferences.Default.Set(nameof(Settings.KeyTwo), keyTwo);
            Preferences.Default.Set(nameof(Settings.QuestionPrefix), questionPrefix);
            Preferences.Default.Set(nameof(Settings.AnswerPrefix), answerPrefix);
            Preferences.Default.Set(nameof(Settings.InterpretationRequest), interpretationRequest);
            Preferences.Default.Set(nameof(Settings.TranslationRequest), translationRequest);
            Preferences.Default.Set(nameof(Settings.KeyThree.Message), KeyThree.Message);
        }
        #endregion
    }

    public class NestedSettings
    {
        public string? Message { get; set; }
    }
}
