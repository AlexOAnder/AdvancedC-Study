using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpGrabLibrary
{
	public class ConsoleWriter :ConsoleWriterInterface
	{
		private bool status = false;

		public void Write(string message)
		{
			if (status)
				Console.WriteLine(message);
			// if we will use logger then we log about status here - but not show that in console
		}

		public void SetStatus(bool value)
		{
			status = value;
		}
	}
}
