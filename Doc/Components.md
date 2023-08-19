# 1. Data persistence
## 1.1. Database
SQLite
## 1.2. ORM
https://csharpforums.net/threads/best-orm-for-working-with-sqlite.5493/

# Data structure

```
@startuml
'PlantUml.com

class Hexagram{
  int Value
}
class History{
  string Question
}
class Text{
  string Header
}


'Line --o Hexagram : have 6 >
Text *- Hexagram : n >
Text *-- Language
LineText *--o Text
History *-- Hexagram

@enduml
```
![Data structure](
//www.plantuml.com/plantuml/png/JOun2y8m48Nt-nMt5OGuEZX8nmuT5DJzQ8yOI2wGNAGY_dUDsDhr--xTUsrMIbg2X-ReIVGI_7Q80M3mb3DsF95D59w0w4JnIhuml6RhiiRqg39hScBnL3YhYxASd7dIbU-OHauV2z3qJXYDYKi9xl56TyOT7g3cS6FMJlxOO4zY2rc6-cMcyLi7lrcLB7c0bcKimRy1
)
