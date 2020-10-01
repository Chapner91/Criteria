using Xunit;
using CriteriaHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriteriaHelper.Tests
{
	public class CriteriaItemTests
	{

		private string _jsonAequalB = "{\"LeftSide\": \"A\",\"CriteriaItemOperator\": \"equal\",\"RightSide\": \"B\"}";

		[Fact()]
		public void CriteriaItem_InitializeWithJsonString()
		{

			var target = new CriteriaItem(_jsonAequalB);
			var control = new CriteriaItem() { LeftSide = "A", CriteriaItemOperator = CriteriaItemOperator.equal, RightSide = "B" };

			Assert.True(
				(control.LeftSide == target.LeftSide) && 
				(control.CriteriaItemOperator == target.CriteriaItemOperator) && 
				(control.RightSide == target.RightSide)
				, "Target and Control CriteriaItem properties differ");
		}

		[Fact()]
		public void CriteriaItem_InitializeWithOperators()
		{

			var target = new CriteriaItem("A", CriteriaItemOperator.equal, "B");
			var control = new CriteriaItem() { LeftSide = "A", CriteriaItemOperator = CriteriaItemOperator.equal, RightSide = "B" };

			Assert.True(
				(control.LeftSide == target.LeftSide) &&
				(control.CriteriaItemOperator == target.CriteriaItemOperator) &&
				(control.RightSide == target.RightSide)
				, "Target and Control CriteriaItem properties differ");
		}

		[Fact()]
		public void GetCriteriaItemSQL_BasicTest()
		{

			var target = new CriteriaItem() { LeftSide = "A", CriteriaItemOperator = CriteriaItemOperator.equal, RightSide = "B" };//new CriteriaItem(Json);

			var Expected = "( A = B )";
			var Actual = target.GetCriteriaItemSQL();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaItemEnglish_BasicTest()
		{

			var target = new CriteriaItem(_jsonAequalB);

			var Expected = "( A is equal to B )";
			var Actual = target.GetCriteriaItemEnglish();

			Assert.Equal(Expected, Actual);
		}
	}
}