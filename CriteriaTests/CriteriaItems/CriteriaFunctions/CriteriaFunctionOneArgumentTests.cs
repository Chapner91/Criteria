using Xunit;
using System;
using Criteria.Enums;

namespace Criteria.CriteriaItems.CriteriaFunctions.Tests
{
	public class CriteriaFunctionOneArgumentTests
	{

		//************************************************************************************
		// constructor tests
		//************************************************************************************

		[Fact()]
		public void CriteriaFunctionOneArgument_PropertyArguments()
		{
			var criteriaItem = new CriteriaFunctionOneArgument("Month", new CriteriaItemSimple(DataType.String, "ColumnA", false));

			Assert.True(
				(criteriaItem.FunctionName == "Month") &&
				(criteriaItem.CriteriaItemType == "function") &&
				(criteriaItem.CriteriaItemFunctionType == "singleArgument") &&
				(criteriaItem.EnglishValue == "The month of (ColumnA)") &&
				(criteriaItem.SQLValue == "MONTH(ColumnA)") &&
				(criteriaItem.ReturnDataType == DataType.Numeric)
				);
		}

		[Fact()]
		public void CriteriaFunctionOneArgument_PropertyArgumentsWithCriteriaItemID()
		{
			var guid = Guid.NewGuid();
			var criteriaItem = new CriteriaFunctionOneArgument(guid, "Length", new CriteriaItemSimple(DataType.String, "ColumnA", false));

			Assert.True(
				(criteriaItem.CriteriaItemID == guid) &&
				(criteriaItem.FunctionName == "Length") &&
				(criteriaItem.CriteriaItemType == "function") &&
				(criteriaItem.CriteriaItemFunctionType == "singleArgument") &&
				(criteriaItem.EnglishValue == "The Length of (ColumnA)") &&
				(criteriaItem.SQLValue == "LEN(ColumnA)") &&
				(criteriaItem.ReturnDataType == DataType.Numeric)
				);
		}





		[Fact()]
		public void SerializeTest()
		{
			Assert.True(false, "This test needs an implementation");
		}
	}
}