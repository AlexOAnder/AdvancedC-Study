using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SingletonTests
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
			Assert.AreEqual(result, result2);
			Assert.AreNotEqual(counter1, counter2, "Counters must have different values");
			Assert.AreEqual(counter1, 0);
			Assert.AreEqual(counter2, 1);
		}

		[TestMethod]
		public void TestMethod2()
		{
			// first problem of that singleton - we cannot create mock for that object
			// but when we using the reflection and some tricks with interfaces, we can inject our logic inside the singleton.
			// need to call GetInstance at least first time to fill static field with value;
			ISingleton unused = SingletonWithInterface.GetInstance();

			System.Reflection.FieldInfo instance = typeof(SingletonWithInterface).GetField("_instance",
				System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

			Mock<ISingleton> mockSingleton = new Mock<ISingleton>();

			instance.SetValue(mockSingleton.Object, null);

			ISingleton resutl = SingletonWithInterface.GetInstance();

			Assert.IsNull(resutl);
		}

		[TestMethod]
		public void ConcurentTest()
		{
			var list = new List<Thread>();
			try
			{
				for (int i = 0; i < 10; i++)
				{
					var thread = new Thread(TestLock);
					list.Add(thread);

					Console.WriteLine("Thread {0} Setted", i);
				}
				// На старт — запускаем стартовые потоки и ждём их запуска
				foreach (var thread in list)
				{
					thread.Start();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				list.ForEach(x => x.Abort());
			}
			finally
			{
				Thread.Sleep(10000);
				var count = 0;
				foreach (var thread in list)
				{
					if (thread.ThreadState == ThreadState.Running)
						thread.Abort();
					Console.WriteLine("Item {0} must be shut down and its state {1}",++count,thread.ThreadState);
					if (thread.ThreadState == ThreadState.WaitSleepJoin)
						thread.Interrupt();
				}
			}


		}

		private void TestLock()
		{
			var s = SingletonWithLock.GetInstance();
			//var s = SingletonWithInterface.GetInstance();
			//var s = Singleton.GetInstance();
			//Console.WriteLine(s.GetValue());
			Thread.Sleep(1000);
		}
	}
}
