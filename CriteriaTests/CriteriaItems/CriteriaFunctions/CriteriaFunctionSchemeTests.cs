using Xunit;
using Criteria.CriteriaItems.CriteriaFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.Enums;
using Criteria.CriteriaExceptions;

namespace Criteria.CriteriaItems.CriteriaFunctions.Tests
{
	public class CriteriaFunctionSchemeTests
	{

		private CriteriaFunctionScheme _lenFunction;
		private CriteriaFunctionScheme _substringFunction;
		private Guid guid1 = Guid.NewGuid();
		private Guid guid2 = Guid.NewGuid();
		private Guid guid3 = Guid.NewGuid();


		public CriteriaFunctionSchemeTests()
		{
			var arguments = new List<Argument>()
			{
				new Argument(guid1, "expression", DataType.String, true)
			};
			_lenFunction = new CriteriaFunctionScheme(guid2, "Length", arguments, true, DataType.Numeric, "LEN({expression})", "the length of {expression}");


			arguments = new List<Argument>()
			{
				new Argument(guid1, "expression", DataType.String, true),
				new Argument(guid2, "startFromIndex", DataType.Numeric, true),
				new Argument(guid3, "lengthOfSubstring", DataType.Numeric, true)
			};
			_substringFunction = new CriteriaFunctionScheme("Substring", arguments, true, DataType.String, "SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})", "Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}");
		}


		//************************************************************************************
		// constructor tests
		//************************************************************************************

		[Fact()]
		public void Constructor_ID()
		{

			var target = _lenFunction;

			var expected = guid2;
			var actual = target.CriteriaFunctionSchemeID;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Constructor_FunctionName()
		{

			var target = _lenFunction;

			var expected = "Length";
			var actual = target.FunctionSchemeName;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Constructor_Arguments()
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
			Assert.True(expected.Count() == actual.Count());
		}

		[Fact()]
		public void Constructor_SQLTranslationString()
		{

			var target = _lenFunction;

			var expected = "LEN({expression})";
			var actual = target.SQLTranslationString;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Constructor_EnglishTranslationString()
		{

			var target = _lenFunction;

			var expected = "the length of {expression}";
			var actual = target.EnglishTranslationString;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Constructor_NumberOfReturnValues()
		{
			var target = _lenFunction;

			var expected = true;
			var actual = target.ReturnsSingleValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Constructor_ReturnDataType()
		{
			var target = _lenFunction;

			var expected = DataType.Numeric;
			var actual = target.ReturnDataType;

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// method tests
		//************************************************************************************

		[Fact()]
		public void AddArgument()
		{
			var target = _lenFunction;
			target.AddArgument(new Argument(guid2, "test", DataType.String, true));

			var expected = new List<Argument>()
			{
				new Argument(guid1, "expression", DataType.String, true),
				new Argument(guid2, "test", DataType.String, true)
			};

			var actual = target.Arguments.ToList();

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void RemoveArgument()
		{
			var target = _substringFunction;
			target.RemoveArgument(guid2);

			var expected = new List<Argument>()
			{
				new Argument(guid1, "expression", DataType.String, true),
				new Argument(guid3, "lengthOfSubstring", DataType.Numeric, true)
			};
			var actual = target.Arguments;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void RemoveArgument_NoMatchingGuid()
		{
			var target = _substringFunction;
			target.RemoveArgument(Guid.NewGuid());

			var expected = new List<Argument>()
			{
				new Argument(guid1, "expression", DataType.String, true),
				new Argument(guid2, "startFromIndex", DataType.Numeric, true),
				new Argument(guid3, "lengthOfSubstring", DataType.Numeric, true)
			};
			var actual = target.Arguments;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void SetTranslatorString_EnglishTranslator()
		{
			var target = _lenFunction;

			target.SetTranslationString(Translator.English, "Test {expression}");
			var expected = "Test {expression}";
			var actual = target.EnglishTranslationString;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void SetTranslatorString_SQLTranslator()
		{
			var target = _lenFunction;

			target.SetTranslationString(Translator.SQL, "Test {expression}");
			var expected = "Test {expression}";
			var actual = target.SQLTranslationString;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void SetTranslatorString_OnlyEffectsOneTranslator()
		{
			var target = _lenFunction;

			// this is to avoid a tautology
			target.SetTranslationString(Translator.English, "another Test {expression}");
			target.SetTranslationString(Translator.SQL, "change this on too {expression}");
			var expected = "another Test {expression}";
			var actual = target.EnglishTranslationString;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void SetTranslatorString_UnmappedArguments1()
		{
			var target = _lenFunction;

			Assert.Throws<CriteriaFunctionSchemeUnmappedArgumentException>(() => target.SetTranslationString(Translator.English, "Test"));
		}

		[Fact()]
		public void SetTranslatorString_UnmappedArguments2()
		{
			var target = _substringFunction;

			Assert.Throws<CriteriaFunctionSchemeUnmappedArgumentException>(() => target.SetTranslationString(Translator.English, "Test {expression}"));
		}

		[Fact()]
		public void SetTranslatorString_CriteriaFunctionSchemeUnmappedArgumentException()
		{
			var target = _substringFunction;

			try
			{
				target.SetTranslationString(Translator.English, "Test {expression}");
			}
			catch (CriteriaFunctionSchemeUnmappedArgumentException e)
			{
				Assert.True(e.MissingArguments.Exists(x => x.Name == "startFromIndex") && e.MissingArguments.Exists(x => x.Name == "lengthOfSubstring"));
			}
		}
	}
}