using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isPangram
{
    class Program
    {
        private const int number_of_letters_in_alphabet = 26;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter sentence to be checked as pangram...");

            string str = Console.ReadLine();
            bool result;

            while (!String.IsNullOrEmpty(str))
            {
                result = checkIfPangram(str);

                if (result == true)
                {
                    Console.WriteLine("Yes");
                }
                else if (result == false)
                {
                    Console.WriteLine("No");
                }

                Console.WriteLine("Enter next sentence to be checked as pangram...");
                str = Console.ReadLine();
            }

        }

        public static bool checkIfPangram(string str)
        {
            string strTmp = str.ToLower();
            if (strTmp.Length < number_of_letters_in_alphabet)
            {
                return false;
            }

            for (char ch = 'a'; ch <= 'z'; ch++)
            {
                if ((strTmp.IndexOf(ch) < 0) && (strTmp.IndexOf((char)(ch + 32)) < 0))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
