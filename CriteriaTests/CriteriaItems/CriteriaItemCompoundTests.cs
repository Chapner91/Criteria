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
	public class CriteriaItemCompoundTests
	{
		private string _numericCriteriaItemCompoundJson = "{\"$type\":\"Criteria.CriteriaItems.CriteriaItemCompound, Criteria\",\"DataType\":0,\"Value\":\"((1),(2))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":0,\"Value\":\"1\"},{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":0,\"Value\":\"2\"}]}";
		private CriteriaItemCompound _numericCriteriaItemCompound = new CriteriaItemCompound()
		{
			DataType = DataType.Numeric,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1"),
					new CriteriaItemSimple(DataType.Numeric, "2")
				}
		};

		private string _nestedNumericCriteriaItemCompoundJson = "{\"$type\":\"Criteria.CriteriaItems.CriteriaItemCompound, Criteria\",\"DataType\":0,\"Value\":\"(1),(2),((3),(4))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":0,\"Value\":\"1\"},{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":0,\"Value\":\"2\"},{\"$type\":\"Criteria.CriteriaItems.CriteriaItemCompound, Criteria\",\"DataType\":0,\"Value\":\"(3),(4)\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":0,\"Value\":\"3\"},{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":0,\"Value\":\"4\"}]}]}";
		private CriteriaItemCompound _nestedNumericCriteriaItemCompound = new CriteriaItemCompound()
		{
			DataType = DataType.Numeric,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1"),
					new CriteriaItemSimple(DataType.Numeric, "2"),
					new CriteriaItemCompound()
					{
						DataType = DataType.Numeric,
						CriteriaItems = new List<ICriteriaItem>()
						{
							new CriteriaItemSimple(DataType.Numeric, "3"),
							new CriteriaItemSimple(DataType.Numeric, "4")
						}
					}
				}
		};

		private string _stringCriteriaItemCompoundJson = "{\"$type\":\"Criteria.CriteriaItems.CriteriaItemCompound, Criteria\",\"DataType\":1,\"Value\":\"((Test1),(Test2))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":1,\"Value\":\"Test1\"},{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":1,\"Value\":\"Test2\"}]}";
		private CriteriaItemCompound _stringCriteriaItemCompound = new CriteriaItemCompound()
		{
			DataType = DataType.String,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.String, "Test1"),
					new CriteriaItemSimple(DataType.String, "Test2")
				}
		};


		private string _nestedStringCriteriaItemCompoundJson = "{\"$type\":\"Criteria.CriteriaItems.CriteriaItemCompound, Criteria\",\"DataType\":1,\"Value\":\"((Test1),(Test2)),(((Test3),(Test4)))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":1,\"Value\":\"Test1\"},{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":1,\"Value\":\"Test2\"},{\"$type\":\"Criteria.CriteriaItems.CriteriaItemCompound, Criteria\",\"DataType\":1,\"Value\":\"((Test3),(Test4))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":1,\"Value\":\"Test3\"},{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":1,\"Value\":\"Test4\"}]}]}";
		private CriteriaItemCompound _nestedStringCriteriaItemCompound = new CriteriaItemCompound()
		{
			DataType = DataType.String,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.String, "Test1"),
					new CriteriaItemSimple(DataType.String, "Test2"),
					new CriteriaItemCompound()
						{
							DataType = DataType.String,
							CriteriaItems = new List<ICriteriaItem>()
								{
									new CriteriaItemSimple(DataType.String, "Test3"),
									new CriteriaItemSimple(DataType.String, "Test4")
								}
						}
				}
		};

		private string _dateTimeCriteriaItemCompoundJson = "{\"$type\":\"Criteria.CriteriaItems.CriteriaItemCompound, Criteria\",\"DataType\":2,\"Value\":\"((2020-01-01),(2020-01-02))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":2,\"Value\":\"2020-01-01\"},{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":2,\"Value\":\"2020-01-02\"}]}";
		private CriteriaItemCompound _dateTimeCriteriaItemCompound = new CriteriaItemCompound()
		{
			DataType = DataType.DateTime,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.DateTime, "2020-01-01"),
					new CriteriaItemSimple(DataType.DateTime, "2020-01-02")
				}
		};

		private string _booleanCriteriaItemCompoundJson = "{\"$type\":\"Criteria.CriteriaItems.CriteriaItemCompound, Criteria\",\"DataType\":3,\"Value\":\"((true),(false))\",\"CriteriaItems\":[{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":3,\"Value\":\"true\"},{\"$type\":\"Criteria.CriteriaItems.CriteriaItemSimple, Criteria\",\"DataType\":3,\"Value\":\"false\"}]}";
		private CriteriaItemCompound _booleanCriteriaItemCompound = new CriteriaItemCompound()
		{
			DataType = DataType.Boolean,
			CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Boolean, "true"),
					new CriteriaItemSimple(DataType.Boolean, "false")
				}
		};

		//************************************************************************************
		// constructor tests
		//************************************************************************************

		[Fact()]
		public void CriteriaItemCompound_ConstructorFromJson_NumericCriteriaItemCompound()
		{

			var expected = _numericCriteriaItemCompound;
			var actual = new CriteriaItemCompound(_numericCriteriaItemCompoundJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemCompound_ConstructorFromJson_NestedNumericCriteriaItemCompound()
		{

			var expected = _nestedNumericCriteriaItemCompound;
			var actual = new CriteriaItemCompound(_nestedNumericCriteriaItemCompoundJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemCompound_ConstructorFromJson_StringCriteriaItemCompound()
		{

			var expected = _stringCriteriaItemCompound;
			var actual = new CriteriaItemCompound(_stringCriteriaItemCompoundJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemCompound_ConstructorFromJson_NestedStringCriteriaItemCompound()
		{

			var expected = _nestedStringCriteriaItemCompound;
			var actual = new CriteriaItemCompound(_nestedStringCriteriaItemCompoundJson);

			Assert.Equal(expected, actual);
		}


		[Fact()]
		public void CriteriaItemCompound_ConstructorFromJson_DateTimeCriteriaItemCompound()
		{

			var expected = _dateTimeCriteriaItemCompound;
			var actual = new CriteriaItemCompound(_dateTimeCriteriaItemCompoundJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemCompound_ConstructorFromJson_BooleanCriteriaItemCompound()
		{

			var expected = _booleanCriteriaItemCompound;
			var actual = new CriteriaItemCompound(_booleanCriteriaItemCompoundJson);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void CriteriaItemSimple_ConstructorFromPropertyArguments()
		{
			var expected = _numericCriteriaItemCompound;
			var actual = new CriteriaItemCompound(DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1"),
					new CriteriaItemSimple(DataType.Numeric, "2")
				});

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// calculated property tests
		//************************************************************************************

		[Fact]
		public void Value_CriteriaItemCompound()
		{
			var target = _numericCriteriaItemCompound;

			var expected = "((1),(2))";
			var actual = target.Value;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Value_NestedCriteriaItemCompound()
		{
			var target = _nestedNumericCriteriaItemCompound;

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
			var a = new CriteriaItemCompound()
			{
				DataType = DataType.Numeric,
				CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1"),
					new CriteriaItemSimple(DataType.Numeric, "2")
				}
			};

			var b = new CriteriaItemCompound()
			{
				DataType = DataType.Numeric,
				CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1"),
					new CriteriaItemSimple(DataType.Numeric, "2")
				}
			};

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Equal_NotEqualObjects()
		{
			var a = new CriteriaItemCompound()
			{
				DataType = DataType.Numeric,
				CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1"),
					new CriteriaItemSimple(DataType.Numeric, "2")
				}
			};

			var b = new CriteriaItemCompound()
			{
				DataType = DataType.Numeric,
				CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1"),
					new CriteriaItemSimple(DataType.Numeric, "2"),
					new CriteriaItemSimple(DataType.Numeric, "3"),
				}
			};

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Serialize_NumericCriteriaItemCompound()
		{
			var target = _numericCriteriaItemCompound;

			var actual = target.Serialize();
			var expected = _numericCriteriaItemCompoundJson;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Serialize_NestedNumericCriteriaItemCompound()
		{
			var target = _nestedNumericCriteriaItemCompound;

			var actual = target.Serialize();
			var expected = target.Serialize(); // _nestedNumericCriteriaItemCompoundjson;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void AddCriteriaItem_CorrectType()
		{
			var target = _numericCriteriaItemCompound;
			var criteriaItem = new CriteriaItemSimple(DataType.Numeric, "3");

			target.AddCriteriaItem(criteriaItem);

			Assert.Contains(criteriaItem, target.CriteriaItems);
		}

		//************************************************************************************
		// exception tests
		//************************************************************************************

		[Fact()]
		public void SetValue_DoesNotMatchDataType()
		{
			var target = _numericCriteriaItemCompound;
			var missMatchedList = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1"),
					new CriteriaItemSimple(DataType.String, "Test")
				};

			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.CriteriaItems = missMatchedList);
		}

		[Fact()]
		public void SetValue_DoesMatchDataType()
		{
			var target = _numericCriteriaItemCompound;
			var missMatchedList = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1"),
					new CriteriaItemSimple(DataType.String, "Test")
				};

			try
			{
				target.CriteriaItems = missMatchedList;
			}
			catch (CriteriaItemTypeMismatchException ex)
			{

			}

			Assert.DoesNotContain(new CriteriaItemSimple(DataType.String, "Test"), target.CriteriaItems);
		}

		[Fact()]
		public void AddCriteriaItem_WrongDataType()
		{
			var target = _numericCriteriaItemCompound;
			var criteriaItem = new CriteriaItemSimple(DataType.String, "Test");

			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.AddCriteriaItem(criteriaItem));
		}
	}
}