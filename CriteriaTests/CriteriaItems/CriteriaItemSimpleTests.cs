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

		private string _BasicStringCriteriaItemSimplejson = "{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":1,\"Value\":\"Test\"}";
		private CriteriaItemSimple _BasicStringCriteriaItemSimple = new CriteriaItemSimple()
		{
			DataType = DataType.String,
			Value = "Test"
		};

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
		// public method tests
		//************************************************************************************

		[Fact()]
		public void Equal_EqualObjects()
		{
			var a = new CriteriaItemSimple()
			{
				DataType = DataType.String,
				Value = "Test"
			};
			var b = new CriteriaItemSimple()
			{
				DataType = DataType.String,
				Value = "Test"
			};

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Equal_NotEqualObjects()
		{
			var a = new CriteriaItemSimple()
			{
				DataType = DataType.String,
				Value = "Test"
			};
			var b = new CriteriaItemSimple()
			{
				DataType = DataType.String,
				Value = "Test1"
			};

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Serialize_BasicCriteriaItemSimple()
		{
			var target = new CriteriaItemSimple()
			{
				DataType = DataType.String,
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
				DataType = DataType.Numeric,
				Value = "1"
			};

			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.Value = "Test");
		}

		[Fact()]
		public void SetValue_DoesMatchDataType()
		{
			var target = new CriteriaItemSimple()
			{
				DataType = DataType.Numeric,
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