using System;
using System.Collections;
using System.Globalization;
using System.Threading;

namespace SortStringNonCultureBCL2
{
    class Program
    {
        //BCL2) Напишите метод для сортировки массива строк в независимости от региональных 
        //стандартов пользователя. Использование Linq запрещено.

        static void Example2()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            string temp = "file";
            Console.WriteLine("Base equal's result in {1} is : {0}",
                temp.Equals("FILE", StringComparison.CurrentCultureIgnoreCase),
                CultureInfo.CurrentCulture.DisplayName);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            Console.WriteLine("In Turkey language result in {1} is : {0}",
               temp.Equals("FILE", StringComparison.CurrentCultureIgnoreCase),
               CultureInfo.CurrentCulture.DisplayName);
            Console.WriteLine("In Turkey language with Ordinal rule result is {0}: ",
                temp.Equals("FILE", StringComparison.OrdinalIgnoreCase));

        }


        static void Main(string[] args)
        {
            IComparer revComparer = new ReverseStringComparer();
            IComparer comparer = new StringComparer();
            String[] words = { "The", "QUICK", "BROWN", "FOX", "jumps",
                         "over", "the", "lazy", "dog" ,"Hiearahy", "Hiccap", "HILLS"};

            // set basic culture to ru-RU 
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

            Array.Sort(words);
            Console.WriteLine("After sorting the entire array by using the default comparer:");
            DisplayValues(words);

            Array.Sort(words, comparer);
            Console.WriteLine("After sorting the entire array by using My string comparer:");
            DisplayValues(words);

            Array.Sort(words, revComparer);
            Console.WriteLine("After sorting the entire array using the My String reverse comparer:");
            DisplayValues(words);

            // Change Culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            Console.WriteLine("In Turkey language");
            //reset previous sorting
            words = new String[] { "The", "QUICK", "BROWN", "FOX", "jumps",
                         "over", "the", "lazy", "dog" ,"Hiearahy", "Hiccap", "HILLS"};


            Array.Sort(words);
            Console.WriteLine("TR After sorting the entire array by using the default comparer:");
            DisplayValues(words);

            Array.Sort(words, comparer);
            Console.WriteLine("TR After sorting the entire array by using My stringcomparer:");
            DisplayValues(words);

            Array.Sort(words, revComparer);
            Console.WriteLine("TR After sorting the entire array using the My reverse string comparer:");
            DisplayValues(words);


            Console.ReadKey();


        }

        public static void DisplayValues(String[] arr)
        {
            for (int i = arr.GetLowerBound(0); i <= arr.GetUpperBound(0);i++)
            {
                Console.WriteLine("[{0}] : {1}", i, arr[i]);
            }
            Console.WriteLine();
        }

        public class ReverseStringComparer : IComparer
        {
            // Call CaseInsensitiveComparer.Compare with the parameters reversed.
            public int Compare(Object x, Object y)
            {
                if (x is string && y is string)
                {
                    return String.Compare((string)y, (string)x, StringComparison.Ordinal);
                }
                return (new Comparer(CultureInfo.CurrentCulture)).Compare(y, x);
            }
        }

        public class StringComparer : IComparer
        {
            public int Compare(Object x, Object y)
            {
                if (x is string && y is string)
                {
                    return String.Compare((string)x, (string)y,StringComparison.Ordinal);
                }
                return (new Comparer(CultureInfo.CurrentCulture)).Compare(x, y);
            }
        }
    }
}
