namespace HexagramNS;

public static class HexagramNameProvider
{
    public enum Language
    {
        English,
        Hungarian
    }

    public static string GetHexagramName(int hexagramNumber, Language language)
    {
        if (hexagramNumber < 1 || hexagramNumber > 64)
        {
            return "Invalid Hexagram Number";
        }

        switch (language)
        {
            case Language.English:
                return GetEnglishHexagramName(hexagramNumber);
            case Language.Hungarian:
                return GetHungarianHexagramName(hexagramNumber);
            default:
                return "Invalid Language";
        }
    }

    private static string GetEnglishHexagramName(int hexagramNumber)
    {
        switch (hexagramNumber)
        {
            case 1: return "The Creative";
            case 2: return "The Receptive";
            case 3: return "Difficulty at the Beginning";
            case 4: return "Youthful Folly";
            case 5: return "Waiting";
            case 6: return "Conflict";
            case 7: return "The Army";
            case 8: return "Holding Together";
            case 9: return "Small Taming";
            case 10: return "Treading";
            case 11: return "Peace";
            case 12: return "Stagnation";
            case 13: return "Fellowship with Men";
            case 14: return "Great Possession";
            case 15: return "Humility";
            case 16: return "Enthusiasm";
            case 17: return "Following";
            case 18: return "Decay";
            case 19: return "Approach";
            case 20: return "Contemplation";
            case 21: return "Biting Through";
            case 22: return "Adornment";
            case 23: return "Splitting Apart";
            case 24: return "Return";
            case 25: return "Innocence";
            case 26: return "Great Taming";
            case 27: return "Nourishment";
            case 28: return "Great Preponderance";
            case 29: return "The Abyss";
            case 30: return "Clinging";
            case 31: return "Influence";
            case 32: return "Duration";
            case 33: return "Retreat";
            case 34: return "Great Power";
            case 35: return "Progress";
            case 36: return "Darkening of the Light";
            case 37: return "Family";
            case 38: return "Opposition";
            case 39: return "Obstruction";
            case 40: return "Deliverance";
            case 41: return "Decrease";
            case 42: return "Increase";
            case 43: return "Breakthrough";
            case 44: return "Encountering";
            case 45: return "Gathering Together";
            case 46: return "Ascending";
            case 47: return "Oppression";
            case 48: return "The Well";
            case 49: return "Revolution";
            case 50: return "The Cauldron";
            case 51: return "Arousing";
            case 52: return "Keeping Still";
            case 53: return "Development";
            case 54: return "Marrying Maiden";
            case 55: return "Abundance";
            case 56: return "The Wanderer";
            case 57: return "The Gentle";
            case 58: return "The Joyous";
            case 59: return "Dispersion";
            case 60: return "Limitation";
            case 61: return "Inner Truth";
            case 62: return "Small Preponderance";
            case 63: return "After Completion";
            case 64: return "Before Completion";
            default: return "Invalid Hexagram Number";
        }
    }

    private static string GetHungarianHexagramName(int hexagramNumber)
    {
        switch (hexagramNumber)
        {
            case 1: return "Alkotó";
            case 2: return "Befogadó";
            case 3: return "Nehézségek a kezdetben";
            case 4: return "Ifjúi balgaság";
            case 5: return "Várakozás";
            case 6: return "Viszály";
            case 7: return "A hadsereg";
            case 8: return "Együtt tartás";
            case 9: return "Kis megszelídítés";
            case 10: return "Taposás";
            case 11: return "Béke";
            case 12: return "Megrekedtség";
            case 13: return "Közösség az emberekkel";
            case 14: return "Nagy birtoklás";
            case 15: return "Alázat";
            case 16: return "Elragadtatás";
            case 17: return "Követés";
            case 18: return "Romlás";
            case 19: return "Közeledés";
            case 20: return "Szemlélődés";
            case 21: return "Átharapás";
            case 22: return "Díszítés";
            case 23: return "Szétválás";
            case 24: return "Visszatérés";
            case 25: return "Ártatlanság";
            case 26: return "Nagy megszelídítés";
            case 27: return "Táplálás";
            case 28: return "Nagy túlsúly";
            case 29: return "A mélység";
            case 30: return "Rátapadás";
            case 31: return "Hatás";
            case 32: return "Tartósság";
            case 33: return "Visszavonulás";
            case 34: return "Nagy hatalom";
            case 35: return "Előrehaladás";
            case 36: return "A fény elsötétítése";
            case 37: return "A család";
            case 38: return "Ellentét";
            case 39: return "Akadályoztatás";
            case 40: return "Megszabadulás";
            case 41: return "Csökkenés";
            case 42: return "Növekedés";
            case 43: return "Kitörés";
            case 44: return "Találkozás";
            case 45: return "Összegyűlés";
            case 46: return "Felszállás";
            case 47: return "Elnyomás";
            case 48: return "A kút";
            case 49: return "Forradalom";
            case 50: return "A nagy üst";
            case 51: return "Felkeltés";
            case 52: return "Nyugalom megőrzése";
            case 53: return "Fejlődés";
            case 54: return "Házasodó leány";
            case 55: return "Bőség";
            case 56: return "A vándor";
            case 57: return "A szelíd";
            case 58: return "Az örömteli";
            case 59: return "Szétoszlás";
            case 60: return "Korlátozás";
            case 61: return "Belső igazság";
            case 62: return "Kis túlsúly";
            case 63: return "A befejezés után";
            case 64: return "A befejezés előtt";
            default: return "Invalid Hexagram Number";
        }
    }
}


public enum HexagramEnum
{
    // 1. Qian (The Creative)
    TheCreative = 1,
    // 2. Kun (The Receptive)
    TheReceptive = 2,
    // 3. Zhun (Difficulty at the Beginning)
    DifficultyAtTheBeginning = 3,
    // 4. Meng (Youthful Folly)
    YouthfulFolly = 4,
    // 5. Xu (Waiting)
    Waiting = 5,
    // 6. Song (Conflict)
    Conflict = 6,
    // 7. Shi (The Army)
    TheArmy = 7,
    // 8. Bi (Holding Together)
    HoldingTogether = 8,
    // 9. Xiao Xu (Small Taming)
    SmallTaming = 9,
    // 10. Lü (Treading)
    Treading = 10,
    // 11. Tai (Peace)
    Peace = 11,
    // 12. Pi (Stagnation)
    Stagnation = 12,
    // 13. Tong Ren (Fellowship with Men)
    FellowshipWithMen = 13,
    // 14. Da You (Great Possession)
    GreatPossession = 14,
    // 15. Qian (Humility)
    Humility = 15,
    // 16. Yu (Enthusiasm)
    Enthusiasm = 16,
    // 17. Sui (Following)
    Following = 17,
    // 18. Gu (Decay)
    Decay = 18,
    // 19. Lin (Approach)
    Approach = 19,
    // 20. Guan (Contemplation)
    Contemplation = 20,
    // 21. Shi He (Biting Through)
    BitingThrough = 21,
    // 22. Bi (Adornment)
    Adornment = 22,
    // 23. Bo (Splitting Apart)
    SplittingApart = 23,
    // 24. Fu (Return)
    Return = 24,
    // 25. Wu Wang (Innocence)
    Innocence = 25,
    // 26. Da Xu (Great Taming)
    GreatTaming = 26,
    // 27. Yi (Nourishment)
    Nourishment = 27,
    // 28. Da Guo (Great Preponderance)
    GreatPreponderance = 28,
    // 29. Kan (The Abyss)
    TheAbyss = 29,
    // 30. Li (Clinging)
    Clinging = 30,
    // 31. Xian (Influence)
    Influence = 31,
    // 32. Heng (Duration)
    Duration = 32,
    // 33. Dun (Retreat)
    Retreat = 33,
    // 34. Da Zhuang (Great Power)
    GreatPower = 34,
    // 35. Jin (Progress)
    Progress = 35,
    // 36. Ming Yi (Darkening of the Light)
    DarkeningOfTheLight = 36,
    // 37. Jia Ren (Family)
    Family = 37,
    // 38. Kui (Opposition)
    Opposition = 38,
    // 39. Jian (Obstruction)
    Obstruction = 39,
    // 40. Jie (Deliverance)
    Deliverance = 40,
    // 41. Sun (Decrease)
    Decrease = 41,
    // 42. Yi (Increase)
    Increase = 42,
    // 43. Guai (Breakthrough)
    Breakthrough = 43,
    // 44. Gou (Encountering)
    Encountering = 44,
    // 45. Cui (Gathering Together)
    GatheringTogether = 45,
    // 46. Sheng (Ascending)
    Ascending = 46,
    // 47. Kun (Oppression)
    Oppression = 47,
    // 48. Jing (The Well)
    TheWell = 48,
    // 49. Ge (Revolution)
    Revolution = 49,
    // 50. Ding (The Cauldron)
    TheCauldron = 50,
    // 51. Zhen (Arousing)
    Arousing = 51,
    // 52. Gen (Keeping Still)
    KeepingStill = 52,
    // 53. Jian (Development)
    Development = 53,
    // 54. Gui Mei (Marrying Maiden)
    MarryingMaiden = 54,
    // 55. Feng (Abundance)
    Abundance = 55,
    // 56. Lü (The Wanderer)
    TheWanderer = 56,
    // 57. Xun (The Gentle)
    TheGentle = 57,
    // 58. Dui (The Joyous)
    TheJoyous = 58,
    // 59. Huan (Dispersion)
    Dispersion = 59,
    // 60. Jie (Limitation)
    Limitation = 60,
    // 61. Zhong Fu (Inner Truth)
    InnerTruth = 61,
    // 62. Xiao Guo (Small Preponderance)
    SmallPreponderance = 62,
    // 63. Ji Ji (After Completion)
    AfterCompletion = 63,
    // 64. Wei Ji (Before Completion)
    BeforeCompletion = 64
}
