# Changelog

 ## [0.25.308.1430] - 2025-03-08
### Changed
**CvHexagram UI Layout Refactor:** Redesigned the `CvHexagram.xaml` layout using Grid for better responsiveness and organization. Adjusted sizes and alignments for Picker, Editor, Labels, and CheckBoxes. Enhanced button arrangement by converting StackLayout to Grid and aligning buttons within grid cells.
**Enhanced Text Copy Functionality:** Added "Copy All," "Copy Answer," and "Copy System" buttons to the UI in `CvHexagram.xaml`. Updated button titles for better clarity and implemented methods to retrieve specific texts for copying, improving user experience in sharing divination results.

### Added
**OpenAI Platform Link:** Added a direct link to the OpenAI platform within the default AI service URLs, providing users with more options for AI consultation.

### Fixed
**URL Handling Logic:** Refined URL handling in the configuration settings to automatically prepend 'https://' to URLs if missing, ensuring correct URL format. Implemented user feedback for invalid URL formats by displaying an alert, improving the robustness of URL inputs.
## [0.25.202.1430] - 2025-02-02

### Added

-   **URL Management in Configuration:** Introduced a section in the configuration panel (`CvConfig.xaml`, `CvConfig.xaml.cs`) to manage saved AI URLs, allowing users to add, remove, open, and select URLs directly within the app.
-   **Default URLs:** Added default URLs for popular AI chat services to the configuration, providing users with immediate options for AI consultation (`Settings.cs`, `DefaultTexts.cs`).

### Changed

-   **UI Layout Improvements:** Refined the layout of `CvHexagram.xaml` to address word wrap issues in the question editor and improve overall responsiveness.
-   **Configuration Options Update:** Updated the configuration options in `CvConfig.xaml` and `Settings.cs` to reflect the removal of redundant settings properties (`keyTwo`, `interpretationRequest`, `KeyThree`) and to incorporate new settings for text prompts.
-   **Prompt Configuration:** Enhanced the prompt configuration system to allow for more customizable and structured prompts to AI, including configurable sections for steps, output format, and notes (`Settings.cs`, `CvConfig.xaml`, `cvHexagram.xaml.cs`).
-   **README.md Update:** Significantly updated the `README.md` file to include a comprehensive overview of features, prerequisites, installation instructions, TODO list, data structure diagram, dotnet EF commands, and plans.

### Fixed

-   **Word Wrap in Question Editor (Attempt):** Addressed the word wrap issue in the question editor of `CvHexagram.xaml` through layout adjustments (further investigation may be needed).

### Removed

-   **Redundant Settings Properties:** Removed `keyTwo`, `interpretationRequest`, and `KeyThree` properties from `Settings.cs` and related configuration UI elements as per user request, streamlining the settings interface.

## [0.25.120.1616] - 2025-01-20

### Added

-   **Multilingual Support:** Hexagram names now support English and Hungarian translations (`HexagramNames.cs`).
-   **Yarrow Stalks Divination:** Implemented the traditional Yarrow Stalks method for divination (`YarrowStalksHelper.cs`).
-   **Configuration System:** Introduced a user-configurable system for prompt templates, allowing customization of AI interactions (`Settings.cs`, `CvConfig.xaml`).
-   **JSON Data Persistence:** Implemented JSON-based storage for preserving question/answer history, enhancing user experience and data management (`JsonHandler.cs`).
-   **Automated APK Publishing:** Added a script template for automating the APK publishing process (`publish.cmd`).

### Changed

-   **Framework Upgrade:** Updated the project to use .NET 9.0.100 SDK, ensuring compatibility with the latest development tools (`global.json`).
-   **MAUI Dependency Update:** Updated MAUI dependencies to version 8.0.100, resolving compatibility issues and improving stability (`YiChing.csproj`).
-   **Hexagram Logic Improvement:** Enhanced the logic for calculating hexagram trigrams, leading to more accurate divination results (`Hexagram.cs`, `TrigramSet.cs`).
-   **Configuration Refactor:** Refactored the configuration persistence system for improved maintainability and extensibility (`Settings.cs`).

### Fixed

-   **Android Keystore Path:** Resolved issues with Android keystore path resolution in the publishing script, ensuring a smoother release process.
-   **Trigram Parsing:** Fixed edge cases in trigram binary parsing, improving the reliability of hexagram generation (`TrigramSet.cs`).
-   **UI Synchronization:** Addressed UI synchronization issues in the Yarrow Stalks visualization, providing a more responsive user interface.
-   **Version Numbering:** Implemented a dynamic version numbering system based on build time, improving version tracking (`YiChing.csproj`).

## [0.24.1215] - 2024-12-15

### Initial Release

-   **Core Divination Functionality:** Introduced basic I Ching consultation features, allowing users to perform divinations.
-   **Coin Method Implementation:** Implemented the three-coin method for divination.
-   **Initial Data Storage:** Added local storage for saving question/answer history.
-   **Cross-Platform UI:** Developed a MAUI-based user interface for cross-platform compatibility.

**Deprecated:**

-   **Perplexity.ai Integration:** Removed Perplexity.ai integration due to issues with sending questions.

### Subsequent updates in 2024

-   **24.930**
    -   **Local Storage for Q/A:** Implemented local storage for questions and answers with automatic deletion after 2 months.
    -   **Dropdown for Q/A Selection:** Added a dropdown menu for users to select and review previous questions and answers.
-   **24.526**
    -   **Icon and Splash Screen:** Fixed icon and splash screen issues.
-   **24.525**
    -   **AI Translation:** Enabled AI to translate answers into the user's selected language.
    -   **UI Enhancements:** Improved the user interface for entering questions and coin throw results.
    -   **Yarrow Stalks Visualization:** Added the ability for users to perform divination using the Yarrow Stalks method via the UI.
    -   **Copy Functionality:** Enabled users to copy divination results to the clipboard.
    -   **Automated Versioning:** Implemented automatic version numbering based on date and time.
    -   **MAUI/Android UI:** Developed the user interface using MAUI for Android.

---

**Full Changelog:** [View all commits](https://github.com/yourusername/YiChing/commits/main)