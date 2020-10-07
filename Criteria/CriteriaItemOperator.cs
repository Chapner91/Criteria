using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public class CriteriaItemOperator
	{
		public string OperationName { get; private set; }
		public string SQLTranslatorString { get; private set; }
		public string EnglishTranslatorString { get; private set; }

		public CriteriaItemOperator(string operationName, string sqlTranslatorString, string englishTranslatorString)
		{
			OperationName = operationName;
			SQLTranslatorString = sqlTranslatorString;
			EnglishTranslatorString = englishTranslatorString;
		}
	}
}	
