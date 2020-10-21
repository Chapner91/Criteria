using Xunit;
using Criteria.CriteriaUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CriteriaTests.Mocks;
using Criteria.Enums;

namespace Criteria.CriteriaUnits.CriteriaFunctions.Tests
{
	public class CriteriaUnitFunctionTests
	{
		private CriteriaFunctionScheme _schemeSubString;
		private Guid guid1, guid2, guid3, guid4, guid5, guid6, guid7;
		private CriteriaUnitFunction _substringFunction;
		private CriteriaUnitFunction _substringFunctionAssignedArguments;
		private string _substringFunctionAssignedArgumentsJson;

		public CriteriaUnitFunctionTests()
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
			_substringFunction = new CriteriaUnitFunction(guid4, "substring", _schemeSubString);

			guid5 = Guid.NewGuid();
			guid6 = Guid.NewGuid();
			guid7 = Guid.NewGuid();

			_substringFunctionAssignedArguments = new CriteriaUnitFunction(guid4, "substring", _schemeSubString);
			_substringFunctionAssignedArguments.AssignArgument("expression", new CriteriaUnitSimple(guid5, DataType.String, "ExpressionColumnName", false));
			_substringFunctionAssignedArguments.AssignArgument("startFromIndex", new CriteriaUnitSimple(guid6, DataType.Numeric, "StartFromIndexColumnName", false));
			_substringFunctionAssignedArguments.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(guid7, DataType.Numeric, "LengthOfSubstringColumnName", false));

			_substringFunctionAssignedArgumentsJson = _substringFunctionAssignedArguments.Serialize();
		}

		[Fact()]
		public void Constructor_ID()
		{
			var guid = guid4;
			var target = _substringFunction;

			var expected = guid;
			var actual = target.CriteriaUnitID;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void Constructor_MetaDataIsCorrect()
		{
			var target = _substringFunction;

			Assert.True(
				target.CriteriaUnitType == "function" &&
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
				target.Exists(x => x.Argument.Name == "expression" && x.CriteriaUnit == null) &&
				target.Exists(x => x.Argument.Name == "startFromIndex" && x.CriteriaUnit == null) &&
				target.Exists(x => x.Argument.Name == "lengthOfSubstring" && x.CriteriaUnit == null)
				);
		}

		[Fact()]
		public void DeserializeFromJson()
		{
			var target = CriteriaUnitFunction.Deserialize(_substringFunctionAssignedArgumentsJson);

			var argumentAssignments = target.ArgumentAssignments.ToList();

			Assert.True(
				target.CriteriaUnitType == "function" &&
				target.FunctionName == "substring" &&
				target.CriteriaUnitID == guid4 &&
				target.ReturnDataType == DataType.String &&
				target.ReturnsSingleValue == true &&
				argumentAssignments.Exists(x => x.Argument.Name == "expression" && x.CriteriaUnit.CriteriaUnitID == guid5) &&
				argumentAssignments.Exists(x => x.Argument.Name == "startFromIndex" && x.CriteriaUnit.CriteriaUnitID == guid6) &&
				argumentAssignments.Exists(x => x.Argument.Name == "lengthOfSubstring" && x.CriteriaUnit.CriteriaUnitID == guid7)
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
			target.AssignArgument(argumentAssignmentID, new CriteriaUnitSimple(guid, DataType.String, "ColumnName", false));

			var expected = guid;
			var actual = argumentAssignment.CriteriaUnit.CriteriaUnitID;

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
			target.AssignArgument("expression", new CriteriaUnitSimple(guid, DataType.String, "ColumnName", false));

			var expected = guid;
			var actual = argumentAssignment.CriteriaUnit.CriteriaUnitID;

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
			target.AssignArgument(argumentAssignment1ID, new CriteriaUnitSimple(guid, DataType.String, "ColumnName", false));
			var guid2 = Guid.NewGuid();
			target.AssignArgument(argumentAssignment2ID, new CriteriaUnitSimple(guid2, DataType.Numeric, "ColumnName", false));


			Assert.NotEqual(argumentAssignment1.CriteriaUnit.CriteriaUnitID, argumentAssignment2.CriteriaUnit.CriteriaUnitID);
		}

		[Fact()]
		public void AssignArgument_OverwriteExistingArgument()
		{
			var target = _substringFunction;
			var argumentAssignments = _substringFunction.ArgumentAssignments.ToList();
			var argumentAssignment = argumentAssignments.Find(x => x.Argument.Name == "expression");
			var argumentAssignmentID = argumentAssignment.ArgumentAssignmentID;


			var guid = Guid.NewGuid();
			target.AssignArgument(argumentAssignmentID, new CriteriaUnitSimple(guid, DataType.String, "ColumnName", false));
			var guid2 = Guid.NewGuid();
			target.AssignArgument(argumentAssignmentID, new CriteriaUnitSimple(guid2, DataType.String, "ColumnName2", false));

			var expected = guid2;
			var actual = argumentAssignment.CriteriaUnit.CriteriaUnitID;

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
			target.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			target.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			target.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			var expected = $"Substring LengthOfSubstringColumnName characters from the StartFromIndexColumnName character of ExpressionColumnName";
			var actual = target.EnglishValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void GetEnglishForSomeValuesAssigned()
		{
			var target = _substringFunction;
			target.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			target.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));

			var expected = $"Substring UNASSIGNED characters from the StartFromIndexColumnName character of ExpressionColumnName";
			var actual = target.EnglishValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void GetSQLForAllValuesAssigned()
		{
			var target = _substringFunction;
			target.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			target.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			target.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			var expected = $"SUBSTRING(ExpressionColumnName, StartFromIndexColumnName, LengthOfSubstringColumnName)";
			var actual = target.SQLValue;

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void GetSQLForSomeValuesAssigned()
		{
			var target = _substringFunction;
			target.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			target.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));

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

			var a = new CriteriaUnitFunction("substring", 
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
			a.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			a.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			a.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			var b = new CriteriaUnitFunction("substring",
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
			b.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			b.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			b.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			Assert.Equal(a, b);
		}

		[Fact]
		public void Equal_ListUnitOrderDifferent()
		{

			var a = new CriteriaUnitFunction("substring",
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
			a.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			a.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			a.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			var b = new CriteriaUnitFunction("substring",
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
			b.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			b.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			b.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			Assert.Equal(a, b);
		}

		[Fact]
		public void NotEqual_FunctionName()
		{

			var a = new CriteriaUnitFunction("Test",
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
			a.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			a.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			a.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			var b = new CriteriaUnitFunction("substring",
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
			b.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			b.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			b.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

			Assert.NotEqual(a, b);
		}

		[Fact]
		public void NotEqual_ArgumentAssignments()
		{

			var a = new CriteriaUnitFunction("substring",
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
			a.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			a.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			a.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "TestColumnName", false));

			var b = new CriteriaUnitFunction("substring",
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
			b.AssignArgument("expression", new CriteriaUnitSimple(DataType.String, "ExpressionColumnName", false));
			b.AssignArgument("startFromIndex", new CriteriaUnitSimple(DataType.Numeric, "StartFromIndexColumnName", false));
			b.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "LengthOfSubstringColumnName", false));

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
			var b = (CriteriaUnitFunction)a.Copy();

			b.AssignArgument("lengthOfSubstring", new CriteriaUnitSimple(DataType.Numeric, "TestColumnName", false));

			Assert.NotEqual(a, b);
		}
	}
}