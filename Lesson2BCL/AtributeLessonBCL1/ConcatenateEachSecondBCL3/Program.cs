using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcatenateEachSecondBCL3
{
    class Program
    {
        //BCL3) Напишите метод для конкатенации каждого второго элемента массива строк в результирующую строку. Обоснуйте выбор реализации.
        static void Main(string[] args)
        {
            String[] words = { "The", "QUICK", "BROWN", "FOX", "jumps",
                         "over", "the", "lazy", "dog" ,"Hiearahy", "Hiccap", "HILLS"};
            // Choosing the StringBuilder  because we need many times to change the result string. 
            // With StringBuilder we create only one instance and dynamically change it 
            // instead of creating many instance of string object every time in cycle.
            StringBuilder sb = new StringBuilder();
            for (int i = words.GetLowerBound(0); i <= words.GetUpperBound(0); i++)
            {
                if (i % 2 == 0)
                {
                    sb.Append(words[i]);
                    sb.Append(",");
                }
            }
            sb.Remove(sb.Length - 1, 1); // remove last comma
            Console.WriteLine("Result is : " + sb);
            Console.ReadKey();

        }

        // BCL4) Приведите пример использования Nullable типа при проектировании класса.
        // Класс не используется для работы с записями БД и не используется для их представления.Обоснуйте свой выбор.

    }
}
