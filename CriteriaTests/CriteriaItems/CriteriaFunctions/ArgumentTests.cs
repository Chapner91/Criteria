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
			var a = new Argument("expression", DataType.String, true);
			var b = new Argument("expression", DataType.String, true);
			
			Assert.Equal(a, b);
		}

		[Fact()]
		public void NotEqual_DifferentDataTypes()
		{
			var a = new Argument("expression", DataType.Numeric, true);
			var b = new Argument("expression", DataType.Boolean, true);

			Assert.NotEqual(a, b);
		}

		[Fact()]
		public void NotEqual_DifferentSingleValue()
		{
			var a = new Argument("expression", DataType.Numeric, true);
			var b = new Argument("expression", DataType.Boolean, false);

			Assert.NotEqual(a, b);
		}
	}
}