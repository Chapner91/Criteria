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
	public class SimpleCriteriaItemTests
	{

		private string _BasicStringSimpleCriteriaItemjson = "{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":1,\"Value\":\"Test\"}";
		private SimpleCriteriaItem _BasicStringSimpleCriteriaItem = new SimpleCriteriaItem()
		{
			DataType = DataType.String,
			Value = "Test"
		};

		//************************************************************************************
		// constructor tests
		//************************************************************************************

		[Fact()]
		public void SimpleCriteriaItem_ConstructorFromJson()
		{

			var expected = _BasicStringSimpleCriteriaItem;
			var actual = new SimpleCriteriaItem(_BasicStringSimpleCriteriaItemjson);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SimpleCriteriaItem_ConstructorFromPropertyArguments()
		{
			var expected = _BasicStringSimpleCriteriaItem;
			var actual = new SimpleCriteriaItem(DataType.String, "Test");

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// public method tests
		//************************************************************************************

		[Fact()]
		public void Equal_EqualObjects()
		{
			var a = new SimpleCriteriaItem()
			{
				DataType = DataType.String,
				Value = "Test"
			};
			var b = new SimpleCriteriaItem()
			{
				DataType = DataType.String,
				Value = "Test"
			};

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Equal_NotEqualObjects()
		{
			var a = new SimpleCriteriaItem()
			{
				DataType = DataType.String,
				Value = "Test"
			};
			var b = new SimpleCriteriaItem()
			{
				DataType = DataType.String,
				Value = "Test1"
			};

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Serialize_BasicSimpleCriteriaItem()
		{
			var target = new SimpleCriteriaItem()
			{
				DataType = DataType.String,
				Value = "Test"
			};

			var actual = target.Serialize();
			var expected = _BasicStringSimpleCriteriaItemjson;

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// exception tests
		//************************************************************************************

		[Fact()]
		public void SetValue_DoesNotMatchDataType()
		{
			var target = new SimpleCriteriaItem()
			{
				DataType = DataType.Numeric,
				Value = "1"
			};

			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.Value = "Test");
		}

		[Fact()]
		public void SetValue_DoesMatchDataType()
		{
			var target = new SimpleCriteriaItem()
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