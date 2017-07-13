using System;

namespace SingletonTests
{
	// Данный класс использует подход иньекции зависимости DI ,
	// что позволяет добваить наш синглтон как параметр в конструктор потребительского класса
	public class ClassConsumer
	{
		private readonly ISingleton _dependency;

		public ClassConsumer(ISingleton dependency)
		{
			if (dependency == null)
				throw new ArgumentException("dependency not founded");

			this._dependency = dependency;
		}

		public int DoSmth()
		{
			return _dependency.GetValue();
		}
	}
}