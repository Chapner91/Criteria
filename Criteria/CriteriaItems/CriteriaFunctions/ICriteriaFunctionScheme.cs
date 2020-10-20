using System;
using System.Collections.Generic;
using Criteria.Enums;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	public interface ICriteriaFunctionScheme
	{
		IEnumerable<Argument> Arguments { get; }
		Guid CriteriaFunctionSchemeID { get; }
		string EnglishTranslationString { get; }
		string FunctionSchemeName { get; }
		DataType ReturnDataType { get; set; }
		bool ReturnsSingleValue { get; set; }
		string SQLTranslationString { get; }

		void AddArgument(Argument argument);
		void RemoveArgument(Guid argumentID);
		void SetTranslationString(Translator TranslatorToUpdate, string value);

		bool Equals(ICriteriaFunctionScheme that);
		ICriteriaFunctionScheme Copy();
	}
}