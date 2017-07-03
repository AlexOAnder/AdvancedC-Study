
namespace FibbonaciClass.Redis
{
    using System;

    using StackExchange.Redis;

    public class RedisConnectorHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection;

		static readonly ConfigurationOptions config = new ConfigurationOptions
		{
			AllowAdmin = true,
			EndPoints = { { "127.0.0.1", 6379 } },
			//AbortOnConnectFail = false,
			//SyncTimeout = 3000
		};

        static RedisConnectorHelper()
        {
			//Sometimes breaks down
			RedisConnectorHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(config));
        }

        public static ConnectionMultiplexer Connection
        {
	        get { return lazyConnection.Value; }
        }
    }
}
