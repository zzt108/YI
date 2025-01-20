# Changelog

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