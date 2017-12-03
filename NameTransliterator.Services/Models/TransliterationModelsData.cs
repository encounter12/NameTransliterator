using NameTransliterator.Models.DomainModels;

namespace NameTransliterator.Services.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public static class TransliterationModelsData
    {
        public static List<TransliterationModel> GetTransliterationModels()
        {
            var transliterationModels = new List<TransliterationModel>();

            Language sourceLanguage = TestData.Languages.FirstOrDefault(l => l.Name == "English");
            Language targetLanguage = TestData.Languages.FirstOrDefault(l => l.Name == "Bulgarian");

            var firstTransliterationModel = new TransliterationModel();

            if (sourceLanguage != null && targetLanguage != null)
            {
                firstTransliterationModel = new TransliterationModel()
                {
                    Id = 1,
                    SourceLanguageId = sourceLanguage.Id,
                    SourceLanguage = sourceLanguage,
                    TargetLanguageId = targetLanguage.Id,
                    TargetLanguage = targetLanguage,
                    TransliterationRules = EnglishBulgarianTransliterationSet
                };
            }

            transliterationModels.Add(firstTransliterationModel);

            sourceLanguage = TestData.Languages.FirstOrDefault(l => l.Name == "Bulgarian");
            targetLanguage = TestData.Languages.FirstOrDefault(l => l.Name == "English");

            var secondTransliterationModel = new TransliterationModel();

            if (sourceLanguage != null && targetLanguage != null)
            {
                secondTransliterationModel = new TransliterationModel()
                {
                    Id = 2,
                    SourceLanguageId = sourceLanguage.Id,
                    SourceLanguage = sourceLanguage,
                    TargetLanguageId = targetLanguage.Id,
                    TargetLanguage = targetLanguage,
                    TransliterationRules = BulgarianEnglishTransliterationSet
                };
            }

            transliterationModels.Add(secondTransliterationModel);

            return transliterationModels;
        }

        public static IList<TransliterationRule> EnglishBulgarianTransliterationSet = new List<TransliterationRule>()
        {
            new TransliterationRule(@"[^A-Za-z01456@ -]+", "", 1),
            new TransliterationRule(@"\s?-\s?", "-", 2),
            new TransliterationRule(@"([^EeIiNn])\1+", "$1", 3),
            new TransliterationRule(@"([EeIiNn])\1+", "$1$1", 4),
            new TransliterationRule("0", "о", 5),
            new TransliterationRule("1", "и", 6),
            new TransliterationRule("4", "ч", 7),
            new TransliterationRule("5", "п", 8),
            new TransliterationRule("6", "ш", 9),
            new TransliterationRule("@", "а", 10),
            new TransliterationRule(@"(\w+)(tia)(\b)", "$1тя$3", 11),
            new TransliterationRule(@"(\w+)(lia)(\b)", "$1ля$3", 12),
            new TransliterationRule(@"\bia", "я", 13),
            new TransliterationRule(@"(\w+)(ia)(\b)", "$1ия$3", 14),
            new TransliterationRule(@"(.*)(ia)([- ])", "$1ия$3", 15),
            new TransliterationRule(@"\byo", "йо", 16),
            new TransliterationRule(@"\bio", "йо", 17),
            new TransliterationRule(@"\bjo", "йо", 18),
            new TransliterationRule(@"(\w+)(yo)(\b)", "$1ьо$3", 19),
            new TransliterationRule(@"(\w+)(io)(\b)", "$1ьо$3", 20),
            new TransliterationRule("latch", "лъч", 21),
            new TransliterationRule("lutch", "лъч", 22),
            new TransliterationRule("mityr", "митър", 23),
            new TransliterationRule("lach", "лъч", 24),
            new TransliterationRule("luch", "лъч", 25),
            new TransliterationRule("itar", "итър", 26),
            new TransliterationRule("itur", "итър", 27),
            new TransliterationRule("etar", "етър", 28),
            new TransliterationRule("etur", "етър", 29),
            new TransliterationRule("etyr", "етър", 30),
            new TransliterationRule("nia", "ня", 31),
            new TransliterationRule("kan", "кън", 32),
            new TransliterationRule("dak", "дък", 33),
            new TransliterationRule("jdi", "жди", 34),
            new TransliterationRule("iya", "ия", 35),
            new TransliterationRule("rof", "ров", 36),
            new TransliterationRule("nof", "нов", 37),
            new TransliterationRule("oya", "оя", 38),
            new TransliterationRule("oia", "оя", 39),
            new TransliterationRule("sht", "щ", 40),
            new TransliterationRule("sch", "ш", 41),
            new TransliterationRule("tch", "ч", 42),
            new TransliterationRule("dj", "дж", 43),
            new TransliterationRule("ts", "ц", 44),
            new TransliterationRule("ch", "ч", 45),
            new TransliterationRule("sh", "ш", 46),
            new TransliterationRule("yu", "ю", 47),
            new TransliterationRule("ya", "я", 48),
            new TransliterationRule("ou", "у", 49),
            new TransliterationRule("oy", "ой", 50),
            new TransliterationRule("oi", "ой", 51),
            new TransliterationRule("ay", "ай", 52),
            new TransliterationRule("ai", "ай", 53),
            new TransliterationRule("ey", "ей", 54),
            new TransliterationRule("ei", "ей", 55),
            new TransliterationRule("iy", "ий", 56),
            new TransliterationRule("ii", "ий", 57),
            new TransliterationRule("zh", "ж", 58),
            new TransliterationRule("jd", "жд", 59),
            new TransliterationRule("a", "а", 60),
            new TransliterationRule("b", "б", 61),
            new TransliterationRule("v", "в", 62),
            new TransliterationRule("w", "в", 63),
            new TransliterationRule("g", "г", 64),
            new TransliterationRule("d", "д", 65),
            new TransliterationRule("e", "е", 66),
            new TransliterationRule("j", "дж", 67),
            new TransliterationRule("z", "з", 68),
            new TransliterationRule("i", "и", 69),
            new TransliterationRule("k", "к", 70),
            new TransliterationRule("c", "к", 71),
            new TransliterationRule("l", "л", 72),
            new TransliterationRule("m", "м", 73),
            new TransliterationRule("n", "н", 74),
            new TransliterationRule("o", "о", 75),
            new TransliterationRule("p", "п", 76),
            new TransliterationRule("r", "р", 77),
            new TransliterationRule("s", "с", 78),
            new TransliterationRule("t", "т", 79),
            new TransliterationRule("u", "у", 80),
            new TransliterationRule("f", "ф", 81),
            new TransliterationRule("h", "х", 82),
            new TransliterationRule("y", "и", 83)
        };

        public static IList<TransliterationRule> BulgarianEnglishTransliterationSet = new List<TransliterationRule>()
        {
            new TransliterationRule(@"[^А-Яа-я1456 -]+", "", 1),
            new TransliterationRule("1", "i", 2),
            new TransliterationRule("4", "ch", 3),
            new TransliterationRule("5", "p", 4),
            new TransliterationRule("6", "sh", 5),
            new TransliterationRule(@"\b*ия\b", "ia", 6),
            new TransliterationRule("а", "a", 7),
            new TransliterationRule("б", "b", 8),
            new TransliterationRule("в", "v", 9),
            new TransliterationRule("г", "g", 10),
            new TransliterationRule("д", "d", 11),
            new TransliterationRule("е", "e", 12),
            new TransliterationRule("ж", "zh", 13),
            new TransliterationRule("з", "z", 14),
            new TransliterationRule("и", "i", 15),
            new TransliterationRule("й", "y", 16),
            new TransliterationRule("к", "k", 17),
            new TransliterationRule("л", "l", 18),
            new TransliterationRule("м", "m", 19),
            new TransliterationRule("н", "n", 20),
            new TransliterationRule("о", "o", 21),
            new TransliterationRule("п", "p", 22),
            new TransliterationRule("р", "r", 23),
            new TransliterationRule("с", "s", 24),
            new TransliterationRule("т", "t", 25),
            new TransliterationRule("у", "u", 26),
            new TransliterationRule("ф", "f", 27),
            new TransliterationRule("х", "h", 28),
            new TransliterationRule("ц", "ts", 29),
            new TransliterationRule("ч", "ch", 30),
            new TransliterationRule("ш", "sh", 31),
            new TransliterationRule("щ", "sht", 32),
            new TransliterationRule("ъ", "a", 33),
            new TransliterationRule("ь", "y", 34),
            new TransliterationRule("ю", "yu", 35),
            new TransliterationRule("я", "ya", 36)
        };
    }
}
