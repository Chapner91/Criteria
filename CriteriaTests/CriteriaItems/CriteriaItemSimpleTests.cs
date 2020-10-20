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

		private Guid _commonGuidA;
		private Guid _commonGuidB;
		private Guid _commonGuidC;

		private string _BasicStringCriteriaItemSimpleLiteraljson;
		private CriteriaItemSimple _BasicStringCriteriaItemSimpleLiteral;
		private string _BasicStringCriteriaItemSimpleNonLiteraljson;
		private CriteriaItemSimple _BasicStringCriteriaItemSimpleNonLiteral;


		public CriteriaItemSimpleTests()
		{
			_commonGuidA = Guid.NewGuid();
			_commonGuidB = Guid.NewGuid();
			_commonGuidC = Guid.NewGuid();

			_BasicStringCriteriaItemSimpleLiteral = new CriteriaItemSimple(_commonGuidA, DataType.String, "Test", true);
			_BasicStringCriteriaItemSimpleLiteraljson = _BasicStringCriteriaItemSimpleLiteral.Serialize();

			_BasicStringCriteriaItemSimpleNonLiteral = new CriteriaItemSimple(_commonGuidB, DataType.String, "ColumnName", false);
			_BasicStringCriteriaItemSimpleNonLiteraljson = _BasicStringCriteriaItemSimpleNonLiteral.Serialize();
		}

		//************************************************************************************
		// constructor tests
		//************************************************************************************

		[Fact()]
		public void CriteriaItemSimple_ConstructorFromJson()
		{

			var expected = _BasicStringCriteriaItemSimpleLiteral;
			var actual = new CriteriaItemSimple(_BasicStringCriteriaItemSimpleLiteraljson);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void CriteriaItemSimple_ConstructorFromPropertyArguments()
		{
			var expected = _BasicStringCriteriaItemSimpleLiteral;
			var actual = new CriteriaItemSimple(_commonGuidA, DataType.String, "Test", true);

			Assert.Equal(expected, actual);
		}


		//************************************************************************************
		// property tests
		//************************************************************************************

		[Fact()]
		public void CriteriaItemSimple_Value()
		{
			var target = _BasicStringCriteriaItemSimpleLiteral;

			var expected = "Test";
			var actual = target.Value;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemSimple_ReturnDataType()
		{
			var target = _BasicStringCriteriaItemSimpleLiteral;

			var expected = DataType.String;
			var actual = target.ReturnDataType;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemSimple_SQLValueLiteral()
		{
			var target = _BasicStringCriteriaItemSimpleLiteral;

			var expected = "'Test'";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemSimple_SQLValueNonLiteral()
		{
			var target = _BasicStringCriteriaItemSimpleNonLiteral;

			var expected = "ColumnName";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemSimple_EnglishValueLiteral()
		{
			var target = _BasicStringCriteriaItemSimpleLiteral;

			var expected = "\"Test\"";
			var actual = target.EnglishValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void CriteriaItemSimple_EnglishValueNonLiteral()
		{
			var target = _BasicStringCriteriaItemSimpleNonLiteral;

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
			var a = new CriteriaItemSimple(_commonGuidA, DataType.String, "Test", true);
			var b = new CriteriaItemSimple(_commonGuidA, DataType.String, "Test", true);

			Assert.Equal(a, b);
		}

		[Fact()]
		public void NotEqual_DifferentValue()
		{
			var a = new CriteriaItemSimple(_commonGuidA, DataType.String, "Test", true);
			var b = new CriteriaItemSimple(_commonGuidA, DataType.String, "Test1", true);

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Equal_DifferentGuid()
		{
			var a = new CriteriaItemSimple(_commonGuidB, DataType.String, "Test", true);
			var b = new CriteriaItemSimple(_commonGuidA, DataType.String, "Test", true);

			Assert.Equal(a, b);
		}

		[Fact()]
		public void NotEqual_LiteralNonLiteral()
		{
			var a = new CriteriaItemSimple(_commonGuidA, DataType.String, "Test", true);
			var b = new CriteriaItemSimple(_commonGuidA, DataType.String, "Test", false);

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void NotEqual_DifferentDataType()
		{
			var a = new CriteriaItemSimple(_commonGuidA, DataType.Numeric, "1", true);
			var b = new CriteriaItemSimple(_commonGuidA, DataType.String, "1", true);

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Copy_CreatesAnEqualCriteriaItem()
		{
			var a = new CriteriaItemSimple(DataType.String, "Test", true);
			var b = a.Copy();

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Copy_CreatesADeepDistinctCopy()
		{
			var a = new CriteriaItemSimple(DataType.String, "Test", true);
			var b = (CriteriaItemSimple)a.Copy();
			b.Value = "Test2";

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Serialize_BasicCriteriaItemSimple()
		{
			var target = new CriteriaItemSimple(_commonGuidA, DataType.String, "Test", true);

			var actual = target.Serialize();
			var expected = _BasicStringCriteriaItemSimpleLiteraljson;

			Assert.Equal(expected, actual);
		}

		//************************************************************************************
		// exception tests
		//************************************************************************************

		[Fact()]
		public void SetValue_DoesNotMatchDataType()
		{
			var target = new CriteriaItemSimple(DataType.Numeric, "1", true);

			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.Value = "Test");
		}

		[Fact()]
		public void SetValue_NumericTypeAllowsStringValueIfNotLiteral()
		{
			var target = new CriteriaItemSimple(DataType.Numeric, "1", true);
			try
			{
				target.IsValueLiteral = false;
				target.Value = "Test";
			}
			catch (CriteriaItemTypeMismatchException ex)
			{

			}

			Assert.True(target.Value == "Test");
		}

		[Fact()]
		public void SetValue_NumericTypeDoesNotAllowStringValueIfLiteral()
		{
			var target = new CriteriaItemSimple(DataType.Numeric, "1", true);
			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.Value = "Test");
		}

		[Fact()]
		public void SetValue_BooleanTypeDoesNotAllowStringValueIfLiteral()
		{
			var target = new CriteriaItemSimple(DataType.Boolean, "true", true);
			Assert.Throws<CriteriaItemTypeMismatchException>(() => target.Value = "Test");
		}

		[Fact()]
		public void SetValue_DoesMatchDataType()
		{
			var target = new CriteriaItemSimple(DataType.Numeric, "1", true);

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