using Xunit;
using Criteria.CriteriaItems.CriteriaFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.Enums;

namespace Criteria.CriteriaItems.CriteriaFunctions.Tests
{
	public class CriteriaFunctionSchemeTests
	{

		private CriteriaFunctionScheme _lenFunction;
		private CriteriaFunctionScheme _substringFunction;

		public CriteriaFunctionSchemeTests()
		{
			var arguments = new List<Argument>()
			{
				new Argument("expression", DataType.String, true)
			};
			_lenFunction = new CriteriaFunctionScheme("Length", arguments, true, DataType.Numeric, "LEN({expression})", "the length of {expression}");


			arguments = new List<Argument>()
			{
				new Argument("expression", DataType.String, true),
				new Argument("startFromIndex", DataType.Numeric, true),
				new Argument("lengthOfSubstring", DataType.Numeric, true)
			};
			_substringFunction = new CriteriaFunctionScheme("Substring", arguments, true, DataType.String, "SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})", "Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}");
		}


		//************************************************************************************
		// constructor tests
		//************************************************************************************

		[Fact()]
		public void CriteriaFunctionScheme_ConstructorFunctionName()
		{

			var target = _lenFunction;

			var expected = "Length";
			var actual = target.FunctionName;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaFunctionScheme_ConstructorArguments()
		{

			var target = _substringFunction;

			var expected = new List<Argument>()
			{
				new Argument("expression", DataType.String, true),
				new Argument("startFromIndex", DataType.Numeric, true),
				new Argument("lengthOfSubstring", DataType.Numeric, true)
			};
			var actual = target.Arguments;

			Assert.All(expected, x => actual.Contains(x));
			Assert.True(expected.Count == actual.Count);
		}

		[Fact()]
		public void CriteriaFunctionScheme_ConstructorSQLTranslationString()
		{

			var target = _lenFunction;

			var expected = "LEN({expression})";
			var actual = target.SQLTranslationString;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaFunctionScheme_ConstructorEnglishTranslationString()
		{

			var target = _lenFunction;

			var expected = "the length of {expression}";
			var actual = target.EnglishTranslationString;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaFunctionScheme_ConstructorNumberOfReturnValues()
		{

			var target = _lenFunction;

			var expected = true;
			var actual = target.ReturnsSingleValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaFunctionScheme_ConstructorReturnDataType()
		{

			var target = _lenFunction;

			var expected = DataType.Numeric;
			var actual = target.ReturnDataType;

			Assert.Equal(expected, actual);
		}
	}
}