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
        string step1;
        string step2;
        string step3;
        string note1;
        string note2;

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
            stepsHeader = "# Steps";
            outputFormatHeader = "# Output Format";
            notesHeader = "# Notes";
            step1 = "Translate the hexagrams and question into {AnswerLanguage}.";
            step2 = "Provide an interpretation of the main hexagram and how the changing lines influence its meaning.";
            step3 = "Explain how the changing hexagram provides additional insight or guidance.";
            note1 = "- Pay attention to the meanings of both hexagrams and how the changing lines transition the reading from the main to the changing hexagram.";
            note2 = "- Ensure the interpretation reflects the philosophical concepts of the I Ching in the context of the question asked.";
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
        public string Step1 { get => step1; set => step1 = value; }
        public string Step2 { get => step2; set => step2 = value; }
        public string Step3 { get => step3; set => step3 = value; }
        public string Note1 { get => note1; set => note1 = value; }
        public string Note2 { get => note2; set => note2 = value; }
        public NestedSettings KeyThree { get; set; }

        #region Methods

        public void LoadValues(Settings? defaults)
        {
            AnswerLanguage = Preferences.Default.Get(nameof(AnswerLanguage), defaults?.AnswerLanguage ?? "English");
            QuestionPrefix = Preferences.Default.Get(nameof(QuestionPrefix), defaults?.QuestionPrefix ?? "Question to I Ching:");
            AnswerPrefix = Preferences.Default.Get(nameof(AnswerPrefix), defaults?.AnswerPrefix ?? "I Ching answered:");
            TranslationRequest = Preferences.Default.Get(nameof(TranslationRequest), defaults?.TranslationRequest ?? "Please translate to");
            StepsHeader = Preferences.Default.Get(nameof(StepsHeader), defaults?.StepsHeader ?? "# Steps");
            OutputFormatHeader = Preferences.Default.Get(nameof(OutputFormatHeader), defaults?.OutputFormatHeader ?? "# Output Format");
            NotesHeader = Preferences.Default.Get(nameof(NotesHeader), defaults?.NotesHeader ?? "# Notes");
            Step1 = Preferences.Default.Get(nameof(Step1), defaults?.Step1 ?? "Translate the hexagrams and question into {AnswerLanguage}.");
            Step2 = Preferences.Default.Get(nameof(Step2), defaults?.Step2 ?? "Provide an interpretation of the main hexagram and how the changing lines influence its meaning.");
            Step3 = Preferences.Default.Get(nameof(Step3), defaults?.Step3 ?? "Explain how the changing hexagram provides additional insight or guidance.");
            Note1 = Preferences.Default.Get(nameof(Note1), defaults?.Note1 ?? "- Pay attention to the meanings of both hexagrams and how the changing lines transition the reading from the main to the changing hexagram.");
            Note2 = Preferences.Default.Get(nameof(Note2), defaults?.Note2 ?? "- Ensure the interpretation reflects the philosophical concepts of the I Ching in the context of the question asked.");
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
            Preferences.Default.Set(nameof(Settings.Step1), step1);
            Preferences.Default.Set(nameof(Settings.Step2), step2);
            Preferences.Default.Set(nameof(Settings.Step3), step3);
            Preferences.Default.Set(nameof(Settings.Note1), note1);
            Preferences.Default.Set(nameof(Settings.Note2), note2);
            Preferences.Default.Set(nameof(Settings.KeyThree.Message), KeyThree.Message);
        }
        #endregion
    }

    public class NestedSettings
    {
        public string? Message { get; set; }
    }
}
