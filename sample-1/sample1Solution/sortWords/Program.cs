using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace sortWords
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter sentence whose words are to be sorted by length...");

            string str = Console.ReadLine();

            char[] separators = new char[6] { ' ', ',', '.', '?', ':', ';' };

            while (!String.IsNullOrEmpty(str))
            {
                // Correctly parse sentences with space, commas, period, question mark, or colon
                List<string> wordList = ParseSentenceIntoWords(str, separators);

                IEnumerable<string> result = SortByLength(wordList);

                foreach (var wrd in result)
                {
                    Console.Write(wrd);
                    Console.Write(' ');
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Enter next sentence to be sorted by length...");
                str = Console.ReadLine();
            }
        }

        static List<string> ParseSentenceIntoWords(string sentence, char[] separators)
        {
            List<string> wordList = sentence.Split(separators).ToList();

            // this is to enable correct parsing and sorting of words in sentence even if 
            // words are separated by commas, period, question mark, etc

            for (int i = 0; i < wordList.Count(); i++)
            {
                foreach (var sep in separators)
                {
                    wordList[i] = wordList[i].TrimStart(sep);
                    wordList[i] = wordList[i].TrimEnd(sep);
                }
            }

            List<string> wordListTmp = wordList;
            for (int i = 0; i < wordList.Count(); i++)
            {
                if (wordList[i] == "")
                {
                    wordListTmp.Remove(wordListTmp[i]);
                }
            }

            wordList = wordListTmp;
            return wordList;
        }

        static IEnumerable<string> SortByLength(IEnumerable<string> e)
        {
            // Use LINQ to sort the array received and return a copy.
            var sorted = from s in e
                         orderby s.Length ascending
                         select s;
            return sorted;
        }
    }
}
