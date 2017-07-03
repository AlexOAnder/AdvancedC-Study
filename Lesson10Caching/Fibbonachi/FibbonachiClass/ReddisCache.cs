using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FibbonaciClass.Redis;
using StackExchange.Redis;

namespace FibbonaciClass
{
	
    public class InRedisCache : ICacher
    {
        private IDatabase cacher;

        public InRedisCache()
        {
            this.cacher = RedisConnectorHelper.Connection.GetDatabase();
        }

        public int GetValue(int n)
        {
            if (!this.cacher.KeyExists(n.ToString()))
            {
                this.cacher.StringSet(n.ToString(), FibbonaciNumbers.Generate(n));
            }

            return (int)this.cacher.StringGet(n.ToString());
        }
    }
}
