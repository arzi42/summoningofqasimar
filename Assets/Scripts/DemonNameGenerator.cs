using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public static class DemonNameGenerator
    {
        private static List<List<string>> _parts = new List<List<string>>
        {
            new List<string> { "be", "tia", "mar", "a", "mam", "as" },
            new List<string> { "la", "lze", "mat", "duk", "bad", "mon", "lphe", "mo" },
            new List<string> { "kor", "bub", "don", "gor", "deus" },
        };

        public static string Generate()
        {
            int length = Random.Range(2, 4);

            string result = string.Empty;

            for (int i = 0; i < length; i++)
            {
                result += _parts[i][Random.Range(0, _parts[i].Count)];
            }

            return result;
        }
    }
}