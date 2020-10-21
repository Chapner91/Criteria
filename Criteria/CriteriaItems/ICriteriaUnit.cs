using Criteria.CriteriaUnits;
using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public interface ICriteriaUnit : ICopyable<ICriteriaUnit>
	{
		string CriteriaUnitType { get; }

		Guid CriteriaUnitID { get; }
		DataType ReturnDataType { get; }
		bool ReturnsSingleValue { get; }
		string SQLValue { get; }
		string EnglishValue { get; }

		string Serialize();
	}
}
