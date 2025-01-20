# Changelog

## [0.25.120.1616] - 2025-01-20

### Added
- Multilingual support for hexagram names (English/Hungarian) in `HexagramNames.cs`
- Yarrow Stalks divination method implementation (`YarrowStalksHelper.cs`)
- Configuration system for prompt templates (`Settings.cs`, `CvConfig.xaml`)
- JSON persistence for question/answer history (`JsonHandler.cs`)
- Automated APK publishing script (`publish.cmd` template)

### Changed
- Upgraded to .NET 9.0.100 SDK (`global.json`)
- MAUI dependencies updated to 8.0.100 (`YiChing.csproj`)
- Improved hexagram trigram calculation logic (`Hexagram.cs`, `TrigramSet.cs`)
- Refactored configuration persistence system (`Settings.cs`)

### Fixed
- Android keystore path resolution in publishing script
- Trigram binary parsing edge cases (`TrigramSet.cs`)
- UI synchronization issues in Yarrow Stalks visualization
- Version numbering system based on build time (`YiChing.csproj`)

## [0.24.1215] - 2024-12-15

### Initial Release
- Basic I Ching consultation functionality
- Coin method divination implementation
- Perplexity.ai integration (later removed)
- Local storage for Q/A history
- MAUI-based cross-platform UI

## 24.930
- store questions and answers in local storage
- can select Q/A in a dropdown
- Q/A deleted after 2 months
- Perplexity.ai button removed as sending questions to p.ai was not working anymore
- 
## 24.526
  - Icon and splash screen fixed
## 24.525
  - Ask AI to translate answer to selected language (in configuration)
  - User can enter a question and a result of coin throws via the UI
  - User can use Yarrow Stalks method to get a divination
  - User can send result of divination to perplexity.ai side as a question
  - User can copy result of divination to clipboard to send other ui sides or save it
  - Auto Version numbers based on date/time
  - UI in MAUI/Android


---

**Full Changelog**: [View all commits](https://github.com/yourusername/YiChing/commits/main)