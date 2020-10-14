using Criteria.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	public class CriteriaFunctionScheme
	{
		public string FunctionName { get; private set; }
		//public string CriteriaFunctionType { get; private set; }
		public List<Argument> Arguments { get; private set; }
		public bool ReturnsSingleValue { get; private set; }
		public DataType ReturnDataType { get; private set; }
		public string SQLTranslationString { get; private set; }
		public string EnglishTranslationString { get; private set; }

		public CriteriaFunctionScheme(string functionName, IEnumerable<Argument> arguments,
			bool returnsSingleValue, DataType returnDataType, string sqlTranslationString, string englishTranslationString)
		{
			FunctionName = functionName;
			//CriteriaFunctionType = criteriaFunctionType;
			Arguments = arguments.ToList();
			ReturnsSingleValue = returnsSingleValue;
			ReturnDataType = returnDataType;
			SQLTranslationString = sqlTranslationString;
			EnglishTranslationString = englishTranslationString;
		}
	}
}

