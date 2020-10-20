using Xunit;
using Criteria.CriteriaItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CriteriaTests.Mocks;
using Criteria.Enums;

namespace Criteria.CriteriaItems.CriteriaFunctions.Tests
{
	public class CriteriaItemFunctionTests
	{
		private CriteriaFunctionScheme _schemeSubString;
		private Guid guid1, guid2, guid3, guid4, guid5, guid6, guid7;
		private CriteriaItemFunction _substringFunction;
		private CriteriaItemFunction _substringFunctionAssignedArguments;
		private string _substringFunctionAssignedArgumentsJson;

		public CriteriaItemFunctionTests()
		{
			guid1 = Guid.NewGuid();
			guid2 = Guid.NewGuid();
			guid3 = Guid.NewGuid();

			var arguments = new List<Argument>()
			{
				new Argument(guid1, "expression", DataType.String, true),
				new Argument(guid2, "startFromIndex", DataType.Numeric, true),
				new Argument(guid3, "lengthOfSubstring", DataType.Numeric, true)
			};
			_schemeSubString = new CriteriaFunctionScheme("Substring", arguments, true, DataType.String, "SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})", "Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}");

			guid4 = Guid.NewGuid();
			_substringFunction = new CriteriaItemFunction(guid4, "substring", _schemeSubString);

			guid5 = Guid.NewGuid();
			guid6 = Guid.NewGuid();
			guid7 = Guid.NewGuid();

			_substringFunctionAssignedArguments = new CriteriaItemFunction(guid4, "substring", _schemeSubString);
			_substringFunctionAssignedArguments.AssignArgument("expression", new CriteriaItemSimple(guid5, DataType.String, "ExpressionColumnName", false));
			_substringFunctionAssignedArguments.AssignArgument("startFromIndex", new CriteriaItemSimple(guid6, DataType.Numeric, "StartFromIndexColumnName", false));
			_substringFunctionAssignedArguments.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(guid7, DataType.Numeric, "LengthOfSubstringColumnName", false));

			_substringFunctionAssignedArgumentsJson = _substringFunctionAssignedArguments.Serialize();
		}

		[Fact()]
		public void Constructor_ID()
		{
			var guid = guid4;
			var target = _substringFunction;

			var expected = guid;
			var actual = target.CriteriaItemID;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Constructor_MetaDataIsCorrect()
		{
			var target = _substringFunction;

			Assert.True(
				target.CriteriaItemType == "function" &&
				target.ReturnDataType == DataType.String &&
				target.ReturnsSingleValue == true
				);
		}

		[Fact()]
		public void Constructor_CreatesAppropriateNumberOfArgumentAssignments()
		{
			var target = _substringFunction;

			Assert.True(_substringFunction.ArgumentAssignments.Count() == 3);
		}

		[Fact()]
		public void Constructor_InitializesAppropriateNullArgumentAssignments()
		{
			var target = _substringFunction.ArgumentAssignments.ToList();

			Assert.True(
				target.Exists(x => x.Argument.Name == "expression" && x.CriteriaItem == null) &&
				target.Exists(x => x.Argument.Name == "startFromIndex" && x.CriteriaItem == null) &&
				target.Exists(x => x.Argument.Name == "lengthOfSubstring" && x.CriteriaItem == null)
				);
		}

		[Fact()]
		public void DeserializeFromJson()
		{
			var target = CriteriaItemFunction.Deserialize(_substringFunctionAssignedArgumentsJson);

			var argumentAssignments = target.ArgumentAssignments.ToList();

			Assert.True(
				target.CriteriaItemType == "function" &&
				target.FunctionName == "substring" &&
				target.CriteriaItemID == guid4 &&
				target.ReturnDataType == DataType.String &&
				target.ReturnsSingleValue == true &&
				argumentAssignments.Exists(x => x.Argument.Name == "expression" && x.CriteriaItem.CriteriaItemID == guid5) &&
				argumentAssignments.Exists(x => x.Argument.Name == "startFromIndex" && x.CriteriaItem.CriteriaItemID == guid6) &&
				argumentAssignments.Exists(x => x.Argument.Name == "lengthOfSubstring" && x.CriteriaItem.CriteriaItemID == guid7)
				);
		}

		//**********************************************************************
		//Should be able to adjust the ArgumentAssignments after construction using either the 
		//ID of the Assignment, or the name of the Argument.

		[Fact()]
		public void AssignArgument_ByArgumentID()
		{
			var target = _substringFunction;
			var argumentAssignments = _substringFunction.ArgumentAssignments.ToList();
			var argumentAssignment = argumentAssignments.Find(x => x.Argument.Name == "expression");
			var argumentAssignmentID = argumentAssignment.ArgumentAssignmentID;

			var guid = Guid.NewGuid();
			target.AssignArgument(argumentAssignmentID, new CriteriaItemSimple(guid, DataType.String, "ColumnName", false));

			var expected = guid;
			var actual = argumentAssignment.CriteriaItem.CriteriaItemID;

			Assert.Equal(expected, actual);

		}

		[Fact()]
		public void AssignArgument_ByArgumentName()
		{
			var target = _substringFunction;
			var argumentAssignments = _substringFunction.ArgumentAssignments.ToList();
			var argumentAssignment = argumentAssignments.Find(x => x.Argument.Name == "expression");
			var argumentAssignmentID = argumentAssignment.ArgumentAssignmentID;

			var guid = Guid.NewGuid();
			target.AssignArgument("expression", new CriteriaItemSimple(guid, DataType.String, "ColumnName", false));

			var expected = guid;
			var actual = argumentAssignment.CriteriaItem.CriteriaItemID;

			Assert.Equal(expected, actual);

		}

		[Fact()]
		public void AssignArgument_AssignsCorrectArgument()
		{
			var target = _substringFunction;
			var argumentAssignments = _substringFunction.ArgumentAssignments.ToList();
			var argumentAssignment1 = argumentAssignments.Find(x => x.Argument.Name == "expression");
			var argumentAssignment1ID = argumentAssignment1.ArgumentAssignmentID;

			var argumentAssignment2 = argumentAssignments.Find(x => x.Argument.Name == "startFromIndex");
			var argumentAssignment2ID = argumentAssignment2.ArgumentAssignmentID;


			var guid = Guid.NewGuid();
			target.AssignArgument(argumentAssignment1ID, new CriteriaItemSimple(guid, DataType.String, "ColumnName", false));
			var guid2 = Guid.NewGuid();
			target.AssignArgument(argumentAssignment2ID, new CriteriaItemSimple(guid2, DataType.Numeric, "ColumnName", false));


			Assert.NotEqual(argumentAssignment1.CriteriaItem.CriteriaItemID, argumentAssignment2.CriteriaItem.CriteriaItemID);
		}

		[Fact()]
		public void AssignArgument_OverwriteExistingArgument()
		{
			var target = _substringFunction;
			var argumentAssignments = _substringFunction.ArgumentAssignments.ToList();
			var argumentAssignment = argumentAssignments.Find(x => x.Argument.Name == "expression");
			var argumentAssignmentID = argumentAssignment.ArgumentAssignmentID;


			var guid = Guid.NewGuid();
			target.AssignArgument(argumentAssignmentID, new CriteriaItemSimple(guid, DataType.String, "ColumnName", false));
			var guid2 = Guid.NewGuid();
			target.AssignArgument(argumentAssignmentID, new CriteriaItemSimple(guid2, DataType.String, "ColumnName2", false));

			var expected = guid2;
			var actual = argumentAssignment.CriteriaItem.CriteriaItemID;

			Assert.Equal(expected, actual);
		}

		//**********************************************************************
		//Should be able to get the SQL and English translations of the function when any number of assignments have been made
		//"Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}"
		//"SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})"

		[Fact()]
		public void GetEnglishForAllValuesAssigned()
		{
			var target = _substringFunction;
			target.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			target.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			target.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			var expected = $"Substring LengthOfSubstringColumnName characters from the StartFromIndexColumnName character of ExpressionColumnName";
			var actual = target.EnglishValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void GetEnglishForSomeValuesAssigned()
		{
			var target = _substringFunction;
			target.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			target.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));

			var expected = $"Substring UNASSIGNED characters from the StartFromIndexColumnName character of ExpressionColumnName";
			var actual = target.EnglishValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void GetSQLForAllValuesAssigned()
		{
			var target = _substringFunction;
			target.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			target.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			target.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			var expected = $"SUBSTRING(ExpressionColumnName, StartFromIndexColumnName, LengthOfSubstringColumnName)";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void GetSQLForSomeValuesAssigned()
		{
			var target = _substringFunction;
			target.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			target.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));

			var expected = $"SUBSTRING(ExpressionColumnName, StartFromIndexColumnName, NULL)";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		//**********************************************************************
		// Should be able to create deep distinct copies of them
		// these tests ensure that the reference types are actually distinct, and not
		// just a reference that is shared by the two resulting copies


		[Fact]
		public void Equal_EqualObjects()
		{

			var a = new CriteriaItemFunction("substring", 
				new CriteriaFunctionScheme(
					"Substring", 
					new List<Argument>()
					{
						new Argument("expression", DataType.String, true),
						new Argument("startFromIndex", DataType.Numeric, true),
						new Argument("lengthOfSubstring", DataType.Numeric, true)
					}, 
					true, 
					DataType.String, 
					"SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})", 
					"Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}")
				);
			a.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			a.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			a.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			var b = new CriteriaItemFunction("substring",
				new CriteriaFunctionScheme(
					"Substring",
					new List<Argument>()
					{
						new Argument("expression", DataType.String, true),
						new Argument("startFromIndex", DataType.Numeric, true),
						new Argument("lengthOfSubstring", DataType.Numeric, true)
					},
					true,
					DataType.String,
					"SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})",
					"Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}")
				);
			b.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			b.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			b.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			Assert.Equal(a, b);
		}

		[Fact]
		public void Equal_ListItemOrderDifferent()
		{

			var a = new CriteriaItemFunction("substring",
				new CriteriaFunctionScheme(
					"Substring",
					new List<Argument>()
					{
						new Argument("startFromIndex", DataType.Numeric, true),
						new Argument("expression", DataType.String, true),
						new Argument("lengthOfSubstring", DataType.Numeric, true)
					},
					true,
					DataType.String,
					"SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})",
					"Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}")
				);
			a.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			a.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			a.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			var b = new CriteriaItemFunction("substring",
				new CriteriaFunctionScheme(
					"Substring",
					new List<Argument>()
					{
						new Argument("expression", DataType.String, true),
						new Argument("startFromIndex", DataType.Numeric, true),
						new Argument("lengthOfSubstring", DataType.Numeric, true)
					},
					true,
					DataType.String,
					"SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})",
					"Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}")
				);
			b.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			b.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			b.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			Assert.Equal(a, b);
		}

		[Fact]
		public void NotEqual_FunctionName()
		{

			var a = new CriteriaItemFunction("Test",
				new CriteriaFunctionScheme(
					"Substring",
					new List<Argument>()
					{
						new Argument("expression", DataType.String, true),
						new Argument("startFromIndex", DataType.Numeric, true),
						new Argument("lengthOfSubstring", DataType.Numeric, true)
					},
					true,
					DataType.String,
					"SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})",
					"Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}")
				);
			a.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			a.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			a.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			var b = new CriteriaItemFunction("substring",
				new CriteriaFunctionScheme(
					"Substring",
					new List<Argument>()
					{
						new Argument("expression", DataType.String, true),
						new Argument("startFromIndex", DataType.Numeric, true),
						new Argument("lengthOfSubstring", DataType.Numeric, true)
					},
					true,
					DataType.String,
					"SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})",
					"Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}")
				);
			b.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			b.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			b.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			Assert.NotEqual(a, b);
		}

		[Fact]
		public void NotEqual_ArgumentAssignments()
		{

			var a = new CriteriaItemFunction("substring",
				new CriteriaFunctionScheme(
					"Substring",
					new List<Argument>()
					{
						new Argument("expression", DataType.String, true),
						new Argument("startFromIndex", DataType.Numeric, true),
						new Argument("lengthOfSubstring", DataType.Numeric, true)
					},
					true,
					DataType.String,
					"SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})",
					"Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}")
				);
			a.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			a.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			a.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "TestColumnName", false));

			var b = new CriteriaItemFunction("substring",
				new CriteriaFunctionScheme(
					"Substring",
					new List<Argument>()
					{
						new Argument("expression", DataType.String, true),
						new Argument("startFromIndex", DataType.Numeric, true),
						new Argument("lengthOfSubstring", DataType.Numeric, true)
					},
					true,
					DataType.String,
					"SUBSTRING({expression}, {startFromIndex}, {lengthOfSubstring})",
					"Substring {lengthOfSubstring} characters from the {startFromIndex} character of {expression}")
				);
			b.AssignArgument("expression", new CriteriaItemSimple(DataType.String, "ExpressionColumnName", false));
			b.AssignArgument("startFromIndex", new CriteriaItemSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			b.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Copy_CreatesAnEqualObject()
		{
			var a = _substringFunctionAssignedArguments;
			var b = a.Copy();

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Copy_CreatesADeepDistinctCopy()
		{
			var a = _substringFunctionAssignedArguments;
			var b = (CriteriaItemFunction)a.Copy();

			b.AssignArgument("lengthOfSubstring", new CriteriaItemSimple(DataType.Numeric, "TestColumnName", false));

			Assert.NotEqual(a, b);
		}
	}
}