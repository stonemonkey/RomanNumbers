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
        public void Calculate_returns_arabic_for_roman_digit(int arabic, string roman)
        {
            var result = roman.ToArabicValue();

            Assert.AreEqual(arabic, result);
        }

        [TestCase(3, 3, 'I')]
        [TestCase(30, 3, 'X')]
        [TestCase(300, 3, 'C')]
        [TestCase(3000, 3, 'M')]
        public void Calculate_returns_sum_for_same_roman_symbol(int arabic, int no, char symbol)
        {
            var roman = GetMultiple(no, symbol);

            var result = roman.ToArabicValue();

            Assert.AreEqual(arabic, result);
        }

        [Test]
        public void Calculate_returns_sum()
        {
            var result = "MCMLIV".ToArabicValue();

            Assert.AreEqual(1954, result);
        }
        
        [Test]
        public void Calculate_returns_sum_2()
        {
            var result = "VIII".ToArabicValue();

            Assert.AreEqual(8, result);
        }

        [Test]
        public void Calculate_returns_4_for_IV()
        {
            var result = "IV".ToArabicValue();

            Assert.AreEqual(4, result);
        }

        [Test]
        public void Calculate_returns_9_for_IX()
        {
            var result = "IX".ToArabicValue();

            Assert.AreEqual(9, result);
        } 
        
        [Test]
        public void Calculate_returns_40_for_XL()
        {
            var result = "XL".ToArabicValue();

            Assert.AreEqual(40, result);
        }       
        
        [Test]
        public void Calculate_returns_90_for_XC()
        {
            var result = "XC".ToArabicValue();

            Assert.AreEqual(90, result);
        }
        
        [Test]
        public void Calculate_returns_400_for_CD()
        {
            var result = "CD".ToArabicValue();

            Assert.AreEqual(400, result);
        }       
        
        [Test]
        public void Calculate_returns_900_for_CM()
        {
            var result = "CM".ToArabicValue();

            Assert.AreEqual(900, result);
        }

        [Test]
        public void Calculate_throws_when_input_is_null()
        {
            string roman = null;

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [Test]
        public void Calculate_throws_when_input_is_empty()
        {
            var roman = string.Empty;

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [Test]
        public void Calculate_throws_when_input_contains_only_white_chars()
        {
            var roman = "     ";

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [Test]
        public void Calculate_throws_when_input_contains_invalid_ronam_symbols()
        {
            var roman = "M3CM";

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [Test]
        public void Calculate_throws_when_input_contains_I_in_front_of_L()
        {
            var roman = "IL";

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [TestCase('I')]
        [TestCase('V')]
        [TestCase('X')]
        [TestCase('L')]
        [TestCase('C')]
        [TestCase('D')]
        [TestCase('M')]
        public void Calculate_throws_when_input_contains_4_consecutive_times_roman_symbol(char ch)
        {
            var roman = GetMultiple(4, ch);

            Assert.Throws<ArgumentException>(() => roman.ToArabicValue());
        }

        [Test]
        public void Calculate_throws_when_input_contains_5_consecutive_M_chars_in_the_middle()
        {
            var roman = "MCMMMMMLI";

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
