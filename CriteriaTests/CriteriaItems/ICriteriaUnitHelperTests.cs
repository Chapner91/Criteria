using Xunit;
using Criteria.CriteriaUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.CriteriaUnits.CriteriaFunctions;

namespace Criteria.CriteriaUnits.Tests
{
	public class ICriteriaUnitHelperTests
	{


		[Fact()]
		public void InstantiateCriteriaUnitByType_Simple()
		{
			var expected = new CriteriaUnitSimple().GetType();
			var actual = ICriteriaUnitHelper.InstantiateCriteriaUnitByType("simple").GetType();
			
			Assert.Equal(expected, actual);
		}


		[Fact()]
		public void InstantiateCriteriaUnitByType_Compound()
		{
			var expected = new CriteriaUnitCompound().GetType();
			var actual = ICriteriaUnitHelper.InstantiateCriteriaUnitByType("compound").GetType();

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void InstantiateCriteriaUnitByType_Function()
		{
			var expected = new CriteriaUnitFunction().GetType();
			var actual = ICriteriaUnitHelper.InstantiateCriteriaUnitByType("function").GetType();

			Assert.Equal(expected, actual);
		}
	}
}