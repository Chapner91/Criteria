using Xunit;
using Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.Enums;
using Criteria.CriteriaExceptions;

namespace Criteria.CriteriaItems.Tests
{
	public class CriteriaItemSimpleTests
	{

		private string _BasicStringCriteriaItemSimplejson;
		private CriteriaItemSimple _BasicStringCriteriaItemSimple;
		
		public CriteriaItemSimpleTests()
		{
			_BasicStringCriteriaItemSimple = new CriteriaItemSimple(DataType.String, "Test");
			_BasicStringCriteriaItemSimplejson = _BasicStringCriteriaItemSimple.Serialize();//"{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":1,\"Value\":\"Test\"}";
		}

		//************************************************************************************
		// constructor tests
		//************************************************************************************

		[Fact()]
		public void CriteriaItemSimple_ConstructorFromJson()
		{

			var expected = _BasicStringCriteriaItemSimple;
			var actual = new CriteriaItemSimple(_BasicStringCriteriaItemSimplejson);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void CriteriaItemSimple_ConstructorFromPropertyArguments()
		{
			var expected = _BasicStringCriteriaItemSimple;
			var actual = new CriteriaItemSimple(DataType.String, "Test");

			Assert.Equal(expected, actual);
		}


		//************************************************************************************
		// property tests
		//************************************************************************************
		[Fact()]
		public void CriteriaItemSimple_Value()
		{
			var target = _BasicStringCriteriaItemSimple;

			var expected = "Test";
			var actual = target.Value;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemSimple_ReturnDataType()
		{
			var target = _BasicStringCriteriaItemSimple;

			var expected = DataType.String;
			var actual = target.ReturnDataType;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemSimple_SQLValue()
		{
			var target = _BasicStringCriteriaItemSimple;

			var expected = "'Test'";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemSimple_EnglishValue()
		{
			var target = _BasicStringCriteriaItemSimple;

			var expected = "\"Test\"";
			var actual = target.EnglishValue;

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// public method tests
		//************************************************************************************

		[Fact()]
		public void Equal_EqualObjects()
		{
			var a = new CriteriaItemSimple()
			{
				ReturnDataType = DataType.String,
				Value = "Test"
			};
			var b = new CriteriaItemSimple()
			{
				ReturnDataType = DataType.String,
				Value = "Test"
			};

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Equal_NotEqualObjects()
		{
			var a = new CriteriaItemSimple()
			{
				ReturnDataType = DataType.String,
				Value = "Test"
			};
			var b = new CriteriaItemSimple()
			{
				ReturnDataType = DataType.String,
				Value = "Test1"
			};

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Serialize_BasicCriteriaItemSimple()
		{
			var target = new CriteriaItemSimple()
			{
				ReturnDataType = DataType.String,
				Value = "Test"
			};

			var actual = target.Serialize();
			var expected = _BasicStringCriteriaItemSimplejson;

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// exception tests
		//************************************************************************************

		[Fact()]
		public void SetValue_DoesNotMatchDataType()
		{
			var target = new CriteriaItemSimple()
			{
				ReturnDataType = DataType.Numeric,
				Value = "1"
			};

			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.Value = "Test");
		}

		[Fact()]
		public void SetValue_DoesMatchDataType()
		{
			var target = new CriteriaItemSimple()
			{
				ReturnDataType = DataType.Numeric,
				Value = "1"
			};

			try
			{
				target.Value = "2";
			}
			catch (CriteriaItemTypeMismatchException ex)
			{

			}

			Assert.True(target.Value == "2");
		}
	}
}