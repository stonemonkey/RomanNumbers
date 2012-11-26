using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RomanNumbers
{
    public static class RomanNumbersStringExtensions
    {
        private const char NullChar = '-';

        private static readonly Dictionary<char, int> _map = 
            new Dictionary<char, int>
                {
                    {'I', 1}, {'V', 5}, 
                    {'X', 10}, {'L', 50}, 
                    {'C', 100}, {'D', 500}, 
                    {'M', 1000}                                             
                };

        public static int ToArabicValue(this string roman)
        {
            if (!roman.IsValidRomanNumber())
                throw new ArgumentException();

            int arabic = 0;
            int size = roman.Length;

            for (var idx = 0; idx < size; idx++)
                arabic += GetArabicDigitValueFor(idx, roman);
            
            return arabic;
        }

        #region Private methods

        // [25.11.2012 cosmo] TODO: needs refactoring ... 
        private static bool IsValidRomanNumber(this string roman)
        {
            // check null, empty, white spaces 
            if (string.IsNullOrWhiteSpace(roman))
                return false;

            // check invalid symbols
            if (!roman.All(_map.Select(kvp => kvp.Key).Contains))
                return false;

            // check more than 3 consecutive occurences of same symbol for
            if (Regex.IsMatch(roman, "I{4}|X{4}|C{4}|M{4}"))
                return false;

            // check more than 1 occurence of the same symbol for
            if (Regex.IsMatch(roman, "V.*?(V)|L.*?(L)|D.*?(D)"))
                return false;

            // check invalid lower value symbols in front of greather value symbols 
            // e.g. "IL", "VX", "XD", "LC" 
            // or "IIV", "XXL", "CCM" etc
            for (var idx = 0; idx < roman.Length; idx++)
            {
                var ch = roman[idx];
                var chNext = GetNextRomanDigit(idx, roman);

                if (!IsSubstraction(ch, chNext) &&
                    IsLowerInFrontOfGreather(ch, chNext))
                    return false;
            }

            return true;
        }
        
        private static bool IsLowerInFrontOfGreather(char ch, char chNext)
        {
            return (chNext != NullChar) && 
                   (_map[ch] < _map[chNext]);
        }

        private static int GetArabicDigitValueFor(int romanDigitIdx, string roman)
        {
            var ch = roman[romanDigitIdx];
            var chNext = GetNextRomanDigit(romanDigitIdx, roman);

            return CalculateRomanDigitValue(ch, chNext);
        }

        private static int CalculateRomanDigitValue(char ch, char chNext)
        {
            if (IsSubstraction(ch, chNext))
                return -(_map[ch]);

            return _map[ch];
        }

        private static bool IsSubstraction(char ch, char chNext)
        {
            return (ch == 'I' && (chNext == 'V' || chNext == 'X')) ||
                   (ch == 'X' && (chNext == 'L' || chNext == 'C')) ||
                   (ch == 'C' && (chNext == 'D' || chNext == 'M'));
        }

        private static char GetNextRomanDigit(int romanDigitIdx, string roman)
        {
            if (romanDigitIdx < (roman.Length - 1))
                return roman[romanDigitIdx + 1];

            // returning something meaningless from the conversion point of view
            // the idea is that we reach the end of roman string, so there is no more next digit
            return NullChar;
        }

        #endregion
    }
}