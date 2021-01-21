using System;
using System.Text;

namespace NumberToWordConversion
{
    public static class NumberToNameConverter
    {
        static readonly string _zero = "zero";

        static readonly string[] _zeroToTeens = new string[]
        {
            String.Empty, 
            "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
            "ten", "eleven", "twelve",
            "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
        };

        static readonly string[] _tenMultiples = new string[]
        {
            String.Empty, String.Empty, "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"
        };

        private enum Period
        {
            Hundred = 0,
            Thousand = 1,
            Million = 2,
            Billion = 3
        };

        static readonly string[] _periodText = new string[] { "hundred", "thousand", "million", "billion" };

        private const UInt32 BillionPeriodBegin = 1000000000;
        private const UInt32 MillionPeriodBegin = 1000000;
        private const UInt32 ThousandPeriodBegin = 1000;
        private const UInt32 HundredPeriodBegin = 100;
        private const UInt32 TenPeriodBegin = 10;
        private const UInt32 MagicTwenty = 20;

        // MaxValue
        // = 2^32
        // = 4,294,967,295 
        // = Four Billion Two Hundred Ninety Four Million Nine Hundred Sixty Seven Thousand Two Hundred Ninety Five
        public static string ConvertToWords(this UInt32 value)
        {
            var valueInWords = new StringBuilder();

            if (value == 0)
            {
                valueInWords.Append(_zero);
            }
            else
            {
                value
                    .ConvertForPeriod(valueInWords, Period.Billion, BillionPeriodBegin)
                    .ConvertForPeriod(valueInWords, Period.Million, MillionPeriodBegin)
                    .ConvertForPeriod(valueInWords, Period.Thousand, ThousandPeriodBegin)
                    .ConvertForPeriod(valueInWords, Period.Hundred, HundredPeriodBegin)
                    .ConvertSubHundred(valueInWords)
                    .ConvertSubTwenty(valueInWords);
            }

            return BuildNumberWord(valueInWords);
        }

        private static UInt32 ConvertForPeriod(this UInt32 value, StringBuilder valueInWords, Period period, UInt32 periodBegin)
        {
            if (value < periodBegin)
            {
                return value;
            }

            var periodValue = value / periodBegin;

            periodValue
                .ConvertForPeriod(valueInWords, Period.Hundred, HundredPeriodBegin)
                .ConvertSubHundred(valueInWords)
                .ConvertSubTwenty(valueInWords);

            valueInWords.Append($"{_periodText[(int)period]} ");

            return value % periodBegin;
        }

        private static UInt32 ConvertSubHundred(this UInt32 value, StringBuilder valueInWords)
        {
            if (value < MagicTwenty)
            {
                return value;
            }

            var periodValue = value / TenPeriodBegin;

            valueInWords.Append($"{_tenMultiples[periodValue]} ");

            return value % TenPeriodBegin;
        }

        private static UInt32 ConvertSubTwenty(this UInt32 value, StringBuilder valueInWords)
        {
            if (value >= MagicTwenty)
            {
                return value;
            }

            if (value != 0)
            {
                valueInWords.Append($"{_zeroToTeens[value]} ");
            }

            return 0;
        }

        private static string BuildNumberWord(StringBuilder valueInWords)
        {
            var startLetter = valueInWords[0].ToString().ToUpper();

            valueInWords.Remove(0, 1).Insert(0, startLetter);

            return valueInWords.ToString();
        }
    }

    class Program
    {
        static void ConvertAndPrint(UInt32 value)
        {
            var valueInWords = value.ConvertToWords();
            Console.WriteLine($"{value} - {valueInWords}");
        }

        static void Main(string[] args)
        {
            ConvertAndPrint((UInt32)1);
            ConvertAndPrint((UInt32)163);
            ConvertAndPrint((UInt32)2462);
            ConvertAndPrint((UInt32)749241);
            ConvertAndPrint((UInt32)4234643);
            Console.WriteLine();

            ConvertAndPrint((UInt32)0);
            ConvertAndPrint((UInt32)1);
            ConvertAndPrint((UInt32)2);
            ConvertAndPrint((UInt32)3);
            ConvertAndPrint((UInt32)4);
            ConvertAndPrint((UInt32)5);
            ConvertAndPrint((UInt32)6);
            ConvertAndPrint((UInt32)7);
            ConvertAndPrint((UInt32)8);
            ConvertAndPrint((UInt32)9);
            ConvertAndPrint((UInt32)10);
            ConvertAndPrint((UInt32)11);
            ConvertAndPrint((UInt32)12);
            ConvertAndPrint((UInt32)13);
            ConvertAndPrint((UInt32)14);
            ConvertAndPrint((UInt32)15);
            ConvertAndPrint((UInt32)16);
            ConvertAndPrint((UInt32)17);
            ConvertAndPrint((UInt32)18);
            ConvertAndPrint((UInt32)19);
            ConvertAndPrint((UInt32)20);
            ConvertAndPrint((UInt32)21);
            ConvertAndPrint((UInt32)30);
            ConvertAndPrint((UInt32)32);
            ConvertAndPrint((UInt32)40);
            ConvertAndPrint((UInt32)43);
            ConvertAndPrint((UInt32)50);
            ConvertAndPrint((UInt32)54);
            ConvertAndPrint((UInt32)60);
            ConvertAndPrint((UInt32)65);
            ConvertAndPrint((UInt32)70);
            ConvertAndPrint((UInt32)76);
            ConvertAndPrint((UInt32)80);
            ConvertAndPrint((UInt32)87);
            ConvertAndPrint((UInt32)90);
            ConvertAndPrint((UInt32)98);
            ConvertAndPrint((UInt32)99);
            ConvertAndPrint((UInt32)100);
            ConvertAndPrint((UInt32)101);
            ConvertAndPrint((UInt32)110);
            ConvertAndPrint((UInt32)111);
            ConvertAndPrint((UInt32)200);
            ConvertAndPrint((UInt32)299);
            ConvertAndPrint((UInt32)999);
            ConvertAndPrint((UInt32)1000);
            ConvertAndPrint((UInt32)1010);
            ConvertAndPrint((UInt32)1099);
            ConvertAndPrint((UInt32)1999);
            ConvertAndPrint((UInt32)9000);
            ConvertAndPrint((UInt32)9999);
            ConvertAndPrint((UInt32)10000);
            ConvertAndPrint((UInt32)11999);
            ConvertAndPrint((UInt32)90999);
            ConvertAndPrint((UInt32)99999);
            ConvertAndPrint((UInt32)100000);
            ConvertAndPrint((UInt32)900000);
            ConvertAndPrint((UInt32)999999);
            ConvertAndPrint((UInt32)1000000);
            ConvertAndPrint((UInt32)1000009);
            ConvertAndPrint((UInt32)1999999);
            ConvertAndPrint((UInt32)9999999);
            ConvertAndPrint((UInt32)10000000);
            ConvertAndPrint((UInt32)99999999);
            ConvertAndPrint((UInt32)1000000000);
            ConvertAndPrint((UInt32)1000009999);
            ConvertAndPrint((UInt32)UInt32.MaxValue);

            Console.ReadKey();
        }
    }
}
