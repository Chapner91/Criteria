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

		private Guid _CommonGuidA;
		private Guid _CommonGuidB;
		private Guid _CommonGuidC;

		private string _numericCriteriaItemCompoundJson;
		private CriteriaItemCompound _numericCriteriaItemCompound;
		private string _nestedNumericCriteriaItemCompoundJson; 
		private CriteriaItemCompound _nestedNumericCriteriaItemCompound;
		private string _stringCriteriaItemCompoundJson;
		private CriteriaItemCompound _stringCriteriaItemCompound;
		private string _nestedStringCriteriaItemCompoundJson;
		private CriteriaItemCompound _nestedStringCriteriaItemCompound;
		private string _dateTimeCriteriaItemCompoundJson;
		private CriteriaItemCompound _dateTimeCriteriaItemCompound;
		private string _booleanCriteriaItemCompoundJson;
		private CriteriaItemCompound _booleanCriteriaItemCompound;

		public CriteriaItemCompoundTests()
		{
			_CommonGuidA = Guid.NewGuid();
			_CommonGuidB = Guid.NewGuid();
			_CommonGuidC = Guid.NewGuid();

			_numericCriteriaItemCompound = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1", true),
					new CriteriaItemSimple(DataType.Numeric, "2", true)
				});
			_numericCriteriaItemCompoundJson = _numericCriteriaItemCompound.Serialize();

			_nestedNumericCriteriaItemCompound = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1", true),
					new CriteriaItemSimple(DataType.Numeric, "2", true),
					new CriteriaItemCompound()
					{
						ReturnDataType = DataType.Numeric,
						CriteriaItems = new List<ICriteriaItem>()
						{
							new CriteriaItemSimple(DataType.Numeric, "3", true),
							new CriteriaItemSimple(DataType.Numeric, "4", true)
						}
					}
				});
			_nestedNumericCriteriaItemCompoundJson = _nestedNumericCriteriaItemCompound.Serialize();

			_stringCriteriaItemCompound = new CriteriaItemCompound(_CommonGuidA, DataType.String, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.String, "Test1", true),
					new CriteriaItemSimple(DataType.String, "Test2", true)
				});
			_stringCriteriaItemCompoundJson = _stringCriteriaItemCompound.Serialize();

			_nestedStringCriteriaItemCompound = new CriteriaItemCompound(_CommonGuidA, DataType.String, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.String, "Test1", true),
					new CriteriaItemSimple(DataType.String, "Test2", true),
					new CriteriaItemCompound()
						{
							ReturnDataType = DataType.String,
							CriteriaItems = new List<ICriteriaItem>()
								{
									new CriteriaItemSimple(DataType.String, "Test3", true),
									new CriteriaItemSimple(DataType.String, "Test4", true)
								}
						}
				});
			_nestedStringCriteriaItemCompoundJson = _nestedStringCriteriaItemCompound.Serialize();

			_dateTimeCriteriaItemCompound = new CriteriaItemCompound(_CommonGuidA, DataType.DateTime, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.DateTime, "2020-01-01", true),
					new CriteriaItemSimple(DataType.DateTime, "2020-01-02", true)
				});
			_dateTimeCriteriaItemCompoundJson = _dateTimeCriteriaItemCompound.Serialize();

			_booleanCriteriaItemCompound = new CriteriaItemCompound(_CommonGuidA, DataType.Boolean, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Boolean, "true", true),
					new CriteriaItemSimple(DataType.Boolean, "false", true)
				});
			_booleanCriteriaItemCompoundJson = _booleanCriteriaItemCompound.Serialize();
		}

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
			expected.CriteriaItems = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "2", true)
				};

			var actual = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "2", true)
				});

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// calculated property tests
		//************************************************************************************

		//[Fact]
		//public void Value_CriteriaItemCompound()
		//{
		//	var target = _numericCriteriaItemCompound;

		//	var expected = "(1,2)";
		//	var actual = target.Value;

		//	Assert.Equal(expected, actual);
		//}

		//[Fact]
		//public void Value_NestedCriteriaItemCompound()
		//{
		//	var target = _nestedNumericCriteriaItemCompound;

		//	var expected = "(1,2,(3,4))";
		//	var actual = target.Value;

		//	Assert.Equal(expected, actual);
		//}

		[Fact]
		public void SQLValue_NestedCriteriaItemCompoundAllNonstringLiterals()
		{
			var target = _nestedNumericCriteriaItemCompound;

			var expected = "(1,2,(3,4))";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SQLValue_NestedCriteriaItemCompoundAllNonstringNonLiterals()
		{
			var target = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "ColumnA", false),
					new CriteriaItemSimple(DataType.Numeric, "ColumnB", false),
					new CriteriaItemCompound()
					{
						ReturnDataType = DataType.Numeric,
						CriteriaItems = new List<ICriteriaItem>()
						{
							new CriteriaItemSimple(DataType.Numeric, "ColumnC", false),
							new CriteriaItemSimple(DataType.Numeric, "ColumnD", false)
						}
					}
				});

			var expected = "(ColumnA,ColumnB,(ColumnC,ColumnD))";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SQLValue_NestedCriteriaItemCompoundNonstringLiteralsAndNonstringNonLiterals()
		{
			var target = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "ColumnA", false),
					new CriteriaItemSimple(DataType.Numeric, "ColumnB", false),
					new CriteriaItemCompound()
					{
						ReturnDataType = DataType.Numeric,
						CriteriaItems = new List<ICriteriaItem>()
						{
							new CriteriaItemSimple(DataType.Numeric, "1", true),
							new CriteriaItemSimple(DataType.Numeric, "2", true)
						}
					}
				});

			var expected = "(ColumnA,ColumnB,(1,2))";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SQLValue_NestedCriteriaItemCompoundStringLiteralsAndStringNonLiterals()
		{
			var target = new CriteriaItemCompound(_CommonGuidA, DataType.String, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.String, "ColumnA", false),
					new CriteriaItemSimple(DataType.String, "ColumnB", false),
					new CriteriaItemCompound()
					{
						ReturnDataType = DataType.String,
						CriteriaItems = new List<ICriteriaItem>()
						{
							new CriteriaItemSimple(DataType.String, "TestA", true),
							new CriteriaItemSimple(DataType.String, "TestB", true)
						}
					}
				});

			var expected = "(ColumnA,ColumnB,('TestA','TestB'))";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}
		//************************************************************************************
		// public method tests
		//************************************************************************************

		[Fact()]
		public void Equal_EqualObjects()
		{
			var a = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "2", true)
				});

			var b = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "2", true)
				});

			Assert.Equal(a, b);
		}

		[Fact()]
		public void NotEqual_CriteriaItems()
		{
			var a = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1", true),
					new CriteriaItemSimple(DataType.Numeric, "2", true)
				});

			var b = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1", true),
					new CriteriaItemSimple(DataType.Numeric, "2", true),
					new CriteriaItemSimple(DataType.Numeric, "3", true),
				});

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Equal_DifferentCriteriaItemID()
		{
			var a = new CriteriaItemCompound(DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1", true),
					new CriteriaItemSimple(DataType.Numeric, "2", true)
				});

			var b = new CriteriaItemCompound(DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1", true),
					new CriteriaItemSimple(DataType.Numeric, "2", true),
				});

			Assert.Equal(a, b);
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
			var criteriaItem = new CriteriaItemSimple(DataType.Numeric, "3", true);

			target.AddCriteriaItem(criteriaItem);

			Assert.Contains(criteriaItem, target.CriteriaItems);
		}

		[Fact()]
		public void AddCriteriaItem_AddItemAtIndex0()
		{

			var expected = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			var actual = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.AddCriteriaItem(0, new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true));

			Assert.True(
				actual.CriteriaItems.Count() == expected.CriteriaItems.Count() &&
				actual.CriteriaItems.SequenceEqual(expected.CriteriaItems)
				);
		}

		[Fact()]
		public void AddCriteriaItem_AddItemAtIndex1()
		{

			var expected = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			var actual = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.AddCriteriaItem(1, new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true));

			Assert.True(
				actual.CriteriaItems.Count() == expected.CriteriaItems.Count() &&
				actual.CriteriaItems.SequenceEqual(expected.CriteriaItems)
				);
		}

		[Fact()]
		public void RemoveCriteriaItem_ByCriteriaItemID()
		{
			var expected = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
				});

			var actual = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaItem(_CommonGuidB);

			Assert.True(
				actual.CriteriaItems.Count() == expected.CriteriaItems.Count() &&
				actual.CriteriaItems.SequenceEqual(expected.CriteriaItems)
				);
		}

		[Fact()]
		public void RemoveCriteriaItem_NoMatchingCriteriaItemID()
		{
			var expected = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			var actual = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaItem(Guid.NewGuid());

			Assert.True(
				actual.CriteriaItems.Count() == expected.CriteriaItems.Count() &&
				actual.CriteriaItems.SequenceEqual(expected.CriteriaItems)
				);
		}

		[Fact()]
		public void RemoveCriteriaItem_ByCriteriaItemID_MultipleMatch()
		{
			var expected = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
				});

			var actual = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaItem(_CommonGuidB);

			Assert.True(
				actual.CriteriaItems.Count() == expected.CriteriaItems.Count() &&
				actual.CriteriaItems.SequenceEqual(expected.CriteriaItems)
				);
		}

		[Fact()]
		public void RemoveCriteriaItem_ByCriteriaItem()
		{
			var expected = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
				});

			var actual = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaItem(new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true));

			Assert.True(
				actual.CriteriaItems.Count() == expected.CriteriaItems.Count() &&
				actual.CriteriaItems.SequenceEqual(expected.CriteriaItems)
				);
		}

		[Fact()]
		public void RemoveCriteriaItem_ByCriteriaItem_MultipleMatch()
		{
			var expected = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
				});

			var actual = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaItem(new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true));

			Assert.True(
				actual.CriteriaItems.Count() == expected.CriteriaItems.Count() &&
				actual.CriteriaItems.SequenceEqual(expected.CriteriaItems)
				);
		}

		[Fact()]
		public void RemoveCriteriaItem_NoMatchingCriteriaItem()
		{
			var expected = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			var actual = new CriteriaItemCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaItemSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaItemSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaItem(new CriteriaItemSimple(Guid.NewGuid(), DataType.Numeric, "4", true));

			Assert.True(
				actual.CriteriaItems.Count() == expected.CriteriaItems.Count() &&
				actual.CriteriaItems.SequenceEqual(expected.CriteriaItems)
				);
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
					new CriteriaItemSimple(DataType.Numeric, "1", true),
					new CriteriaItemSimple(DataType.String, "Test", true)
				};

			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.CriteriaItems = missMatchedList);
		}

		[Fact()]
		public void SetValue_DoesMatchDataType()
		{
			var target = _numericCriteriaItemCompound;
			var missMatchedList = new List<ICriteriaItem>()
				{
					new CriteriaItemSimple(DataType.Numeric, "1", true),
					new CriteriaItemSimple(DataType.String, "Test", true)
				};

			try
			{
				target.CriteriaItems = missMatchedList;
			}
			catch (CriteriaItemTypeMismatchException ex)
			{

			}

			Assert.DoesNotContain(new CriteriaItemSimple(DataType.String, "Test", true), target.CriteriaItems);
		}

		[Fact()]
		public void AddCriteriaItem_WrongDataType()
		{
			var target = _numericCriteriaItemCompound;
			var criteriaItem = new CriteriaItemSimple(DataType.String, "Test", true);

			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.AddCriteriaItem(criteriaItem));
		}
	}
}