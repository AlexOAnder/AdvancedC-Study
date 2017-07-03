using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject2
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
			string fileName = Path.Combine(exePath, "books.xml");
			/*DataContractSerializer dcs = new DataContractSerializer(typeof(Catalog));
			FileStream fs = new FileStream(fileName, FileMode.Open);
			using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
			{
				Catalog catalog = (Catalog)dcs.ReadObject(reader);
			}
			fs.Close();*/


			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Catalog));
			FileStream fs = new FileStream(fileName, FileMode.Open);
			using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
			{
				Catalog c = xmlSerializer.Deserialize(reader) as Catalog;  
				Assert.IsTrue(c.Books.Count>0);
			}
			fs.Close();
			
		}

		[TestMethod]
		public void TestWriteXml()
		{
			Catalog cat = new Catalog();
			cat.Date = DateTime.Now;
			cat.Books = new List<Book>();
			cat.Books.Add(new Book());
			cat.Books.Add(new Book());
			XmlSerializer serializer = new XmlSerializer(cat.GetType());
			using (FileStream fs = new FileStream("test.xml", FileMode.OpenOrCreate))
			{
				serializer.Serialize(fs, cat);
			}

		}
	}
}
