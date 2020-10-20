using Xunit;
using System.Collections.Generic;
using System.Linq;
using Criteria.Enums;

namespace Criteria.CriteriaItems.CriteriaFunctions.Tests
{
	public class ArgumentTests
	{
		[Fact()]
		public void Argument_Constructor()
		{
			var target = new Argument("expression", DataType.String, true);

			Assert.True
				(
					target.Name == "expression" &&
					target.DataType == DataType.String &&
					target.RequiresSingleValue == true					
				);
		}

		[Fact()]
		public void Equal_EqualObjects()
		{
			var argumentID = System.Guid.NewGuid();
			var a = new Argument(argumentID, "expression", DataType.String, true);
			var b = new Argument(argumentID, "expression", DataType.String, true);
			
			Assert.Equal(a, b);
		}

		[Fact()]
		public void NotEqual_DifferentDataTypes()
		{
			var argumentID = System.Guid.NewGuid();
			var a = new Argument(argumentID, "expression", DataType.Numeric, true);
			var b = new Argument(argumentID, "expression", DataType.Boolean, true);

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void NotEqual_DifferentSingleValue()
		{
			var argumentID = System.Guid.NewGuid();
			var a = new Argument(argumentID, "expression", DataType.Numeric, true);
			var b = new Argument(argumentID, "expression", DataType.Numeric, false);

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void Equal_DifferentArgumentID()
		{
			var a = new Argument("expression", DataType.Numeric, true);
			var b = new Argument("expression", DataType.Numeric, true);

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Copy_CreatesAnEqualObject()
		{
			var a = new Argument("expression", DataType.Numeric, true);
			var b = a.Copy();

			Assert.Equal(a, b);
		}

		[Fact()]
		public void Copy_CreatesADeepDistinctCopy()
		{
			var a = new Argument("expression", DataType.Numeric, true);
			var b = (Argument)a.Copy();
			b.Name = "test";

			Assert.NotEqual(a, b);
		}
	}
}