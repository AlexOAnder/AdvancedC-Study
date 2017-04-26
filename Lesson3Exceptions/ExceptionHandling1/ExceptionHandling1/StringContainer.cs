using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling1
{
	public class StringContainer: IStringContainer
	{
		private StringBuilder sb;

		public StringContainer()
		{
			sb = new StringBuilder();
		}

		public void AddString(string str)
		{
			sb.Append(str[0] + " ");
		}

		public string ShowResult()
		{
			return sb.ToString();
		}
	}
}
