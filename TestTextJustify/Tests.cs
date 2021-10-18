using NUnit.Framework;
using System.Collections.Generic;

namespace TestTextJustify
{
    class Tests
    {
        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[] { new[] { "xxx" }, 3, "xxx" }; //Одно слово без пробелов
            yield return new object[] { new[] { "xxx" }, 4, "xxx " }; //Одно слово с одним пробелом
            yield return new object[] { new[] { "xxx" }, 7, "xxx    " }; //Одно слово с пробелами

            yield return new object[] { new[] { "x", "y" }, 3, "x y" }; //Два слова, один пробел
            yield return new object[] { new[] { "x", "y", "z" }, 5, "x y z" }; //Три слова, по одному пробелу
            yield return new object[] { new[] { "x", "y", "z", "w" }, 7, "x y z w" }; //Четыре слова, по одному пробелу

            yield return new object[] { new[] { "x", "y" }, 4, "x  y" }; //Два слова, два пробела
            yield return new object[] { new[] { "x", "y", "z" }, 7, "x  y  z" }; //Три слова, по два пробела
            yield return new object[] { new[] { "x", "y", "z" }, 13, "x     y     z" }; //Три слова, по пять пробелов
            yield return new object[] { new[] { "x", "y", "z", "w" }, 10, "x  y  z  w" }; //Четыре слова, по два пробела

            yield return new object[] { new[] { "x", "y" }, 5, "x   y" }; //Два слова, три пробела
            yield return new object[] { new[] { "x", "y", "z" }, 9, "x   y   z" }; //Три слова, по три пробела
            yield return new object[] { new[] { "x", "y", "z", "w" }, 13, "x   y   z   w" }; //Четыре слова, по три пробела

            yield return new object[] { new[] { "x", "y", "z" }, 8, "x   y  z" }; //Три слова, по два+ пробела
            yield return new object[] { new[] { "x", "y", "z", "w" }, 11, "x   y  z  w" }; //Четыре слова, по два+ пробела
            yield return new object[] { new[] { "x", "y", "z", "w" }, 12, "x   y   z  w" }; //Четыре слова, по два++ пробела

            yield return new object[] { new[] { "x", "yyyyyyyyyy", "z" }, 14, "x yyyyyyyyyy z" }; //Длинное и два коротких

            yield return new object[] { new[] { "abcd", "efg", "hijkl", "mn" }, 14 + 3, "abcd efg hijkl mn" }; //Разные слова, по одному пробелу
            yield return new object[] { new[] { "abcd", "efg", "hijkl", "mn" }, 14 + 4, "abcd  efg hijkl mn" }; //Разные слова, по одному+ пробелу
            yield return new object[] { new[] { "abcd", "efg", "hijkl", "mn" }, 14 + 5, "abcd  efg  hijkl mn" }; //Разные слова, по одному++ пробелу
            yield return new object[] { new[] { "abcd", "efg", "hijkl", "mn" }, 14 + 6, "abcd  efg  hijkl  mn" }; //Разные слова, по два пробела
            yield return new object[] { new[] { "abcd", "efg", "hijkl", "mn" }, 14 + 7, "abcd   efg  hijkl  mn" }; //Разные слова, по два+ пробела
            yield return new object[] { new[] { "abcd", "efg", "hijkl", "mn" }, 14 + 8, "abcd   efg   hijkl  mn" }; //Разные слова, по два++ пробела
            yield return new object[] { new[] { "abcd", "efg", "hijkl", "mn" }, 14 + 9, "abcd   efg   hijkl   mn" }; //Разные слова, по три пробела

            yield return new object[] { new[] { "xxx", "xxx", "xxx", "xxx" }, 12 + 3, "xxx xxx xxx xxx" }; //одинаковые слова xxx
            yield return new object[] { new[] { "xxx", "xxx", "yyy", "yyy" }, 12 + 3, "xxx xxx yyy yyy" }; //одинаковые слова xxx yyy
            yield return new object[] { new[] { "xxx", "xxx", "xxx", "xxx" }, 12 + 5, "xxx  xxx  xxx xxx" }; //одинаковые слова xxx неравномерно
            yield return new object[] { new[] { "xxx", "xxx", "yyy", "yyy" }, 12 + 5, "xxx  xxx  yyy yyy" }; //одинаковые слова xxx yyy неравномерно

            yield return new object[] { new[] { "x", "y", "z", "t", "a", "b", "c" }, 16, "x  y  z  t a b c" }; //Много промежутков
            yield return new object[] { new[] { "x", "y", "z", "t", "a", "b", "c" }, 17, "x  y  z  t  a b c" }; //Много промежутков
            yield return new object[] { new[] { "x", "y", "z", "t", "a", "b", "c" }, 18, "x  y  z  t  a  b c" }; //Много промежутков
            yield return new object[] { new[] { "x", "y", "z", "t", "a", "b", "c" }, 24, "x   y   z   t   a   b  c" }; //Много промежутков
        }

        [TestCaseSource(nameof(TestCases))]
        public void Test(string[] input, int length, string expected)
        {
            var textJustifier = new TextJustifier();
            var actual = textJustifier.Justify(input, length);
            Assert.AreEqual(expected, actual);
        }
    }
}
