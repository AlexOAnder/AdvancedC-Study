namespace SingletonTests
{
	public class SingletonWithInterface : ISingleton
	{
		private int _count = 0;
		private static readonly SingletonWithInterface _instance;

		private SingletonWithInterface()
		{ }

		static SingletonWithInterface()
		{
			_instance = new SingletonWithInterface(); 
		}

		public static SingletonWithInterface GetInstance()
		{
			return _instance;
		}

		SingletonWithInterface ISingleton.GetInstance()
		{
			return GetInstance();
		}

		public int GetValue()
		{
			return _count++;
		}
	}
}