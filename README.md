# Yi Ching for AI

[![Android](https://img.shields.io/badge/Platform-Android-3ddc84?logo=android)](https://developer.android.com)
[![.NET](https://img.shields.io/badge/.NET-9.0.100-512bd4?logo=dotnet)](https://dotnet.microsoft.com)

A modern implementation of the I Ching (Book of Changes) with a focus on AI integration, built using .NET MAUI for cross-platform deployment.

## Features

### Core Functionality

-   **🎴 Divination Methods:**
    -   Three Coin Method
    -   Traditional Yarrow Stalks Method
-   **🌐 Multilingual Support:**
    -   English and Hungarian hexagram names.
    -   Configurable output language for AI responses.
-   **📱 Cross-Platform UI:**
    -   MAUI-based Android interface.
    -   Responsive, mobile-first design.
-   **💾 Data Management:**
    -   JSON serialization for storing questions and answers.
    -   Automatic data cleanup after 2 months.

### Advanced Features

-   **🔐 Secure Publishing:**
    -   Automated build pipeline with keystore-based APK signing.
-   **⚙️ Configuration System:**
    -   Customizable prompt templates for AI interactions.
    -   Persistent user settings for a personalized experience.
    -   **URL Management:** Save and manage frequently used URLs for AI consultations.
-   **🔄 State Management:**
    -   Continuous auto-save of application state.
    -   Session restoration for seamless continuity.
    -   History tracking of user interactions.
-   **🌐 URL Management:**
    -   Save and manage frequently used URLs
    -   Open URLs directly from the app
    -   Default URLs include popular AI chat services (DeepSeek Chat, Claude, ChatGPT)
    -   Add/remove URLs through configuration interface
    -   Persistent storage of URL preferences
    -   Pre-populated with default AI chat URLs for convenience.
    -   Easy URL management directly within the app's configuration settings.

### Prerequisites

-   [.NET SDK 9.0.100](https://dotnet.microsoft.com/download)
-   [Visual Studio 2022](https://visualstudio.microsoft.com) with:
    -   .NET MAUI workload
    -   Android SDK 34
    -   .NET desktop development workload
-   Java JDK 11+

### Installation

```bash
git clone https://github.com/yourusername/YiChing.git
cd YiChing
dotnet workload restore
```

## TODO
- Configurable prompt to AI
- Store questions in GDrive or OneDrive
  - How to config on Win/Amdroid?
- access to AI APIs, getting direct answers

## Data structure
![Data structure](
https://www.plantuml.com/plantuml/png/JOun2y8m48Nt-nMt5OGuEZX8nmuT5DJzQ8yOI2wGNAGY_dUDsDhr--xTUsrMIbg2X-ReIVGI_7Q80M3mb3DsF95D59w0w4JnIhuml6RhiiRqg39hScBnL3YhYxASd7dIbU-OHauV2z3qJXYDYKi9xl56TyOT7g3cS6FMJlxOO4zY2rc6-cMcyLi7lrcLB7c0bcKimRy1
)

# Plans
- Accessing https://www.jamesdekorne.com/GBCh/hex{number}.htm for hexagram description
- ? Randomized throws
- User reaction based coin throws
  - gif
  - based on user response or position
- Read char sequence from question as coin throw results. Like oo1, hht, ffi, 001, etc

### Use the following URL format:
https://www.perplexity.ai/?s=O&q=your_question_here
Replace "your_question_here" with your actual question. This will open Perplexity.ai and perform a search for your question

### You can also add Perplexity.ai as a custom search engine in your browser with the following URL:
https://www.perplexity.ai/search?q=%s
Then when you type your question in the address bar and hit enter, it will open Perplexity.ai and search for your question

### Some additional tips:
You can add more parameters to the URL like &focus=[internet,scholar,writing,wolfram,youtube,reddit] to specify the search focus
Setting copilot=true will enable pro mode for the search
The OpenAI API can also be used to access models programmatically
