using Xunit;
using Criteria.CriteriaItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.CriteriaItems.CriteriaFunctions;

namespace Criteria.CriteriaItems.Tests
{
	public class ICriteriaItemHelperTests
	{


		[Fact()]
		public void InstantiateCriteriaItemByType_Simple()
		{
			var expected = new CriteriaItemSimple().GetType();
			var actual = ICriteriaItemHelper.InstantiateCriteriaItemByType("simple").GetType();
			
			Assert.Equal(expected, actual);
		}


		[Fact()]
		public void InstantiateCriteriaItemByType_Compound()
		{
			var expected = new CriteriaItemCompound().GetType();
			var actual = ICriteriaItemHelper.InstantiateCriteriaItemByType("compound").GetType();

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void InstantiateCriteriaItemByType_Function()
		{
			var expected = new CriteriaItemFunction().GetType();
			var actual = ICriteriaItemHelper.InstantiateCriteriaItemByType("function").GetType();

			Assert.Equal(expected, actual);
		}
	}
}