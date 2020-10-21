using Xunit;
using Criteria.CriteriaUnits.CriteriaFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CriteriaTests.Mocks;
using Criteria.Enums;
using Criteria.CriteriaExceptions;

namespace Criteria.CriteriaUnits.CriteriaFunctions.Tests
{
	public class ArgumentAssignmentTests
	{

		private CriteriaUnitSimpleStringMock criteriaUnitSimpleString;
		private Argument argumentSingleString;

		private CriteriaUnitCompoundStringMock criteriaUnitCompoundString;
		private Argument argumentMultipleString;

		public ArgumentAssignmentTests()
		{
			criteriaUnitSimpleString = new CriteriaUnitSimpleStringMock();
			argumentSingleString = new Argument(Guid.Parse("ed52dd9d-1f0c-482a-ada9-77faef448dab"), "testexpression", DataType.String, true);
			criteriaUnitCompoundString = new CriteriaUnitCompoundStringMock();
			argumentMultipleString = new Argument(Guid.Parse("94c2daeb-bff3-4139-89b4-509f3115a847"), "testexpression", DataType.String, false);
		}

		[Fact()]
		public void ArgumentAssignment_ConstructorWithNoCriteriaUnit()
		{
			var target = new ArgumentAssignment(argumentSingleString);

			Assert.True(
				target.Argument.ArgumentID == Guid.Parse("ed52dd9d-1f0c-482a-ada9-77faef448dab") &&
				target.CriteriaUnit == null
				);
		}

		[Fact()]
		public void ArgumentAssignment_ConstructorWithNoCriteriaUnitWithIDPassed()
		{
			var guid = Guid.NewGuid();
			var target = new ArgumentAssignment(guid, argumentSingleString);

			Assert.True(
				target.ArgumentAssignmentID == guid &&
				target.Argument.ArgumentID == Guid.Parse("ed52dd9d-1f0c-482a-ada9-77faef448dab") &&
				target.CriteriaUnit == null
				);
		}

		[Fact()]
		public void ArgumentAssignment_ConstructorWithBothParameters()
		{
			var target = new ArgumentAssignment(argumentSingleString, criteriaUnitSimpleString);

			Assert.True(
				target.Argument.ArgumentID == Guid.Parse("ed52dd9d-1f0c-482a-ada9-77faef448dab") &&
				target.CriteriaUnit.CriteriaUnitID == Guid.Parse("8f84f401-2de2-42b7-b9e1-939385c0eace")
				);
		}

		[Fact()]
		public void ArgumentAssignment_ConstructorWithBothParametersWithIDPassed()
		{
			var guid = Guid.NewGuid();
			var target = new ArgumentAssignment(guid, argumentSingleString, criteriaUnitSimpleString);

			Assert.True(
				target.ArgumentAssignmentID == guid &&
				target.Argument.ArgumentID == Guid.Parse("ed52dd9d-1f0c-482a-ada9-77faef448dab") &&
				target.CriteriaUnit.CriteriaUnitID == Guid.Parse("8f84f401-2de2-42b7-b9e1-939385c0eace")
				);
		}

		[Fact()]
		public void SetCriteriaUnit()
		{
			var target = new ArgumentAssignment(argumentSingleString);

			target.CriteriaUnit = criteriaUnitSimpleString;

			var expected = Guid.Parse("8f84f401-2de2-42b7-b9e1-939385c0eace");
			var actual = target.CriteriaUnit.CriteriaUnitID;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void SetCriteriaUnit_WrongDataType()
		{
			var argument = new Argument("expression", DataType.Numeric, true);
			var target = new ArgumentAssignment(argument);

			Assert.Throws<ArgumentTypeException>(() => target.CriteriaUnit = criteriaUnitSimpleString);
		}

		[Fact()]
		public void SetCriteriaUnit_WrongCardinality()
		{
			var argument = new Argument("expression", DataType.String, true);
			var target = new ArgumentAssignment(argument);

			Assert.Throws<ArgumentTypeException>(() => target.CriteriaUnit = criteriaUnitCompoundString);
		}

		[Fact()]
		public void SetCriteriaUnit_OverwriteExistingCriteria()
		{
			var guid = Guid.NewGuid();
			var replacementCriteriaUnit = new CriteriaUnitSimple(guid, DataType.String, "test", true);
			var target = new ArgumentAssignment(argumentSingleString, criteriaUnitSimpleString);

			target.CriteriaUnit = replacementCriteriaUnit;

			var expected = guid;
			var actual = target.CriteriaUnit.CriteriaUnitID;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Equal_EqualObject()
		{
			var a = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaUnitSimple(DataType.String, "test", true));
			var b = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaUnitSimple(DataType.String, "test", true));
			Assert.Equal(a, b);
		}

		[Fact()]
		public void NotEqual_NotEqualArgument()
		{
			var a = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaUnitSimple(DataType.String, "test", true));
			var b = new ArgumentAssignment(new Argument("expression1", DataType.String, true), new CriteriaUnitSimple(DataType.String, "test", true));
			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void NotEqual_NotEqualCriteria()
		{
			var a = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaUnitSimple(DataType.String, "test", true));
			var b = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaUnitSimple(DataType.String, "test1", true));
			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Copy_CreatesAnEqualObject()
		{
			var a = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaUnitSimple(DataType.String, "test", true));
			var b = a.Copy();

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Copy_CreatesADeepDistinctCopy()
		{
			var a = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaUnitSimple(DataType.String, "test", true));
			var b = a.Copy();

			b.CriteriaUnit = new CriteriaUnitSimple(DataType.String, "test2", true);

			Assert.NotEqual(a, b);
		}

		//[Fact()]
		//public void SetArgument_NullCriteriaChangeToSameArgumentType()
		//{
		//	var guid = Guid.NewGuid();
		//	var replacementArgument = new Argument(guid, "replacement", DataType.String, true);
		//	var target = new ArgumentAssignment(argumentSingleString);

		//	target.Argument = replacementArgument;

		//	var expected = guid;
		//	var actual = target.Argument.ArgumentID;

		//	Assert.Equal(expected, actual);
		//}

		//[Fact()]
		//public void SetArgument_NullCriteriaChangeToDifferentArgumentDataType()
		//{
		//	var guid = Guid.NewGuid();
		//	var replacementArgument = new Argument(guid, "replacement", DataType.Numeric, true);
		//	var target = new ArgumentAssignment(argumentSingleString);

		//	target.SetArgument(replacementArgument);

		//	var expected = guid;
		//	var actual = target.Argument.ArgumentID;

		//	Assert.Equal(expected, actual);
		//}

		//[Fact()]
		//public void SetArgument_NullCriteriaChangeToSameArgumentCardinality()
		//{
		//	var guid = Guid.NewGuid();
		//	var replacementArgument = new Argument(guid, "replacement", DataType.String, false);
		//	var target = new ArgumentAssignment(argumentSingleString);

		//	target.SetArgument(replacementArgument);

		//	var expected = guid;
		//	var actual = target.Argument.ArgumentID;

		//	Assert.Equal(expected, actual);
		//}

		//[Fact()]
		//public void SetArgument_ChangeToSameArgumentType()
		//{
		//	var guid = Guid.NewGuid();
		//	var replacementArgument = new Argument(guid, "replacement", DataType.String, true);
		//	var target = new ArgumentAssignment(argumentSingleString, criteriaUnitSimpleString);

		//	target.SetArgument(replacementArgument);

		//	var expected = guid;
		//	var actual = target.Argument.ArgumentID;

		//	Assert.Equal(expected, actual);
		//}
	}
}