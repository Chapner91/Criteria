using Criteria.CriteriaItems;
using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public interface ICriteriaItem : ICopyable<ICriteriaItem>
	{
		string CriteriaItemType { get; }

		Guid CriteriaItemID { get; }
		DataType ReturnDataType { get; }
		bool ReturnsSingleValue { get; }
		string SQLValue { get; }
		string EnglishValue { get; }

		string Serialize();
	}
}
