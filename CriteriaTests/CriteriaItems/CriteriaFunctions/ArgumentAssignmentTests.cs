using Xunit;
using Criteria.CriteriaItems.CriteriaFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CriteriaTests.Mocks;
using Criteria.Enums;
using Criteria.CriteriaExceptions;

namespace Criteria.CriteriaItems.CriteriaFunctions.Tests
{
	public class ArgumentAssignmentTests
	{

		private CriteriaItemSimpleStringMock criteriaItemSimpleString;
		private ArgumentSingleStringMock argumentSingleString;

		private CriteriaItemCompoundStringMock criteriaItemCompoundString;
		private ArgumentMultipleStringMock argumentMultipleString;

		public ArgumentAssignmentTests()
		{
			criteriaItemSimpleString = new CriteriaItemSimpleStringMock();
			argumentSingleString = new ArgumentSingleStringMock();
			criteriaItemCompoundString = new CriteriaItemCompoundStringMock();
			argumentMultipleString = new ArgumentMultipleStringMock();
		}

		[Fact()]
		public void ArgumentAssignment_ConstructorWithNoCriteriaItem()
		{
			var target = new ArgumentAssignment(argumentSingleString);

			Assert.True(
				target.Argument.ArgumentID == Guid.Parse("ed52dd9d-1f0c-482a-ada9-77faef448dab") &&
				target.CriteriaItem == null
				);
		}

		[Fact()]
		public void ArgumentAssignment_ConstructorWithNoCriteriaItemWithIDPassed()
		{
			var guid = Guid.NewGuid();
			var target = new ArgumentAssignment(guid, argumentSingleString);

			Assert.True(
				target.ArgumentAssignmentID == guid &&
				target.Argument.ArgumentID == Guid.Parse("ed52dd9d-1f0c-482a-ada9-77faef448dab") &&
				target.CriteriaItem == null
				);
		}

		[Fact()]
		public void ArgumentAssignment_ConstructorWithBothParameters()
		{
			var target = new ArgumentAssignment(argumentSingleString, criteriaItemSimpleString);

			Assert.True(
				target.Argument.ArgumentID == Guid.Parse("ed52dd9d-1f0c-482a-ada9-77faef448dab") &&
				target.CriteriaItem.CriteriaItemID == Guid.Parse("8f84f401-2de2-42b7-b9e1-939385c0eace")
				);
		}

		[Fact()]
		public void ArgumentAssignment_ConstructorWithBothParametersWithIDPassed()
		{
			var guid = Guid.NewGuid();
			var target = new ArgumentAssignment(guid, argumentSingleString, criteriaItemSimpleString);

			Assert.True(
				target.ArgumentAssignmentID == guid &&
				target.Argument.ArgumentID == Guid.Parse("ed52dd9d-1f0c-482a-ada9-77faef448dab") &&
				target.CriteriaItem.CriteriaItemID == Guid.Parse("8f84f401-2de2-42b7-b9e1-939385c0eace")
				);
		}

		[Fact()]
		public void SetCriteriaItem()
		{
			var target = new ArgumentAssignment(argumentSingleString);

			target.CriteriaItem = criteriaItemSimpleString;

			var expected = Guid.Parse("8f84f401-2de2-42b7-b9e1-939385c0eace");
			var actual = target.CriteriaItem.CriteriaItemID;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void SetCriteriaItem_WrongDataType()
		{
			var argument = new Argument("expression", DataType.Numeric, true);
			var target = new ArgumentAssignment(argument);

			Assert.Throws<ArgumentTypeException>(() => target.CriteriaItem = criteriaItemSimpleString);
		}

		[Fact()]
		public void SetCriteriaItem_WrongCardinality()
		{
			var argument = new Argument("expression", DataType.String, true);
			var target = new ArgumentAssignment(argument);

			Assert.Throws<ArgumentTypeException>(() => target.CriteriaItem = criteriaItemCompoundString);
		}

		[Fact()]
		public void SetCriteriaItem_OverwriteExistingCriteria()
		{
			var guid = Guid.NewGuid();
			var replacementCriteriaItem = new CriteriaItemSimple(guid, DataType.String, "test", true);
			var target = new ArgumentAssignment(argumentSingleString, criteriaItemSimpleString);

			target.CriteriaItem = replacementCriteriaItem;

			var expected = guid;
			var actual = target.CriteriaItem.CriteriaItemID;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Equal_EqualObject()
		{
			var a = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaItemSimple(DataType.String, "test", true));
			var b = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaItemSimple(DataType.String, "test", true));
			Assert.Equal(a, b);
		}

		[Fact()]
		public void NotEqual_NotEqualArgument()
		{
			var a = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaItemSimple(DataType.String, "test", true));
			var b = new ArgumentAssignment(new Argument("expression1", DataType.String, true), new CriteriaItemSimple(DataType.String, "test", true));
			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void NotEqual_NotEqualCriteria()
		{
			var a = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaItemSimple(DataType.String, "test", true));
			var b = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaItemSimple(DataType.String, "test1", true));
			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Copy_CreatesAnEqualObject()
		{
			var a = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaItemSimple(DataType.String, "test", true));
			var b = a.Copy();

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Copy_CreatesADeepDistinctCopy()
		{
			var a = new ArgumentAssignment(new Argument("expression", DataType.String, true), new CriteriaItemSimple(DataType.String, "test", true));
			var b = a.Copy();

			b.CriteriaItem = new CriteriaItemSimple(DataType.String, "test2", true);

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
		//	var target = new ArgumentAssignment(argumentSingleString, criteriaItemSimpleString);

		//	target.SetArgument(replacementArgument);

		//	var expected = guid;
		//	var actual = target.Argument.ArgumentID;

		//	Assert.Equal(expected, actual);
		//}
	}
}