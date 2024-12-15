﻿using System;
using System.Linq;

namespace YiChing
{
    public class Settings : IDisposable
    {
        string answerLanguage;
        string questionPrefix;
        string answerPrefix;
        string translationRequest;
        string stepsHeader;
        string outputFormatHeader;
        string notesHeader;

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
            questionPrefix = "Question to I Ching:";
            answerPrefix = "I Ching answered:";
            translationRequest = "Please translate to";
            stepsHeader = "# Steps\n\n1. Translate the hexagrams and question into English.\n2. Provide an interpretation of the main hexagram and how the changing lines influence its meaning.\n3. Explain how the changing hexagram provides additional insight or guidance.";
            outputFormatHeader = "# Output Format\n\n" +
                        "Provide a paragraph in the requested translation that includes the translated question, hexagrams, and a detailed interpretation of the I Ching reading.";
            notesHeader = "# Notes\n\n" +
                         "- Pay attention to the meanings of both hexagrams and how the changing lines transition the reading from the main to the changing hexagram.\n\n" +
                         "- Ensure the interpretation reflects the philosophical concepts of the I Ching in the context of the question asked.";
            KeyThree = new NestedSettings
            {
                Message = "This is a message from the settings"
            };
        }
        #endregion

        public string AnswerLanguage { get => answerLanguage; set => answerLanguage = value; }
        public string QuestionPrefix { get => questionPrefix; set => questionPrefix = value; }
        public string AnswerPrefix { get => answerPrefix; set => answerPrefix = value; }
        public string TranslationRequest { get => translationRequest; set => translationRequest = value; }
        public string StepsHeader { get => stepsHeader; set => stepsHeader = value; }
        public string OutputFormatHeader { get => outputFormatHeader; set => outputFormatHeader = value; }
        public string NotesHeader { get => notesHeader; set => notesHeader = value; }
        public NestedSettings KeyThree { get; set; }

        #region Methods

        public void LoadValues(Settings? defaults)
        {
            AnswerLanguage = Preferences.Default.Get(nameof(AnswerLanguage), defaults?.AnswerLanguage ?? "English");
            QuestionPrefix = Preferences.Default.Get(nameof(QuestionPrefix), defaults?.QuestionPrefix ?? "Question to I Ching:");
            AnswerPrefix = Preferences.Default.Get(nameof(AnswerPrefix), defaults?.AnswerPrefix ?? "I Ching answered:");
            TranslationRequest = Preferences.Default.Get(nameof(TranslationRequest), defaults?.TranslationRequest ?? "Please translate to");
            StepsHeader = Preferences.Default.Get(nameof(StepsHeader), defaults?.StepsHeader ?? "# Steps\n\n1. Translate the hexagrams and question into {AnswerLanguage}.\n2. Provide an interpretation of the main hexagram and how the changing lines influence its meaning.\n3. Explain how the changing hexagram provides additional insight or guidance.");
            OutputFormatHeader = Preferences.Default.Get(nameof(OutputFormatHeader), defaults?.OutputFormatHeader ?? "# Output Format");
            NotesHeader = Preferences.Default.Get(nameof(NotesHeader), defaults?.NotesHeader ??
                "# Notes\n\n" +
                "- Pay attention to the meanings of both hexagrams and how the changing lines transition the reading from the main to the changing hexagram.\n\n" +
                "- Ensure the interpretation reflects the philosophical concepts of the I Ching in the context of the question asked.");
            KeyThree = new NestedSettings
            {
                Message = Preferences.Default.Get(nameof(KeyThree.Message), defaults?.KeyThree?.Message ?? "Default Message")
            };
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
            Preferences.Default.Set(nameof(Settings.KeyThree.Message), KeyThree.Message);
        }
        #endregion
    }

    public class NestedSettings
    {
        public string? Message { get; set; }
    }
}
