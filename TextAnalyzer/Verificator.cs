using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalyzer
{
    public class Verificator
    {
        //This method helps to make valid id's array from a textbox. 
        //Main issue if there are a lot of leading zeros or commas
        //
        public string[] getValidated(string identificators)
        {
            string[] identificator = identificators.Split(',', ';');
            List<string> ids = new List<string>();
            foreach (var id in identificator)
            {
                if (id.All(char.IsDigit) == true && id != "")
                    ids.Add(id.TrimStart(new Char[] { '0' }));
            }

            string[] validId = ids.Distinct().ToArray();

            return validId;
        }
        //This method calculates amount of words in a string by splitting string by delimiters into array
        //and counting length of an array
        //
        public string wordsCount(string text)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n', '\t', '\'', '\\', '—' };

            string wordsQuantity = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length.ToString();

            return wordsQuantity;
        }

        //This method calculates amount of vowels in a string by decoding and encoding.
        // GetEncoding ISO-8859-8 is used for the majority of european languages.
        // GetEncoding UTF-8 is used for the minority of european languages.

        public string vowelsCount(string text)
        {
            string lowerText = text.ToLower();

            byte[] tempBytes;

            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(lowerText);

            string finalStr = System.Text.Encoding.UTF8.GetString(tempBytes);

            var vowels = new HashSet<char> { 'e', 'y', 'u', 'i', 'o', 'a' };

            string vowelsQuantity = finalStr.Count(c => vowels.Contains(c)).ToString();

            if (vowelsQuantity == "0")
            {
                tempBytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(lowerText);

                finalStr = System.Text.Encoding.UTF8.GetString(tempBytes);

                Console.WriteLine(finalStr);

                vowels = new HashSet<char> { 'ё', 'у', 'е', 'ы', 'а', 'о', 'э', 'я', 'и', 'ю' };

                vowelsQuantity = finalStr.Count(c => vowels.Contains(c)).ToString();
            }

            return vowelsQuantity;
        }
    }
}

