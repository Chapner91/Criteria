using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.Enums;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	class CriteriaFunctionOneArg : ICriteriaFunction
	{
		public string CriteriaItemType => throw new NotImplementedException();
		public string CriteriaItemFunctionType => throw new NotImplementedException();
		public DataType DataType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public string Value => throw new NotImplementedException();

		public List<DataType> AcceptedDataTypes => throw new NotImplementedException();

		public string Serialize()
		{
			throw new NotImplementedException();
		}
	}
}
