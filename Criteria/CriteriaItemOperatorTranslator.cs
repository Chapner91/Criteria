using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public static class CriteriaItemOperatorTranslator
	{
		private static Dictionary<string, CriteriaItemOperator> _translator = new Dictionary<string, CriteriaItemOperator>();

		static CriteriaItemOperatorTranslator()
		{
			// get criteriaItemOperators from DB
			var criteriaItemOperators = new List<CriteriaItemOperator>
			{
				new CriteriaItemOperator("Equal", "{leftSide} = {rightSide}", "{leftSide} is equal to {rightSide}"),
				new CriteriaItemOperator("NotEqual", "{leftSide} != {rightSide}", "{leftSide} is not equal to {rightSide}"),
				new CriteriaItemOperator("LessThan", "{leftSide} < {rightSide}", "{leftSide} is less than {rightSide}"),
				new CriteriaItemOperator("LessThanOrEqual", "{leftSide} <= {rightSide}", "{leftSide} is less than or equal to {rightSide}"),
				new CriteriaItemOperator("GreaterThan", "{leftSide} > {rightSide}", "{leftSide} is greater than {rightSide}"),
				new CriteriaItemOperator("GreaterThanOrEqual", "{leftSide} >= {rightSide}", "{leftSide} is greater than or equal to {rightSide}"),
				new CriteriaItemOperator("InList", "{leftSide} IN ( {rightSide} )", "{leftSide} is in the list ( {rightSide} )"),
				new CriteriaItemOperator("NotInLis", "{leftSide} NOT IN ( {rightSide} )", "{leftSide} is not in the list ( {rightSide} )")
			};

			foreach(CriteriaItemOperator criteriaItemOperator in criteriaItemOperators)
			{
				_translator.Add(criteriaItemOperator.OperationName, criteriaItemOperator);
			}

		}

		public static string TranslateToSQL(string operationName, ICriteriaItem leftSide, ICriteriaItem rightSide)
		{
			return _translator[operationName].SQLTranslatorString.Replace("{leftSide}", leftSide.Value).Replace("{rightSide}", rightSide.Value);
		}

		public static string TranslateToEnglish(string operationName, ICriteriaItem leftSide, ICriteriaItem rightSide)
		{
			return _translator[operationName].EnglishTranslatorString.Replace("{leftSide}", leftSide.Value).Replace("{rightSide}", rightSide.Value);
		}
	}
}