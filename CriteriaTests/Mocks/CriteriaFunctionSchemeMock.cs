using Criteria.CriteriaItems.CriteriaFunctions;
using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriteriaTests.Mocks
{
	public class CriteriaFunctionSchemeMockSubstring : ICriteriaFunctionScheme
	{
		public string FunctionSchemeName => "Substring";
		public Guid CriteriaFunctionSchemeID => Guid.Parse("da7945ec-3f8d-44fd-bd9e-c0d143f61e4d");
		public List<Argument> _arguments { get; set; } = new List<Argument>()
		{
			new Argument("expression", DataType.String, true),
			new Argument("startFromIndex", DataType.Numeric, true),
			new Argument("lengthOfSubstring", DataType.Numeric, true)
		};
		public IEnumerable<Argument> Arguments
		{
			get { return _arguments; }
			private set => throw new NotImplementedException();
		}
		public string EnglishTranslationString => "Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}";
		public string SQLTranslationString => "SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})";

		public DataType ReturnDataType { get => DataType.String; set { } }
		public bool ReturnsSingleValue { get => true; set { } }

		public void AddArgument(Argument argument) => throw new NotImplementedException();
		public void RemoveArgument(Guid argumentID) => throw new NotImplementedException();
		public void SetTranslationString(Translator TranslatorToUpdate, string value) => throw new NotImplementedException();
		public ICriteriaFunctionScheme Copy() { throw new NotImplementedException(); }

		public bool Equals(ICriteriaFunctionScheme that)
		{
			throw new NotImplementedException();
		}
	}

	public class CriteriaFunctionSchemeMockMonth : ICriteriaFunctionScheme
	{
		public string FunctionSchemeName => "Month";
		public Guid CriteriaFunctionSchemeID => Guid.Parse("ea7945ec-3f8d-44fd-bd9e-c0d143f61e4d");
		public List<Argument> _arguments { get; set; } = new List<Argument>()
		{
			new Argument("dateExpression", DataType.DateTime, true)
		};
		public IEnumerable<Argument> Arguments
		{
			get { return _arguments; }
			private set => throw new NotImplementedException();
		}
		public string EnglishTranslationString => "the month number of {dateExpression}";
		public string SQLTranslationString => "Month({dateExpression})";

		public DataType ReturnDataType { get => DataType.String; set => throw new NotImplementedException(); }
		public bool ReturnsSingleValue { get => true; set => throw new NotImplementedException(); }

		public void AddArgument(Argument argument) => throw new NotImplementedException();
		public void RemoveArgument(Guid argumentID) => throw new NotImplementedException();
		public void SetTranslationString(Translator TranslatorToUpdate, string value) => throw new NotImplementedException();
		public ICriteriaFunctionScheme Copy() { throw new NotImplementedException(); }

		public bool Equals(ICriteriaFunctionScheme that)
		{
			throw new NotImplementedException();
		}
	}
}
