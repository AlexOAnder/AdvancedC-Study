using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Choose how deep we need to search ?");
			string value = Console.ReadLine();
			int level =Convert.ToInt32(value);
			string enteredUrl = "http://samlib.ru/b/barmaglo/index_1.shtml";
			var answer = Task.Run(() => HttpGrabLibrary.Grabber.StartToDownloadAsync(enteredUrl, level, showStatusInConsole: true));
			answer.Wait();
			Console.WriteLine("completed");
			Console.ReadKey();
		}
	}
}
