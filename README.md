# YI
## Features
### 24.930
- store questions and answers in local storage
- can select Q/A in a dropdown
- Q/A deleted after 2 months
- Perplexity.ai button removed as sending questions to p.ai was not working anymore
- 
### 24.526
  - Icon and splash screen fixed
### 24.525
  - Ask AI to translate answer to selected language (in configuration)
  - User can enter a question and a result of coin throws via the UI
  - User can use Yarrow Stalks method to get a divination
  - User can send result of divination to perplexity.ai side as a question
  - User can copy result of divination to clipboard to send other ui sides or save it
  - Auto Version numbers based on date/time
  - UI in MAUI/Android

# Plans
- Signed APK to ease distribution
- Stacked navigation
- Continuous saving of data, restore last state when started
  - persist hexagrams to JSON
  - persist jicsing 2 JSON
- categorized questions
- ? Randomized throws
- User reaction based coin throws
  - gif
  - based on user response or position

- Read char sequence from question as coin throw results. Like oo1, hht, ffi, 001, etc

## TODO
- Immediately update questions list after copy click
- Configurable prompt to AI
- Hexagram names in answers, besides numbers
- Store questions in GDrive or OneDrive
  - How to config on Win/Amdroid?
- access to AI APIs, getting direct answers
- 
## Data structure
![Data structure](
https://www.plantuml.com/plantuml/png/JOun2y8m48Nt-nMt5OGuEZX8nmuT5DJzQ8yOI2wGNAGY_dUDsDhr--xTUsrMIbg2X-ReIVGI_7Q80M3mb3DsF95D59w0w4JnIhuml6RhiiRqg39hScBnL3YhYxASd7dIbU-OHauV2z3qJXYDYKi9xl56TyOT7g3cS6FMJlxOO4zY2rc6-cMcyLi7lrcLB7c0bcKimRy1
)
## Dotnet EF

- Open the package manager prompt
- To install the dotnet-ef tool, run the following command:
- For .NET 7
  - PM> dotnet tool install --global dotnet-ef
- add Migration

PM> dotnet ef migrations add Initial --project DataLayer --context YiDbContext
- undo Migration
To undo this action, use 'ef migrations remove' (no 'Initial')

PM> dotnet ef migrations remove --project DataLayer --context yidbcontext

Your startup project 'DataLayer' doesn't reference Microsoft.EntityFrameworkCore.Design. 
--> Do not just add nuget package but add using Microsoft.EntityFrameworkCore.Design; 

# Plans
- Accessing https://www.jamesdekorne.com/GBCh/hex{number}.htm for hexagram description
- Signed APK to ease distribution
- Continuous saving of data, restore last state when started
  - persist hexagrams to JSON
  - persist jicsing 2 JSON
- categorized questions
- ? Randomized throws
- User reaction based coin throws
  - gif
  - based on user response or position
- Do UI in MAUI/Android
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
The OpenAI API can also be used to access Perplexity models programmatically

