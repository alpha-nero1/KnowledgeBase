/*
    16.20. T9
*/

using System;
using System.Linq;
using System.Collections.Generic;

namespace question_16._20
{
    class Program
    {
        private static string[] _digitChars = new string[] 
        {
            "", "",
            "abc",
            "def",
            "ghi",
            "jkl",
            "mno",
            "pqrs",
            "tuv",
            "wxyz"
        };

        private static List<string> _words = new List<string>
        {
            "tree",
            "used",
            "possum",
            "kangaroo",
            "the",
            "home",
            "jess",
            "oven",
            "steel",
            "beans",
            "cool"
        };

        static void Main(string[] args)
        {
            // Alterantively we could have stored digit combinations in a dictionary.
            foreach (var word in GetWords(new int[] { 8, 7, 3, 3 }))
            {
                Console.WriteLine("Option = {0}", word);
            }
        }

        // Return a list of words given a sequence of digits, like on old phones.
        public static List<string> GetWords(int[] digits)
        {
            // Get a copy of the words..
            var words = new List<string>(_words);
            
            // Loop the digits and filter words on each pass.
            for (int i = 0; i < digits.Length; i++)
            {
                int digit = digits[i];
                words = words.Where(w => _digitChars[digit].Contains(w[i])).ToList();
            }

            return words;
        }
    }
}
