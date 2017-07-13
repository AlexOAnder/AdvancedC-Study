using System;

namespace SingletonTests
{
	// realization with double check and lock
	class SingletonWithLock
	{
		private static volatile SingletonWithLock _instance;
		private static readonly object _syncRoot = new object();
		private int count = 1000;
		private SingletonWithLock()
		{
		}

		public static SingletonWithLock GetInstance()
		{
			if (_instance == null)
			{
				lock (_syncRoot)
				{
					if (_instance == null)
					{
						_instance = new SingletonWithLock();
						Console.WriteLine("Created a new instance from null");
					}
				}
			}
			else
			{
				Console.WriteLine("Just return exists value");
			}
			
			return _instance;
		}

		public int GetValue()
		{
			return count++;
		}
	}
}