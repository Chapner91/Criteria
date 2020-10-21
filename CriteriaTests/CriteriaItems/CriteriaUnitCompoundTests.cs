using Xunit;
using Criteria.CriteriaUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.Enums;
using Criteria.CriteriaExceptions;

namespace Criteria.CriteriaUnits.Tests
{
	public class CriteriaUnitCompoundTests
	{

		private Guid _CommonGuidA;
		private Guid _CommonGuidB;
		private Guid _CommonGuidC;

		private string _numericCriteriaUnitCompoundJson;
		private CriteriaUnitCompound _numericCriteriaUnitCompound;
		private string _nestedNumericCriteriaUnitCompoundJson; 
		private CriteriaUnitCompound _nestedNumericCriteriaUnitCompound;
		private string _stringCriteriaUnitCompoundJson;
		private CriteriaUnitCompound _stringCriteriaUnitCompound;
		private string _nestedStringCriteriaUnitCompoundJson;
		private CriteriaUnitCompound _nestedStringCriteriaUnitCompound;
		private string _dateTimeCriteriaUnitCompoundJson;
		private CriteriaUnitCompound _dateTimeCriteriaUnitCompound;
		private string _booleanCriteriaUnitCompoundJson;
		private CriteriaUnitCompound _booleanCriteriaUnitCompound;

		public CriteriaUnitCompoundTests()
		{
			_CommonGuidA = Guid.NewGuid();
			_CommonGuidB = Guid.NewGuid();
			_CommonGuidC = Guid.NewGuid();

			_numericCriteriaUnitCompound = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "1", true),
					new CriteriaUnitSimple(DataType.Numeric, "2", true)
				});
			_numericCriteriaUnitCompoundJson = _numericCriteriaUnitCompound.Serialize();

			_nestedNumericCriteriaUnitCompound = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "1", true),
					new CriteriaUnitSimple(DataType.Numeric, "2", true),
					new CriteriaUnitCompound()
					{
						ReturnDataType = DataType.Numeric,
						CriteriaUnits = new List<ICriteriaUnit>()
						{
							new CriteriaUnitSimple(DataType.Numeric, "3", true),
							new CriteriaUnitSimple(DataType.Numeric, "4", true)
						}
					}
				});
			_nestedNumericCriteriaUnitCompoundJson = _nestedNumericCriteriaUnitCompound.Serialize();

			_stringCriteriaUnitCompound = new CriteriaUnitCompound(_CommonGuidA, DataType.String, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.String, "Test1", true),
					new CriteriaUnitSimple(DataType.String, "Test2", true)
				});
			_stringCriteriaUnitCompoundJson = _stringCriteriaUnitCompound.Serialize();

			_nestedStringCriteriaUnitCompound = new CriteriaUnitCompound(_CommonGuidA, DataType.String, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.String, "Test1", true),
					new CriteriaUnitSimple(DataType.String, "Test2", true),
					new CriteriaUnitCompound()
						{
							ReturnDataType = DataType.String,
							CriteriaUnits = new List<ICriteriaUnit>()
								{
									new CriteriaUnitSimple(DataType.String, "Test3", true),
									new CriteriaUnitSimple(DataType.String, "Test4", true)
								}
						}
				});
			_nestedStringCriteriaUnitCompoundJson = _nestedStringCriteriaUnitCompound.Serialize();

			_dateTimeCriteriaUnitCompound = new CriteriaUnitCompound(_CommonGuidA, DataType.DateTime, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.DateTime, "2020-01-01", true),
					new CriteriaUnitSimple(DataType.DateTime, "2020-01-02", true)
				});
			_dateTimeCriteriaUnitCompoundJson = _dateTimeCriteriaUnitCompound.Serialize();

			_booleanCriteriaUnitCompound = new CriteriaUnitCompound(_CommonGuidA, DataType.Boolean, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Boolean, "true", true),
					new CriteriaUnitSimple(DataType.Boolean, "false", true)
				});
			_booleanCriteriaUnitCompoundJson = _booleanCriteriaUnitCompound.Serialize();
		}

		//************************************************************************************
		// constructor tests
		//************************************************************************************

		[Fact()]
		public void CriteriaUnitCompound_ConstructorFromJson_NumericCriteriaUnitCompound()
		{

			var expected = _numericCriteriaUnitCompound;
			var actual = new CriteriaUnitCompound(_numericCriteriaUnitCompoundJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaUnitCompound_ConstructorFromJson_NestedNumericCriteriaUnitCompound()
		{

			var expected = _nestedNumericCriteriaUnitCompound;
			var actual = new CriteriaUnitCompound(_nestedNumericCriteriaUnitCompoundJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaUnitCompound_ConstructorFromJson_StringCriteriaUnitCompound()
		{

			var expected = _stringCriteriaUnitCompound;
			var actual = new CriteriaUnitCompound(_stringCriteriaUnitCompoundJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaUnitCompound_ConstructorFromJson_NestedStringCriteriaUnitCompound()
		{

			var expected = _nestedStringCriteriaUnitCompound;
			var actual = new CriteriaUnitCompound(_nestedStringCriteriaUnitCompoundJson);

			Assert.Equal(expected, actual);
		}


		[Fact()]
		public void CriteriaUnitCompound_ConstructorFromJson_DateTimeCriteriaUnitCompound()
		{

			var expected = _dateTimeCriteriaUnitCompound;
			var actual = new CriteriaUnitCompound(_dateTimeCriteriaUnitCompoundJson);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaUnitCompound_ConstructorFromJson_BooleanCriteriaUnitCompound()
		{

			var expected = _booleanCriteriaUnitCompound;
			var actual = new CriteriaUnitCompound(_booleanCriteriaUnitCompoundJson);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void CriteriaUnitSimple_ConstructorFromPropertyArguments()
		{
			var expected = _numericCriteriaUnitCompound;
			expected.CriteriaUnits = new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "2", true)
				};

			var actual = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "2", true)
				});

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// calculated property tests
		//************************************************************************************

		//[Fact]
		//public void Value_CriteriaUnitCompound()
		//{
		//	var target = _numericCriteriaUnitCompound;

		//	var expected = "(1,2)";
		//	var actual = target.Value;

		//	Assert.Equal(expected, actual);
		//}

		//[Fact]
		//public void Value_NestedCriteriaUnitCompound()
		//{
		//	var target = _nestedNumericCriteriaUnitCompound;

		//	var expected = "(1,2,(3,4))";
		//	var actual = target.Value;

		//	Assert.Equal(expected, actual);
		//}

		[Fact]
		public void SQLValue_NestedCriteriaUnitCompoundAllNonstringLiterals()
		{
			var target = _nestedNumericCriteriaUnitCompound;

			var expected = "(1,2,(3,4))";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SQLValue_NestedCriteriaUnitCompoundAllNonstringNonLiterals()
		{
			var target = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "ColumnA", false),
					new CriteriaUnitSimple(DataType.Numeric, "ColumnB", false),
					new CriteriaUnitCompound()
					{
						ReturnDataType = DataType.Numeric,
						CriteriaUnits = new List<ICriteriaUnit>()
						{
							new CriteriaUnitSimple(DataType.Numeric, "ColumnC", false),
							new CriteriaUnitSimple(DataType.Numeric, "ColumnD", false)
						}
					}
				});

			var expected = "(ColumnA,ColumnB,(ColumnC,ColumnD))";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SQLValue_NestedCriteriaUnitCompoundNonstringLiteralsAndNonstringNonLiterals()
		{
			var target = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "ColumnA", false),
					new CriteriaUnitSimple(DataType.Numeric, "ColumnB", false),
					new CriteriaUnitCompound()
					{
						ReturnDataType = DataType.Numeric,
						CriteriaUnits = new List<ICriteriaUnit>()
						{
							new CriteriaUnitSimple(DataType.Numeric, "1", true),
							new CriteriaUnitSimple(DataType.Numeric, "2", true)
						}
					}
				});

			var expected = "(ColumnA,ColumnB,(1,2))";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SQLValue_NestedCriteriaUnitCompoundStringLiteralsAndStringNonLiterals()
		{
			var target = new CriteriaUnitCompound(_CommonGuidA, DataType.String, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.String, "ColumnA", false),
					new CriteriaUnitSimple(DataType.String, "ColumnB", false),
					new CriteriaUnitCompound()
					{
						ReturnDataType = DataType.String,
						CriteriaUnits = new List<ICriteriaUnit>()
						{
							new CriteriaUnitSimple(DataType.String, "TestA", true),
							new CriteriaUnitSimple(DataType.String, "TestB", true)
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
			var a = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "2", true)
				});

			var b = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "2", true)
				});

			Assert.Equal(a, b);
		}

		[Fact()]
		public void NotEqual_CriteriaUnits()
		{
			var a = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "1", true),
					new CriteriaUnitSimple(DataType.Numeric, "2", true)
				});

			var b = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "1", true),
					new CriteriaUnitSimple(DataType.Numeric, "2", true),
					new CriteriaUnitSimple(DataType.Numeric, "3", true),
				});

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Equal_DifferentCriteriaUnitID()
		{
			var a = new CriteriaUnitCompound(DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "1", true),
					new CriteriaUnitSimple(DataType.Numeric, "2", true)
				});

			var b = new CriteriaUnitCompound(DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "1", true),
					new CriteriaUnitSimple(DataType.Numeric, "2", true),
				});

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Serialize_NumericCriteriaUnitCompound()
		{
			var target = _numericCriteriaUnitCompound;

			var actual = target.Serialize();
			var expected = _numericCriteriaUnitCompoundJson;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Serialize_NestedNumericCriteriaUnitCompound()
		{
			var target = _nestedNumericCriteriaUnitCompound;

			var actual = target.Serialize();
			var expected = target.Serialize(); // _nestedNumericCriteriaUnitCompoundjson;

			Assert.Equal(expected, actual);
		}


		[Fact()]
		public void AddCriteriaUnit_CorrectType()
		{
			var target = _numericCriteriaUnitCompound;
			var criteriaUnit = new CriteriaUnitSimple(DataType.Numeric, "3", true);

			target.AddCriteriaUnit(criteriaUnit);

			Assert.Contains(criteriaUnit, target.CriteriaUnits);
		}

		[Fact()]
		public void AddCriteriaUnit_AddUnitAtIndex0()
		{

			var expected = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			var actual = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.AddCriteriaUnit(0, new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true));

			Assert.True(
				actual.CriteriaUnits.Count() == expected.CriteriaUnits.Count() &&
				actual.CriteriaUnits.SequenceEqual(expected.CriteriaUnits)
				);
		}

		[Fact()]
		public void AddCriteriaUnit_AddUnitAtIndex1()
		{

			var expected = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			var actual = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.AddCriteriaUnit(1, new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true));

			Assert.True(
				actual.CriteriaUnits.Count() == expected.CriteriaUnits.Count() &&
				actual.CriteriaUnits.SequenceEqual(expected.CriteriaUnits)
				);
		}

		[Fact()]
		public void RemoveCriteriaUnit_ByCriteriaUnitID()
		{
			var expected = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
				});

			var actual = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaUnit(_CommonGuidB);

			Assert.True(
				actual.CriteriaUnits.Count() == expected.CriteriaUnits.Count() &&
				actual.CriteriaUnits.SequenceEqual(expected.CriteriaUnits)
				);
		}

		[Fact()]
		public void RemoveCriteriaUnit_NoMatchingCriteriaUnitID()
		{
			var expected = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			var actual = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaUnit(Guid.NewGuid());

			Assert.True(
				actual.CriteriaUnits.Count() == expected.CriteriaUnits.Count() &&
				actual.CriteriaUnits.SequenceEqual(expected.CriteriaUnits)
				);
		}

		[Fact()]
		public void RemoveCriteriaUnit_ByCriteriaUnitID_MultipleMatch()
		{
			var expected = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
				});

			var actual = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaUnit(_CommonGuidB);

			Assert.True(
				actual.CriteriaUnits.Count() == expected.CriteriaUnits.Count() &&
				actual.CriteriaUnits.SequenceEqual(expected.CriteriaUnits)
				);
		}

		[Fact()]
		public void RemoveCriteriaUnit_ByCriteriaUnit()
		{
			var expected = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
				});

			var actual = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaUnit(new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true));

			Assert.True(
				actual.CriteriaUnits.Count() == expected.CriteriaUnits.Count() &&
				actual.CriteriaUnits.SequenceEqual(expected.CriteriaUnits)
				);
		}

		[Fact()]
		public void RemoveCriteriaUnit_ByCriteriaUnit_MultipleMatch()
		{
			var expected = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
				});

			var actual = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaUnit(new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true));

			Assert.True(
				actual.CriteriaUnits.Count() == expected.CriteriaUnits.Count() &&
				actual.CriteriaUnits.SequenceEqual(expected.CriteriaUnits)
				);
		}

		[Fact()]
		public void RemoveCriteriaUnit_NoMatchingCriteriaUnit()
		{
			var expected = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			var actual = new CriteriaUnitCompound(_CommonGuidA, DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(_CommonGuidC, DataType.Numeric, "0", true),
					new CriteriaUnitSimple(_CommonGuidA, DataType.Numeric, "1", true),
					new CriteriaUnitSimple(_CommonGuidB, DataType.Numeric, "2", true)
				});

			actual.RemoveCriteriaUnit(new CriteriaUnitSimple(Guid.NewGuid(), DataType.Numeric, "4", true));

			Assert.True(
				actual.CriteriaUnits.Count() == expected.CriteriaUnits.Count() &&
				actual.CriteriaUnits.SequenceEqual(expected.CriteriaUnits)
				);
		}
		//************************************************************************************
		// exception tests
		//************************************************************************************

		[Fact()]
		public void SetValue_DoesNotMatchDataType()
		{
			var target = _numericCriteriaUnitCompound;
			var missMatchedList = new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "1", true),
					new CriteriaUnitSimple(DataType.String, "Test", true)
				};

			Assert.Throws<CriteriaUnitTypeMismatchException>(() => target.CriteriaUnits = missMatchedList);
		}

		[Fact()]
		public void SetValue_DoesMatchDataType()
		{
			var target = _numericCriteriaUnitCompound;
			var missMatchedList = new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "1", true),
					new CriteriaUnitSimple(DataType.String, "Test", true)
				};

			try
			{
				target.CriteriaUnits = missMatchedList;
			}
			catch (CriteriaUnitTypeMismatchException ex)
			{

			}

			Assert.DoesNotContain(new CriteriaUnitSimple(DataType.String, "Test", true), target.CriteriaUnits);
		}

		[Fact()]
		public void AddCriteriaUnit_WrongDataType()
		{
			var target = _numericCriteriaUnitCompound;
			var criteriaUnit = new CriteriaUnitSimple(DataType.String, "Test", true);

			Assert.Throws<CriteriaUnitTypeMismatchException>(() => target.AddCriteriaUnit(criteriaUnit));
		}

		[Fact()]
		public void Copy_CreatesAnEqualCriteriaUnit()
		{
			var a = new CriteriaUnitCompound(DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "1", true),
					new CriteriaUnitSimple(DataType.Numeric, "2", true)
				});
			var b = a.Copy();

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Copy_CreatesADeepDistinctCopy()
		{
			var a = new CriteriaUnitCompound(DataType.Numeric, new List<ICriteriaUnit>()
				{
					new CriteriaUnitSimple(DataType.Numeric, "1", true),
					new CriteriaUnitSimple(DataType.Numeric, "2", true)
				});
			var b = (CriteriaUnitCompound)a.Copy();
			b.AddCriteriaUnit(new CriteriaUnitSimple(DataType.Numeric, "3", true));

			Assert.NotEqual(a, b);
		}
	}
}