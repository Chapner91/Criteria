using Xunit;
using Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.CriteriaExceptions;

namespace Criteria.Tests
{
	public class CriteriaItemTests
	{
		private string _jsonStringTest = "{\"DataType\" : \"String\", \"Value\" : \"Test\"}";
		private string _jsonDataTypeValueMismatch = "{\"DataType\" : \"Numeric\", \"Value\" : \"Test\"}";

		[Fact()]
		public void CriteriaItem_InitializeWithJsonString()
		{
			CriteriaItem control = new CriteriaItem() { DataType = DataType.String, Value = "Test" };
			CriteriaItem target = new CriteriaItem(_jsonStringTest);
			
			Assert.True(
				(control.DataType == target.DataType) && 
				(control.Value == target.Value)
				, "Target and Control CriteriaPredicate properties differ");
		}

		[Fact()]
		public void CriteriaItem_InitializeWithPropertyArguments()
		{
			CriteriaItem control = new CriteriaItem() { DataType = DataType.String, Value = "Test" };
			CriteriaItem target = new CriteriaItem(DataType.String, "Test");

			Assert.True(
				(control.DataType == target.DataType) &&
				(control.Value == target.Value)
				, "Target and Control CriteriaPredicate properties differ");
		}

		[Fact()]
		public void CriteriaItem_InitializeWithMismatchedTypes()
		{
			Assert.Throws<CriteriaItemDataTypeClashException>(() => new CriteriaItem(_jsonDataTypeValueMismatch));
		}
	}
}