using Xunit;
using CriteriaHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriteriaHelper.Tests
{
	public class CriteriaPredicateTests
	{

		private string _jsonAEqualB					= "{\"LeftSide\": \"A\",\"CriteriaItemOperator\": \"Equal\",\"RightSide\": \"B\"}";
		private string _jsonANotEqualB				= "{\"LeftSide\": \"A\",\"CriteriaItemOperator\": \"NotEqual\",\"RightSide\": \"B\"}";
		private string _jsonALessThanB				= "{\"LeftSide\": \"A\",\"CriteriaItemOperator\": \"LessThan\",\"RightSide\": \"B\"}";
		private string _jsonALessThanOrEqualB		= "{\"LeftSide\": \"A\",\"CriteriaItemOperator\": \"LessThanOrEqual\",\"RightSide\": \"B\"}";
		private string _jsonAGreaterThanB			= "{\"LeftSide\": \"A\",\"CriteriaItemOperator\": \"GreaterThan\",\"RightSide\": \"B\"};";
		private string _jsonAGreaterThanOrEqualB	= "{\"LeftSide\": \"A\",\"CriteriaItemOperator\": \"GreaterThanOrEqual\",\"RightSide\": \"B\"}";
		private string _jsonAInListBCD				= "{\"LeftSide\": \"A\",\"CriteriaItemOperator\": \"InList\",\"RightSide\": \"B, C, D\"}";
		private string _jsonANotInListBCD			= "{\"LeftSide\": \"A\",\"CriteriaItemOperator\": \"NotInList\",\"RightSide\": \"B, C, D\"}";

		[Fact()]
		public void CriteriaPredicate_InitializeWithJsonString()
		{

			var target = new CriteriaPredicate(_jsonAEqualB);
			var control = new CriteriaPredicate() { LeftSide = "A", CriteriaItemOperator = CriteriaItemOperator.Equal, RightSide = "B" };

			Assert.True(
				(control.LeftSide == target.LeftSide) && 
				(control.CriteriaItemOperator == target.CriteriaItemOperator) && 
				(control.RightSide == target.RightSide)
				, "Target and Control CriteriaPredicate properties differ");
		}

		[Fact()]
		public void CriteriaPredicate_InitializeWithOperators()
		{

			var target = new CriteriaPredicate("A", CriteriaItemOperator.Equal, "B");
			var control = new CriteriaPredicate() { LeftSide = "A", CriteriaItemOperator = CriteriaItemOperator.Equal, RightSide = "B" };

			Assert.True(
				(control.LeftSide == target.LeftSide) &&
				(control.CriteriaItemOperator == target.CriteriaItemOperator) &&
				(control.RightSide == target.RightSide)
				, "Target and Control CriteriaPredicate properties differ");
		}

		[Fact()]
		public void GetCriteriaPredicateSQL_EqualOperator()
		{

			var target = new CriteriaPredicate(_jsonAEqualB);

			var Expected = "( A = B )";
			var Actual = target.GetCriteriaPredicateSQL();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateEnglish_EqualOperator()
		{

			var target = new CriteriaPredicate(_jsonAEqualB);

			var Expected = "( A is equal to B )";
			var Actual = target.GetCriteriaPredicateEnglish();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateSQL_NotEqualOperator()
		{

			var target = new CriteriaPredicate(_jsonANotEqualB);

			var Expected = "( A != B )";
			var Actual = target.GetCriteriaPredicateSQL();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateEnglish_NotEqualOperator()
		{

			var target = new CriteriaPredicate(_jsonANotEqualB);

			var Expected = "( A is not equal to B )";
			var Actual = target.GetCriteriaPredicateEnglish();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateSQL_LessThanOperator()
		{

			var target = new CriteriaPredicate(_jsonALessThanB);

			var Expected = "( A < B )";
			var Actual = target.GetCriteriaPredicateSQL();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateEnglish_LessThanOperator()
		{

			var target = new CriteriaPredicate(_jsonALessThanB);

			var Expected = "( A is less than B )";
			var Actual = target.GetCriteriaPredicateEnglish();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateSQL_LessThanOrEqualOperator()
		{

			var target = new CriteriaPredicate(_jsonALessThanOrEqualB);

			var Expected = "( A <= B )";
			var Actual = target.GetCriteriaPredicateSQL();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateEnglish_LessThanOrEqualOperator()
		{

			var target = new CriteriaPredicate(_jsonALessThanOrEqualB);

			var Expected = "( A is less than or equal to B )";
			var Actual = target.GetCriteriaPredicateEnglish();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateSQL_GreaterThanOperator()
		{

			var target = new CriteriaPredicate(_jsonAGreaterThanB);

			var Expected = "( A > B )";
			var Actual = target.GetCriteriaPredicateSQL();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateEnglish_GreaterThanOperator()
		{

			var target = new CriteriaPredicate(_jsonAGreaterThanB);

			var Expected = "( A is greater than B )";
			var Actual = target.GetCriteriaPredicateEnglish();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateSQL_GreaterThanOrEqualOperator()
		{

			var target = new CriteriaPredicate(_jsonAGreaterThanOrEqualB);

			var Expected = "( A >= B )";
			var Actual = target.GetCriteriaPredicateSQL();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateEnglish_GreaterThanOrEqualOperator()
		{

			var target = new CriteriaPredicate(_jsonAGreaterThanOrEqualB);

			var Expected = "( A is greater than or equal to B )";
			var Actual = target.GetCriteriaPredicateEnglish();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateSQL_InListOperator()
		{

			var target = new CriteriaPredicate(_jsonAInListBCD);

			var Expected = "( A IN ( B, C, D ) )";
			var Actual = target.GetCriteriaPredicateSQL();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateEnglish_InListOperator()
		{

			var target = new CriteriaPredicate(_jsonAInListBCD);

			var Expected = "( A is in the list ( B, C, D ) )";
			var Actual = target.GetCriteriaPredicateEnglish();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateSQL_NotInListOperator()
		{

			var target = new CriteriaPredicate(_jsonANotInListBCD);

			var Expected = "( A NOT IN ( B, C, D ) )";
			var Actual = target.GetCriteriaPredicateSQL();

			Assert.Equal(Expected, Actual);
		}

		[Fact()]
		public void GetCriteriaPredicateEnglish_NotInListOperator()
		{

			var target = new CriteriaPredicate(_jsonANotInListBCD);

			var Expected = "( A is not in the list ( B, C, D ) )";
			var Actual = target.GetCriteriaPredicateEnglish();

			Assert.Equal(Expected, Actual);
		}
	}
}



