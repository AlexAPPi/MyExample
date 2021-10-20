using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTextJustify
{
    /* Задача:
     * Необходимо реализовать выравнивание текста по ширине в методе Justify класса TextJustifier
     * 
     * Выравнивание строки по ширине достигается расстановкой пробелов между словами по следующим правилам:
     * - Между каждым словом должен стоять хотя бы один пробел
     * - Пробелы в начале строки должны отсутствовать
     * - Пробелы в конце строки должны отсутствовать
     *      Исключение: строка из одного слова добивается пробелами до конца
     *      пример: "a    "
     * - Пробелы распределяются равномерно
     *      пример: "a  b  c  d  e"
     * - Если равномерное распределение пробелов невозможно,
     *      то "лишние" пробелы добавляются по одному в интервалы начиная с крайнего левого
     *      пример: "a   b   c  d  e"
     *
     * Входные данные:
     * - текст - массив слов, состоящий из латинских букв и цифр, БЕЗ пробельных символов, БЕЗ переносов строк
     * - длина результирующей строки - целое неотрицательное число,
     *      достаточно большое, чтобы между каждым словом в результирующей строке вместилось хотя бы по одному пробелу
     * 
     * Пример использования:
     * var result = new TextJustifier().Justify(new [] {"Xxx", "yyy", "123"}, 20);
     * 
     * Для проверки решения необходимо запустить тесты.
     */
    public class TextJustifier
    {
        /// <summary>
        /// Return list of space char
        /// </summary>
        /// <returns></returns>
        private IEnumerable<char> ListOfSpaceChar(int size)
        {
            while(size-- > 0)
            {
                yield return ' ';
            }
        }

        /// <summary>
        /// Return list of space char
        /// </summary>
        /// <returns></returns>
        private char[] ListOfSpaceCharNative(int size)
        {
            char[] result = new char[size];
            for(int i = 0; i < size; i++)
            {
                result[i] = ' ';
            }
            return result;
        }

        /// <summary>
        /// Generate string of space chars
        /// </summary>
        /// <param name="l">Result string length</param>
        /// <returns></returns>
        private string GenerateSpaceString(int length)
        {
            IEnumerable<char> spaces = ListOfSpaceChar(length);
            return new string(spaces.ToArray());
        }

        /// <summary>
        /// Generate string of space chars
        /// </summary>
        /// <param name="length">Result string length</param>
        /// <returns></returns>
        private StringBuilder GenerateSpaceStringBuilder(int length)
        {
            StringBuilder result = new StringBuilder(length);
            while(length-- > 0)
            {
                result.Append(' ');
            }
            return result;
        }

        /// <summary>
        /// Calculate array of string lengths
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        private int CalcStringsLength(string[] stringArray) =>
            stringArray.Sum(someString => someString.Length);

        /// <summary>
        /// Calculate array of string lengths
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        private int CalcStringsLengthNative(string[] stringArray)
        {
            int result = 0;
            for(int i = 0; i < stringArray.Length; i++)
            {
                result += stringArray[i].Length;
            }
            return result;
        }

        /// <summary>
        /// Convert string array to stringbuilder array
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private StringBuilder[] GetStringBuilders(string[] x)
        {
            int length = x.Length;
            StringBuilder[] result = new StringBuilder[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = new StringBuilder(x[i]);
            }
            return result;
        }

        /// <summary>
        /// Justify string array to one string for length
        /// </summary>
        /// <param name="x">String array for justify</param>
        /// <param name="l">Result string length</param>
        /// <returns></returns>
        public string Justify(string[] x, int l)
        {
            int arrayLength = x.Length;
#if UseNative
            int stringsLength = CalcStringsLengthNative(x);
#else
            int stringsLength = CalcStringsLength(x);
#endif
            if (stringsLength > l)
            {
                throw new ArgumentException($"The length of the resulting string is less than the lengths of the concatenated strings");
            }

            if (arrayLength == 1)
            {
                string firstString = x[0];
                {
                    if (stringsLength == l)
                    {
                        return firstString;
                    }

                    StringBuilder sep = GenerateSpaceStringBuilder(l - firstString.Length);
                    return firstString + sep;
                }
            }

            int itemIndex = -1;
            StringBuilder[] builders = GetStringBuilders(x);
            for (var i = l - stringsLength; stringsLength < l; i--)
            {
                itemIndex++;
                builders[itemIndex].Append(' ');
                stringsLength++;

                if (itemIndex == arrayLength - 2)
                {
                    itemIndex = -1;
                }
            }

#if UseNative
            StringBuilder resultStringBuilder = new StringBuilder();
            for(int i = 0; i < arrayLength; i++)
            {
                resultStringBuilder.Append(builders[i]);
            }
            return resultStringBuilder.ToString();      
#else
            return builders.Aggregate((x, y) => x.Append(y)).ToString();
#endif
        }
    }
}
