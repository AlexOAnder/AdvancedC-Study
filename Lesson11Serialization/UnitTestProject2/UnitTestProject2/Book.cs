using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UnitTestProject2
{
	[XmlRoot(ElementName = "catalog", Namespace = "http://library.by/catalog")]
	[CollectionDataContract(Name = "catalog", Namespace = "http://library.by/catalog")]
	public class Catalog
	{
		[XmlArray("books")]
		[XmlArrayItem("book", typeof(Book))]
		public List<Book> Books { get; set; }
		[XmlAttribute("date",DataType = "date")]
		public DateTime Date { get; set; }
	}

	[DataContract(Name = "book", Namespace = "")]
	public class Book 
	{
		[XmlAttribute(AttributeName = "id")]
		public string ID { get; set; }

		[XmlElement(ElementName = "isbn")]
		[DataMember(Name = "isbn", Order = 0)]
		public string ISBN { get; set; }

		[XmlElement(ElementName = "author")]
		[DataMember(Name = "author", Order = 1)]
		public string Author { get; set; }

		[XmlElement(ElementName = "title")]
		[DataMember(Name = "title", Order = 2)]
		public string Title { get; set; }

		[XmlElement(ElementName = "genre")]
		[DataMember(Name = "genre", Order = 3)]
		public Genre Genre { get; set; }

		[XmlElement(ElementName = "publisher")]
		[DataMember(Name = "publisher", Order = 4)]
		public string Publisher { get; set; }

		[XmlElement(ElementName = "publish_date",DataType="date")]
		[DataMember(Name = "publish_date", Order = 5)]
		public DateTime PublishDate { get; set; }

		[XmlElement(ElementName = "description")]
		[DataMember(Name = "description", Order = 6)]
		public string Description { get; set; }

		[XmlElement(ElementName = "registration_date", DataType = "date")]
		[DataMember(Name = "registration_date", Order = 7)]
		public DateTime RegistrationDate { get; set; }

	}
	

}

