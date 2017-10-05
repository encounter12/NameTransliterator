namespace NameTransliterator.Services.Archived
{
    using System.Collections.Generic;

    public static class ArchivedTransliterationDictionaries
    {
        public static string[] BulgarianEnglishTransliterationSet = new string[]
        {
            "а - a",
            "б - b",
            "в - v",
            "г - g",
            "д - d",
            "е - e",
            "ж - zh",
            "з - z",
            "и - i",
            "й - y",
            "к - k",
            "л - l",
            "м - m",
            "н - n",
            "о - o",
            "п - p",
            "р - r",
            "с - s",
            "т - t",
            "у - u",
            "ф - f",
            "х - h",
            "ц - ts",
            "ч - ch",
            "ш - sh",
            "щ - sht",
            "ъ - a",
            "ь - y",
            "ю - yu",
            "я - ya",
            @"\b*ия\b - ia"
        };

        public static SortedDictionary<string, string> transliterationSet = new SortedDictionary<string, string>(new LengthComparer())
        {
            { "а", "a"},
            { "б", "b"},
            { "в", "v"},
            { "г", "g"},
            { "д", "d"},
            { "е", "e"},
            { "ж", "zh"},
            { "з", "z"},
            { "и", "i"},
            { "й", "y"},
            { "к", "k"},
            { "л", "l"},
            { "м", "m"},
            { "н", "n"},
            { "о", "o"},
            { "п", "p"},
            { "р", "r"},
            { "с", "s"},
            { "т", "t"},
            { "у", "u"},
            { "ф", "f"},
            { "х", "h"},
            { "ц", "ts"},
            { "ч", "ch"},
            { "ш", "sh"},
            { "щ", "sht"},
            { "ъ", "a"},
            { "ь", "y"},
            { "ю", "yu"},
            { "я", "ya"},
            { @"\b*ия\b", "ia"},
        };
    }
}
