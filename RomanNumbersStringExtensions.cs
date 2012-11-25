using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RomanNumbers
{
    public static class RomanNumbersStringExtensions
    {
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

        // [25.11.2012 cosmo] TODO 1: needs refactoring ... and tests
        public static bool IsValidRomanNumber(this string roman)
        {
            // check null, empty, white spaces 
            if (string.IsNullOrWhiteSpace(roman))
                return false;
            
            // check invalid symbols
            var romanSymbols = _map.Select(kvp => kvp.Key);
            if (!roman.All(romanSymbols.Contains))
                return false;

            // check more than 3 consecutive same symbol
            var fourSymbolsPattern = new StringBuilder();
            foreach (var romanSymbol in romanSymbols)
                fourSymbolsPattern.AppendFormat("{0}{{4}}|", romanSymbol);
            fourSymbolsPattern.Remove(fourSymbolsPattern.Length - 1, 1);

            return !Regex.IsMatch(roman, fourSymbolsPattern.ToString());
            //return !Regex.IsMatch(roman, "I{4}|V{4}|X{4}|L{4}|C{4}|D{4}|M{4}");

            // [25.11.2012 cosmo] TODO 0: add rules for invalid lower value symbols in front of greather or equal value symbols e.g. "IL" ... "IIV" ... etc
        }

        #region Private methods

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
            // the idea is that we reach the end of roman string, so there is no more digit
            return '-';
        }

        #endregion
    }
}