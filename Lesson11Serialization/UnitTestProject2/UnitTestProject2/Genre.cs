using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UnitTestProject2
{
	
	public enum Genre
	{
		[XmlEnum("Romance")]
		Romance = 0,
		[XmlEnum("Fantasy")]
		Fantasy = 1,
		[XmlEnum("Computer")]
		Computer = 2,
		[XmlEnum("Horror")]
		Horror = 3,
		[XmlEnum("Science Fiction")]
		ScienceFiction = 4
	}
}
