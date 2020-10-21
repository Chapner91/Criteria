using Criteria.CriteriaUnits.CriteriaFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaUnits
{
	public static class ICriteriaUnitHelper
	{
		public static ICriteriaUnit InstantiateCriteriaUnitByType(string criteriaUnitType)
		{
			ICriteriaUnit criteriaUnit = default(ICriteriaUnit);
			switch (criteriaUnitType)
			{
				case "simple":
					criteriaUnit = new CriteriaUnitSimple();
					break;
				case "compound":
					criteriaUnit = new CriteriaUnitCompound();
					break;
				case "function":
					criteriaUnit = new CriteriaUnitFunction();
					break;
			}
			return criteriaUnit;
		}
	}
}