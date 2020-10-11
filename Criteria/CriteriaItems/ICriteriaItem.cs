using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public interface ICriteriaItem
	{
		string CriteriaItemType { get; }

		DataType ReturnDataType { get; set; }
		string Value { get; }
		string SQLValue { get; }
		string EnglishValue { get; }

		string Serialize();
		//string Deserialize();
	}
}
