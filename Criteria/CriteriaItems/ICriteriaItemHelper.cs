using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaItems
{
	public static class ICriteriaItemHelper
	{
		public static ICriteriaItem InstantiateCriteriaItemByType(string criteriaItemType)
		{
			ICriteriaItem criteriaItem = default(ICriteriaItem);
			switch (criteriaItemType)
			{
				case "simple":
					criteriaItem = new CriteriaItemSimple();
					break;
				case "compound":
					criteriaItem = new CriteriaItemCompound();
					break;
				case "function":
					criteriaItem = new CriteriaItemFunction();
					break;
			}
			return criteriaItem;
		}
	}
}