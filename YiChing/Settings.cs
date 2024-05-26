using System;
using System.Linq;

namespace YiChing
{
    public class Settings : IDisposable
    {
        string answerLanguage;
        string keyTwo;

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
            KeyThree = new NestedSettings
            {
                Message = "This is a message from the settings"
            };
        }
        #endregion

        public string AnswerLanguage { get => answerLanguage; set => answerLanguage = value; }
        public string KeyTwo { get => keyTwo; set => keyTwo = value; }
        public NestedSettings KeyThree { get; set; }

        #region Methods

        public void LoadValues(Settings? defaults)
        {
            AnswerLanguage = Preferences.Default.Get(nameof(AnswerLanguage), defaults?.AnswerLanguage);
            KeyTwo = Preferences.Default.Get(nameof(KeyTwo), defaults?.KeyTwo);
            KeyThree = new NestedSettings
            {
                Message = Preferences.Default.Get(nameof(KeyThree.Message), defaults?.KeyThree.Message)
            };
        }
        public void SaveValues()
        {
            Preferences.Default.Set(nameof(Settings.AnswerLanguage), answerLanguage);
            Preferences.Default.Set(nameof(Settings.KeyTwo), keyTwo);
            Preferences.Default.Set(nameof(Settings.KeyThree.Message), KeyThree.Message);
        }
        #endregion
    }

    public class NestedSettings
    {
        public string? Message { get; set; }
    }
}
