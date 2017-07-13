using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1
{
	[TestClass]
	public class SingletonUnitTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			//just test that singleton returns the same object everytimes
			var result = Singleton.GetInstance();
			var counter1 = result.GetCount();
			var result2 = Singleton.GetInstance();
			var counter2 = result2.GetCount();
			Assert.AreNotEqual(counter1, counter2, "Counters must have different values");
			Assert.AreEqual(counter1, 0);
			Assert.AreEqual(counter2, 1);
		}

		[TestMethod]
		public void TestMethod2()
		{
			//first problem of that singleton - we cannot create mock for that object
			// but when we using the reflection and some tricks with interfaces, we can inject our logic inside the singleton.
			//need to call GetInstance at least first time to fill static field with value;
			ISingleton unused = SingletonWithInterface.GetInstance();

			System.Reflection.FieldInfo instance = typeof(SingletonWithInterface).GetField("_instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

			Mock<ISingleton> mockSingleton = new Mock<ISingleton>();

			instance.SetValue(mockSingleton.Object, null);

			ISingleton resutl = SingletonWithInterface.GetInstance();

			Assert.IsNull(resutl);
		}

		[TestMethod]
		public void TestMethod3()
		{
			// first solution to use - using dependency injection 
			// Данный класс использует подход иньекции зависимости DI ,
			// что позволяет добваить наш синглтон как параметр в конструктор потребительского класса
			ISingleton s = SingletonWithInterface.GetInstance();
			ClassConsumer obj = new ClassConsumer(s);
			Assert.AreEqual(obj.DoSmth(), 0);
			Assert.AreEqual(obj.DoSmth(), 1);
		}

		[TestMethod]
		public void TestMethod4()
		{
			//Попробуем использовать механизм ленивой загрузки

		}
	}
}
