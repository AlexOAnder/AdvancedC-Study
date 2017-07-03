using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpGrabLibrary
{
	interface ConsoleWriterInterface
	{
		void Write(string message);
		void SetStatus(bool value);
	}
}
