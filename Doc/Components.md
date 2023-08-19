# 1. Data persistence
## 1.1. Database
SQLite
## 1.2. ORM
https://csharpforums.net/threads/best-orm-for-working-with-sqlite.5493/

# Data structure

```

@startuml
class Hexagram{
  int Value
}
class History{
  string Question
}

Line --o Hexagram : have 6 >
Text *- Hexagram : n >
Text *-- Language
History *-- Hexagram

@enduml

//www.plantuml.com/plantuml/png/JOun2m8n34Rt_8fl5hewECYD3hS8uXuqr48lXIQL4_6_Mq6zMzzx9DzWpjNRbEcIsGm7cJbMdfu497MSEJUXryycyr8V7sbUaqOScvYdeZsXCQaWXF8_WXrkV1TiiQUJp8vDM3jTSC386Xj7eU-F3fUOQ12zzfLl
```
