using System;
using System.Text;
using NUnit.Framework;

namespace RomanNumbers
{
    [TestFixture]
    public class RomanNumbersStringExtensionsTests
    {
        [TestCase(1, "I")]
        [TestCase(5, "V")]
        [TestCase(10, "X")]
        [TestCase(50, "L")]
        [TestCase(100, "C")]
        [TestCase(500, "D")]
        [TestCase(1000, "M")]
        public void ToArabicValue_returns_arabic_for_roman_digit(int arabic, string roman)
        {
            var result = roman.ToArabicValue();

            Assert.AreEqual(arabic, result);
        }

        [TestCase(3, 3, 'I')]
        [TestCase(30, 3, 'X')]
        [TestCase(300, 3, 'C')]
        [TestCase(3000, 3, 'M')]
        public void ToArabicValue_returns_sum_for_same_roman_symbol(int arabic, int no, char symbol)
        {
            var roman = GetMultiple(no, symbol);

            var result = roman.ToArabicValue();

            Assert.AreEqual(arabic, result);
        }

        [TestCase(1954, "MCMLIV")]
        [TestCase(1944, "MCMXLIV")]
        [TestCase(88, "LXXXVIII")]
        [TestCase(39, "XXXIX")]
        public void ToArabicValue_returns_sum_for_roman_number(int sum, string roman)
        {
            var result = roman.ToArabicValue();

            Assert.AreEqual(sum, result);
        }

        [Test]
        public void ToArabicValue_returns_4_for_IV()
        {
            var result = "IV".ToArabicValue();

            Assert.AreEqual(4, result);
        }

        [Test]
        public void ToArabicValue_returns_9_for_IX()
        {
            var result = "IX".ToArabicValue();

            Assert.AreEqual(9, result);
        } 
        
        [Test]
        public void ToArabicValue_returns_40_for_XL()
        {
            var result = "XL".ToArabicValue();

            Assert.AreEqual(40, result);
        }       
        
        [Test]
        public void ToArabicValue_returns_90_for_XC()
        {
            var result = "XC".ToArabicValue();

            Assert.AreEqual(90, result);
        }
        
        [Test]
        public void ToArabicValue_returns_400_for_CD()
        {
            var result = "CD".ToArabicValue();

            Assert.AreEqual(400, result);
        }       
        
        [Test]
        public void ToArabicValue_returns_900_for_CM()
        {
            var result = "CM".ToArabicValue();

            Assert.AreEqual(900, result);
        }

        [Test]
        public void ToArabicValue_throws_when_input_is_null()
        {
            string roman = null;

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [Test]
        public void ToArabicValuethrows_when_input_is_empty()
        {
            var roman = string.Empty;

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [Test]
        public void ToArabicValue_throws_when_input_contains_only_white_chars()
        {
            var roman = "     ";

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [Test]
        public void ToArabicValue_throws_when_input_contains_invalid_roman_symbols()
        {
            var roman = "M3CM";

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [TestCase("IL")] [TestCase("IC")] [TestCase("ID")] [TestCase("IM")]
        [TestCase("VX")] [TestCase("VL")] [TestCase("VC")] [TestCase("VD")] [TestCase("VM")]
        [TestCase("XD")] [TestCase("XM")]
        [TestCase("LC")] [TestCase("LD")] [TestCase("LM")]
        [TestCase("IIV")] [TestCase("XXL")] [TestCase("CCM")]
        public void ToArabicValue_throws_when_input_contains_lower_value_symbol_in_front_of_greather_value_symbol(
            string invalidRoman)
        {
            Assert.Throws<ArgumentException>(() => invalidRoman.ToArabicValue());
        }

        [TestCase('I')]
        [TestCase('X')]
        [TestCase('C')]
        [TestCase('M')]
        public void ToArabicValue_throws_when_input_contains_4_consecutive_times_roman_symbol(char ch)
        {
            var roman = GetMultiple(4, ch);

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [TestCase('V')]
        [TestCase('L')]
        [TestCase('D')]
        public void ToArabicValue_throws_when_input_contains_2_consecutive_times_roman_symbol(char ch)
        {
            var roman = GetMultiple(2, ch);

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [Test]
        public void ToArabicValue_throws_when_input_contains_5_consecutive_M_chars_in_the_middle()
        {
            var roman = "MCMMMMMLI";

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }
        
        [Test]
        public void ToArabicValue_throws_when_input_contains_2_non_consecutive_L_chars_in_the_middle()
        {
            var roman = "MCLXLI";

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        private string GetMultiple(int no, char symbol)
        {
            var roman = new StringBuilder();
            
            for (var i = 0; i < no; i++)
                roman.Append(symbol);
            
            return roman.ToString();
        }
    }
}
