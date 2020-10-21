using Xunit;
using Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.Enums;
using Criteria.CriteriaExceptions;

namespace Criteria.CriteriaUnits.Tests
{
	public class CriteriaUnitSimpleTests
	{

		private Guid _commonGuidA;
		private Guid _commonGuidB;
		private Guid _commonGuidC;

		private string _BasicStringCriteriaUnitSimpleLiteraljson;
		private CriteriaUnitSimple _BasicStringCriteriaUnitSimpleLiteral;
		private string _BasicStringCriteriaUnitSimpleNonLiteraljson;
		private CriteriaUnitSimple _BasicStringCriteriaUnitSimpleNonLiteral;


		public CriteriaUnitSimpleTests()
		{
			_commonGuidA = Guid.NewGuid();
			_commonGuidB = Guid.NewGuid();
			_commonGuidC = Guid.NewGuid();

			_BasicStringCriteriaUnitSimpleLiteral = new CriteriaUnitSimple(_commonGuidA, DataType.String, "Test", true);
			_BasicStringCriteriaUnitSimpleLiteraljson = _BasicStringCriteriaUnitSimpleLiteral.Serialize();

			_BasicStringCriteriaUnitSimpleNonLiteral = new CriteriaUnitSimple(_commonGuidB, DataType.String, "ColumnName", false);
			_BasicStringCriteriaUnitSimpleNonLiteraljson = _BasicStringCriteriaUnitSimpleNonLiteral.Serialize();
		}

		//************************************************************************************
		// constructor tests
		//************************************************************************************

		[Fact()]
		public void CriteriaUnitSimple_ConstructorFromJson()
		{

			var expected = _BasicStringCriteriaUnitSimpleLiteral;
			var actual = new CriteriaUnitSimple(_BasicStringCriteriaUnitSimpleLiteraljson);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void CriteriaUnitSimple_ConstructorFromPropertyArguments()
		{
			var expected = _BasicStringCriteriaUnitSimpleLiteral;
			var actual = new CriteriaUnitSimple(_commonGuidA, DataType.String, "Test", true);

			Assert.Equal(expected, actual);
		}


		//************************************************************************************
		// property tests
		//************************************************************************************

		[Fact()]
		public void CriteriaUnitSimple_Value()
		{
			var target = _BasicStringCriteriaUnitSimpleLiteral;

			var expected = "Test";
			var actual = target.Value;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaUnitSimple_ReturnDataType()
		{
			var target = _BasicStringCriteriaUnitSimpleLiteral;

			var expected = DataType.String;
			var actual = target.ReturnDataType;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaUnitSimple_SQLValueLiteral()
		{
			var target = _BasicStringCriteriaUnitSimpleLiteral;

			var expected = "'Test'";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaUnitSimple_SQLValueNonLiteral()
		{
			var target = _BasicStringCriteriaUnitSimpleNonLiteral;

			var expected = "ColumnName";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaUnitSimple_EnglishValueLiteral()
		{
			var target = _BasicStringCriteriaUnitSimpleLiteral;

			var expected = "\"Test\"";
			var actual = target.EnglishValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaUnitSimple_EnglishValueNonLiteral()
		{
			var target = _BasicStringCriteriaUnitSimpleNonLiteral;

			var expected = "ColumnName";
			var actual = target.EnglishValue;

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// public method tests
		//************************************************************************************

		[Fact()]
		public void Equal_EqualObjects()
		{
			var a = new CriteriaUnitSimple(_commonGuidA, DataType.String, "Test", true);
			var b = new CriteriaUnitSimple(_commonGuidA, DataType.String, "Test", true);

			Assert.Equal(a, b);
		}

		[Fact()]
		public void NotEqual_DifferentValue()
		{
			var a = new CriteriaUnitSimple(_commonGuidA, DataType.String, "Test", true);
			var b = new CriteriaUnitSimple(_commonGuidA, DataType.String, "Test1", true);

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Equal_DifferentGuid()
		{
			var a = new CriteriaUnitSimple(_commonGuidB, DataType.String, "Test", true);
			var b = new CriteriaUnitSimple(_commonGuidA, DataType.String, "Test", true);

			Assert.Equal(a, b);
		}

		[Fact()]
		public void NotEqual_LiteralNonLiteral()
		{
			var a = new CriteriaUnitSimple(_commonGuidA, DataType.String, "Test", true);
			var b = new CriteriaUnitSimple(_commonGuidA, DataType.String, "Test", false);

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void NotEqual_DifferentDataType()
		{
			var a = new CriteriaUnitSimple(_commonGuidA, DataType.Numeric, "1", true);
			var b = new CriteriaUnitSimple(_commonGuidA, DataType.String, "1", true);

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Copy_CreatesAnEqualCriteriaUnit()
		{
			var a = new CriteriaUnitSimple(DataType.String, "Test", true);
			var b = a.Copy();

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Copy_CreatesADeepDistinctCopy()
		{
			var a = new CriteriaUnitSimple(DataType.String, "Test", true);
			var b = (CriteriaUnitSimple)a.Copy();
			b.Value = "Test2";

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Serialize_BasicCriteriaUnitSimple()
		{
			var target = new CriteriaUnitSimple(_commonGuidA, DataType.String, "Test", true);

			var actual = target.Serialize();
			var expected = _BasicStringCriteriaUnitSimpleLiteraljson;

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// exception tests
		//************************************************************************************

		[Fact()]
		public void SetValue_DoesNotMatchDataType()
		{
			var target = new CriteriaUnitSimple(DataType.Numeric, "1", true);

			Assert.Throws<CriteriaUnitTypeMismatchException>(() => target.Value = "Test");
		}

		[Fact()]
		public void SetValue_NumericTypeAllowsStringValueIfNotLiteral()
		{
			var target = new CriteriaUnitSimple(DataType.Numeric, "1", true);
			try
			{
				target.IsValueLiteral = false;
				target.Value = "Test";
			}
			catch (CriteriaUnitTypeMismatchException ex)
			{

			}

			Assert.True(target.Value == "Test");
		}

		[Fact()]
		public void SetValue_NumericTypeDoesNotAllowStringValueIfLiteral()
		{
			var target = new CriteriaUnitSimple(DataType.Numeric, "1", true);
			Assert.Throws<CriteriaUnitTypeMismatchException>(() => target.Value = "Test");
		}

		[Fact()]
		public void SetValue_BooleanTypeDoesNotAllowStringValueIfLiteral()
		{
			var target = new CriteriaUnitSimple(DataType.Boolean, "true", true);
			Assert.Throws<CriteriaUnitTypeMismatchException>(() => target.Value = "Test");
		}

		[Fact()]
		public void SetValue_DoesMatchDataType()
		{
			var target = new CriteriaUnitSimple(DataType.Numeric, "1", true);

			try
			{
				target.Value = "2";
			}
			catch (CriteriaUnitTypeMismatchException ex)
			{

			}

			Assert.True(target.Value == "2");
		}


	}
}