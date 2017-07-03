using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerformanceCounterHelper;

namespace MvcMusicStore.Infrastructure
{
	public static class CounterHelper
	{
		public static CounterHelper<Counters> counterHelper = PerformanceHelper.CreateCounterHelper<Counters>("Test project");
	}
}