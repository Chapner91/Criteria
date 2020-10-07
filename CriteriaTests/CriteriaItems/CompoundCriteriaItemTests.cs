using Xunit;
using Criteria.CriteriaItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.Enums;
using Criteria.CriteriaExceptions;

namespace Criteria.CriteriaItems.Tests
{
	public class CompoundCriteriaItemTests
	{
		private string _numericCompoundCriteriaItemJson = "{\"$type\":\"Criteria.CriteriaItems.CompoundCriteriaItem, Criteria\",\"DataType\":0,\"Value\":\"((1),(2))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":0,\"Value\":\"1\"},{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":0,\"Value\":\"2\"}]}";
		private CompoundCriteriaItem _numericCompoundCriteriaItem = new CompoundCriteriaItem()
		{
			DataType = DataType.Numeric,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.Numeric, "1"),
					new SimpleCriteriaItem(DataType.Numeric, "2")
				}
		};

		private string _nestedNumericCompoundCriteriaItemJson = "{\"$type\":\"Criteria.CriteriaItems.CompoundCriteriaItem, Criteria\",\"DataType\":0,\"Value\":\"(1),(2),((3),(4))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":0,\"Value\":\"1\"},{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":0,\"Value\":\"2\"},{\"$type\":\"Criteria.CriteriaItems.CompoundCriteriaItem, Criteria\",\"DataType\":0,\"Value\":\"(3),(4)\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":0,\"Value\":\"3\"},{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":0,\"Value\":\"4\"}]}]}";
		private CompoundCriteriaItem _nestedNumericCompoundCriteriaItem = new CompoundCriteriaItem()
		{
			DataType = DataType.Numeric,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.Numeric, "1"),
					new SimpleCriteriaItem(DataType.Numeric, "2"),
					new CompoundCriteriaItem()
					{
						DataType = DataType.Numeric,
						CriteriaItems = new List<ICriteriaItem>()
						{
							new SimpleCriteriaItem(DataType.Numeric, "3"),
							new SimpleCriteriaItem(DataType.Numeric, "4")
						}
					}
				}
		};

		private string _stringCompoundCriteriaItemJson = "{\"$type\":\"Criteria.CriteriaItems.CompoundCriteriaItem, Criteria\",\"DataType\":1,\"Value\":\"((Test1),(Test2))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":1,\"Value\":\"Test1\"},{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":1,\"Value\":\"Test2\"}]}";
		private CompoundCriteriaItem _stringCompoundCriteriaItem = new CompoundCriteriaItem()
		{
			DataType = DataType.String,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.String, "Test1"),
					new SimpleCriteriaItem(DataType.String, "Test2")
				}
		};


		private string _nestedStringCompoundCriteriaItemJson = "{\"$type\":\"Criteria.CriteriaItems.CompoundCriteriaItem, Criteria\",\"DataType\":1,\"Value\":\"((Test1),(Test2)),(((Test3),(Test4)))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":1,\"Value\":\"Test1\"},{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":1,\"Value\":\"Test2\"},{\"$type\":\"Criteria.CriteriaItems.CompoundCriteriaItem, Criteria\",\"DataType\":1,\"Value\":\"((Test3),(Test4))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":1,\"Value\":\"Test3\"},{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":1,\"Value\":\"Test4\"}]}]}";
		private CompoundCriteriaItem _nestedStringCompoundCriteriaItem = new CompoundCriteriaItem()
		{
			DataType = DataType.String,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.String, "Test1"),
					new SimpleCriteriaItem(DataType.String, "Test2"),
					new CompoundCriteriaItem()
						{
							DataType = DataType.String,
							CriteriaItems = new List<ICriteriaItem>()
								{
									new SimpleCriteriaItem(DataType.String, "Test3"),
									new SimpleCriteriaItem(DataType.String, "Test4")
								}
						}
				}
		};

		private string _dateTimeCompoundCriteriaItemJson = "{\"$type\":\"Criteria.CriteriaItems.CompoundCriteriaItem, Criteria\",\"DataType\":2,\"Value\":\"((2020-01-01),(2020-01-02))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":2,\"Value\":\"2020-01-01\"},{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":2,\"Value\":\"2020-01-02\"}]}";
		private CompoundCriteriaItem _dateTimeCompoundCriteriaItem = new CompoundCriteriaItem()
		{
			DataType = DataType.DateTime,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.DateTime, "2020-01-01"),
					new SimpleCriteriaItem(DataType.DateTime, "2020-01-02")
				}
		};

		private string _booleanCompoundCriteriaItemJson = "{\"$type\":\"Criteria.CriteriaItems.CompoundCriteriaItem, Criteria\",\"DataType\":3,\"Value\":\"((true),(false))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":3,\"Value\":\"true\"},{\"$type\":\"Criteria.CriteriaItems.SimpleCriteriaItem, Criteria\",\"DataType\":3,\"Value\":\"false\"}]}";
		private CompoundCriteriaItem _booleanCompoundCriteriaItem = new CompoundCriteriaItem()
		{
			DataType = DataType.Boolean,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.Boolean, "true"),
					new SimpleCriteriaItem(DataType.Boolean, "false")
				}
		};

		//************************************************************************************
		// constructor tests
		//************************************************************************************

		[Fact()]
		public void CompoundCriteriaItem_ConstructorFromJson_NumericCompoundCriteriaItem()
		{

			var expected = _numericCompoundCriteriaItem;
			var actual = new CompoundCriteriaItem(_numericCompoundCriteriaItemJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CompoundCriteriaItem_ConstructorFromJson_NestedNumericCompoundCriteriaItem()
		{

			var expected = _nestedNumericCompoundCriteriaItem;
			var actual = new CompoundCriteriaItem(_nestedNumericCompoundCriteriaItemJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CompoundCriteriaItem_ConstructorFromJson_StringCompoundCriteriaItem()
		{

			var expected = _stringCompoundCriteriaItem;
			var actual = new CompoundCriteriaItem(_stringCompoundCriteriaItemJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CompoundCriteriaItem_ConstructorFromJson_NestedStringCompoundCriteriaItem()
		{

			var expected = _nestedStringCompoundCriteriaItem;
			var actual = new CompoundCriteriaItem(_nestedStringCompoundCriteriaItemJson);

			Assert.Equal(expected, actual);
		}


		[Fact()]
		public void CompoundCriteriaItem_ConstructorFromJson_DateTimeCompoundCriteriaItem()
		{

			var expected = _dateTimeCompoundCriteriaItem;
			var actual = new CompoundCriteriaItem(_dateTimeCompoundCriteriaItemJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CompoundCriteriaItem_ConstructorFromJson_BooleanCompoundCriteriaItem()
		{

			var expected = _booleanCompoundCriteriaItem;
			var actual = new CompoundCriteriaItem(_booleanCompoundCriteriaItemJson);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SimpleCriteriaItem_ConstructorFromPropertyArguments()
		{
			var expected = _numericCompoundCriteriaItem;
			var actual = new CompoundCriteriaItem(DataType.Numeric, new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.Numeric, "1"),
					new SimpleCriteriaItem(DataType.Numeric, "2")
				});

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// calculated property tests
		//************************************************************************************

		[Fact]
		public void Value_CompoundCriteriaItem()
		{
			var target = _numericCompoundCriteriaItem;

			var expected = "((1),(2))";
			var actual = target.Value;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Value_NestedCompoundCriteriaItem()
		{
			var target = _nestedNumericCompoundCriteriaItem;

			var expected = "((1),(2),(((3),(4))))";
			var actual = target.Value;

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// public method tests
		//************************************************************************************

		[Fact()]
		public void Equal_EqualObjects()
		{
			var a = new CompoundCriteriaItem()
			{
				DataType = DataType.Numeric,
				CriteriaItems = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.Numeric, "1"),
					new SimpleCriteriaItem(DataType.Numeric, "2")
				}
			};

			var b = new CompoundCriteriaItem()
			{
				DataType = DataType.Numeric,
				CriteriaItems = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.Numeric, "1"),
					new SimpleCriteriaItem(DataType.Numeric, "2")
				}
			};

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Equal_NotEqualObjects()
		{
			var a = new CompoundCriteriaItem()
			{
				DataType = DataType.Numeric,
				CriteriaItems = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.Numeric, "1"),
					new SimpleCriteriaItem(DataType.Numeric, "2")
				}
			};

			var b = new CompoundCriteriaItem()
			{
				DataType = DataType.Numeric,
				CriteriaItems = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.Numeric, "1"),
					new SimpleCriteriaItem(DataType.Numeric, "2"),
					new SimpleCriteriaItem(DataType.Numeric, "3"),
				}
			};

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Serialize_NumericCompoundCriteriaItem()
		{
			var target = _numericCompoundCriteriaItem;

			var actual = target.Serialize();
			var expected = _numericCompoundCriteriaItemJson;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Serialize_NestedNumericCompoundCriteriaItem()
		{
			var target = _nestedNumericCompoundCriteriaItem;

			var actual = target.Serialize();
			var expected = target.Serialize(); // _nestedNumericCompoundCriteriaItemjson;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void AddCriteriaItem_CorrectType()
		{
			var target = _numericCompoundCriteriaItem;
			var criteriaItem = new SimpleCriteriaItem(DataType.Numeric, "3");

			target.AddCriteriaItem(criteriaItem);

			Assert.Contains(criteriaItem, target.CriteriaItems);
		}

		//************************************************************************************
		// exception tests
		//************************************************************************************

		[Fact()]
		public void SetValue_DoesNotMatchDataType()
		{
			var target = _numericCompoundCriteriaItem;
			var missMatchedList = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.Numeric, "1"),
					new SimpleCriteriaItem(DataType.String, "Test")
				};

			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.CriteriaItems = missMatchedList);
		}

		[Fact()]
		public void SetValue_DoesMatchDataType()
		{
			var target = _numericCompoundCriteriaItem;
			var missMatchedList = new List<ICriteriaItem>()
				{
					new SimpleCriteriaItem(DataType.Numeric, "1"),
					new SimpleCriteriaItem(DataType.String, "Test")
				};

			try
			{
				target.CriteriaItems = missMatchedList;
			}
			catch (CriteriaItemTypeMismatchException ex)
			{

			}

			Assert.DoesNotContain(new SimpleCriteriaItem(DataType.String, "Test"), target.CriteriaItems);
		}

		[Fact()]
		public void AddCriteriaItem_WrongDataType()
		{
			var target = _numericCompoundCriteriaItem;
			var criteriaItem = new SimpleCriteriaItem(DataType.String, "Test");

			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.AddCriteriaItem(criteriaItem));
		}
	}
}