using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace FibbonaciClass
{
	public class MemmoryCache : ICacher
	{
		private ObjectCache cache;

		public MemmoryCache()
		{
			this.cache = MemoryCache.Default;
		}

		public int GetValue(int n)
		{
			if (!this.cache.Contains(n.ToString()))
			{
				var expirationDate = new DateTimeOffset(DateTime.Now + new TimeSpan(0, 2, 0));
				this.cache.Add(n.ToString(), FibbonaciNumbers.Generate(n), expirationDate);
			}

			return (int)this.cache.Get(n.ToString());
		}
	}
}
