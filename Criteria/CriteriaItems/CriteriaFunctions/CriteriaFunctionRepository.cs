using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	public class CriteriaFunctionConfigurationRepository
	{
		//	Date functions 
		//-------------------------------------------------------------------------------------
		//DateAdd(enum interval, int increment, ICriteriaItem Date)
		//DateDiff(enum interval, ICriteriaItem StartDate, ICriteriaItem EndDate)
		//DateName(enum interval, ICriteriaItem Date)
		//GetDate()
		//Day(ICriteriaItem EndDate)
		//Month(ICriteriaItem EndDate)
		//Year(ICriteriaItem EndDate)

		//	String functions
		//-------------------------------------------------------------------------------------
		//Left(CriteriaItemSimple, Int increment)
		//Substring(CriteriaItemSimple, Int StartingIndex, Int Length)
		//Len(CriteriaItemSimple)
		//Lower(CriteriaItemSimple)
		//Upper(CriteriaItemSimple)
		//LTrim(CriteriaItemSimple)
		//RTrim(CriteriaItemSimple)
		//Trim(CriteriaItemSimple)
		//Lpad(CriteriaItemSimple, char padding)
		//RPad(CriteriaItemSimple, char padding)

		//	Math Functions
		//-------------------------------------------------------------------------------------
		//ABS(CriteriaItemSimple)
		//Ceiling(CriteriaItemSimple)
		//Floor(CriteriaItemSimple)
		//Round(CriteriaItemSimple, Int precision)

		// General Functions
		//-------------------------------------------------------------------------------------
		//Concat(Ordered list of ICriteriaItems)
		//Coalesce(Ordered list of ICriteriaItem)
		//Isnull(ICriteriaItem ExpressionToCheck, ICriteriaItem Replacement)
		//Nullif(ICriteriaItem ExpressionToCheck, ICriteriaItem ExpressionToCheckFor)

		//private static Dictionary<string, CriteriaFunctionConfiguration> ConfiguredFunctionList = new Dictionary<string, CriteriaFunctionConfiguration>();

		//static CriteriaFunctionRepository()
		//{
		//	var criteriaFunctions = new List<CriteriaFunctionConfiguration>()
		//	{
		//		new CriteriaFunctionConfiguration("Month"),
		//		new CriteriaFunctionConfiguration("Length")
		//	}


		//	// TODO : Read Functions in from a databse table 
		//	//ConfiguredFunctionList.Add()
		//}

	}
}


