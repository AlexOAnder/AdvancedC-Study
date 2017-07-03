using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerformanceCounterHelper;

namespace MvcMusicStore.Infrastructure
{
	[PerformanceCounterCategory("MvcMusicStor", System.Diagnostics.PerformanceCounterCategoryType.MultiInstance, "MvcMusicStor")]
	public enum Counters
	{
		[PerformanceCounter("Shopping cart count", "Shopping cart count", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
		ShoppingCart,

		[PerformanceCounter("LogIn count", "LogIn count", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
		LogIn,

		[PerformanceCounter("LogOut count", "LogOut count", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
		LogOut
	}
}