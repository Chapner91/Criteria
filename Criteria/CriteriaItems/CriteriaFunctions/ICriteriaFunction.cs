using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	public interface ICriteriaFunction : ICriteriaItem
	{
		string CriteriaItemFunctionType { get; }
		List<DataType> AcceptedDataTypes { get; }  
	}
}
