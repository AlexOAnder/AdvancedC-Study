namespace SingletonTests
{
	//Опишите для приведенного примера недостатки паттерна Singleton. Приведите свое решение для устранения этих недостатков.
	// Проблема 1 - нетестируемо. Нельзя заменить синглтон чем то другим во время тестирования mock (проблема всех сингтонов)
	// Проблема 2 - режим гонки может привести к непредсказуемому результату
	public class Singleton 
	{
		private static int _count = 0;
		private static Singleton _instance;
		private Singleton()
		{ }

		public static Singleton GetInstance()
		{
			if (_instance == null)
				_instance = new Singleton();
			return _instance;
		}

		public int GetCount()
		{
			return _count++;
		}

	}
}