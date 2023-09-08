# YI
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