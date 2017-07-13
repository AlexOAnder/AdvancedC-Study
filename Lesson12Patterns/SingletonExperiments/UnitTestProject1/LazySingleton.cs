using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonTests
{
	public class LazySingleton
	{
		private int _counter;
		private static readonly Lazy<LazySingleton> _instance = new Lazy<LazySingleton>(() => new LazySingleton());

		public static LazySingleton Instance
		{
			get { return _instance.Value; }
		}

		public int GetCount()
		{
			return _counter++;
		}
	}
}
