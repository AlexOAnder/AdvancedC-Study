using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling1
{
	// that mean that all contracts can be taken from class AvancedContractSample
	[ContractClass(typeof(AdvancedContractSample))]
	public interface IStringContainer
	{
		void AddString(string str);

		string ShowResult();
	}

	[ContractClassFor(typeof(IStringContainer))]
	sealed class AdvancedContractSample : IStringContainer
	{  
		// here we are implement a method from interface
		// and encapsulate all contracts rules
		public void AddString(string str)
		{
			//And describe the Contract - that the string must be not null
			Contract.Requires(!string.IsNullOrEmpty(str));
			// if we need to return a value - we cann return any value because Contract will take pre and post requerement on compiling
			// but realization will be taken from runtime
		}
		public string ShowResult()
		{
			return null;
		}
	}
}
