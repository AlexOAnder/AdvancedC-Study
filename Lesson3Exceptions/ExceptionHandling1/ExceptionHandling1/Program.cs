using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling1
{
	class Program
	{
		static void Main(string[] args)
		{
			//Напишите консольное приложение, которое выводит на экран первый из введенных символов каждой строки ввода.
			//Опишите корректное поведение приложения, если пользователь ввел пустую строку.
			Console.WriteLine("Enter your lines of the text. Type '-q' for the exit ");
			StringBuilder sb = new StringBuilder();
			try
			{
				while (true)
				{

					var str = Console.ReadLine();
					if (string.IsNullOrWhiteSpace(str))
					{
						throw  new NullReferenceException();
					}
					if (str.IndexOf("-q", StringComparison.Ordinal) == -1)
					{
						sb.Append(str[0]);
						sb.Append(" ");
					}
					else
					{
						break;
					}
				}

			}
			catch (NullReferenceException nullEx)
			{
				Console.WriteLine("We catched a null reference" + nullEx.Message);
			}
			finally
			{
				Console.WriteLine("Result is: ");
				Console.WriteLine(sb.ToString());
				Console.ReadKey();
			}
		}
	}
}
