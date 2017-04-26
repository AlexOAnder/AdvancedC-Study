using System;
using System.Diagnostics.Contracts;
using ExceptionHandling1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestAddingTheElement()
		{
			StringContainer sc = new StringContainer();
			sc.AddString("Jajs");
			sc.AddString("kolen");
			var res = sc.ShowResult();
			var expected = "J k ";
			Assert.AreEqual(expected, res);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestAddingTheEmptyString()
		{
			// expect the exception of the exception System.Diagnostics.Contracts.__ContractsRuntime+ContractException
			StringContainer sc = new StringContainer();
			string str = null;
			sc.AddString(str);
		}
	}
}
