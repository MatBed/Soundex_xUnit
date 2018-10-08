﻿using System;
using System.Linq;

namespace Soundex_CSharp_xUnit
{
    public class SoundexAlgorithm
    {
        SoundexData soundexData = new SoundexData();

        public string Encode(string word)
        {
            if (String.IsNullOrEmpty(word))
                return "0000";
            if (word.Length == 1)
                return String.Format(word + "000");
            if (word.Any(x => !char.IsLetter(x)))
                return "0000";

            string lowerCaseWord = word.ToLower();
            string encodedWord = EncodeWord(lowerCaseWord);
            TrimString(ref encodedWord);

            return encodedWord;
        }

        public void TrimString(ref string word)
        {
            if (word.Length > 4)
                word = word.Remove(4);
            else if (word.Length < 4)
            {
                do
                {
                    word += "0";
                } while (word.Length < 4);
            }
        }

        public string EncodeWord(string word)
        {
            string encodedWord = "";
            encodedWord += word[0].ToString();

            for (int i = 1; i < word.Length; i++)
            {
                if (i > 1 && (word[i - 1].ToString() == "w" || word[i - 1].ToString() == "h")
                    && AreTwoCharsWithTheSameNumber(word[i - 2].ToString(), word[i].ToString()))
                    continue;

                else if (soundexData.charToNumber.ContainsKey(word[i].ToString()))
                {
                    encodedWord += soundexData.charToNumber[word[i].ToString()];

                    if (AreTwoCharsWithTheSameNumber(word[i].ToString(), word[i - 1].ToString()) && i > 1)
                        encodedWord = RemoveDuplicatedChars(encodedWord);
                }

                else if ((AreTwoCharsWithTheSameNumber(word[i - 1].ToString(), word[i + 1].ToString())
                             && (word[i].ToString() == "w"
                             || word[i].ToString() == "h"))
                         || (AreTwoCharsWithTheSameNumber(word[i].ToString(), word[i + 1].ToString()))
                         && i > word.Length - 1)
                    encodedWord = RemoveDuplicatedChars(encodedWord);
            }

            return encodedWord;
        }

        public string RemoveDuplicatedChars(string word)
        {
            string result = new string(word.Distinct().ToArray());
            return result;
        }

        public bool AreTwoCharsWithTheSameNumber(string firstChar, string secondChar)
        {
            if (soundexData.charToNumber.ContainsKey(firstChar) && soundexData.charToNumber.ContainsKey(secondChar))
            {
                string valueOfFirstChar = soundexData.charToNumber[firstChar];
                string valueOfSecondChar = soundexData.charToNumber[secondChar];

                if (valueOfFirstChar == valueOfSecondChar)
                    return true;
            }

            return false;
        }
    }
}
