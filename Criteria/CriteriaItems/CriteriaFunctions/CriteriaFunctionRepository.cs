using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaUnits.CriteriaFunctions
{
	public class CriteriaFunctionConfigurationRepository
	{
		//	Date functions 
		//-------------------------------------------------------------------------------------
		//DateAdd(enum interval, int increment, ICriteriaUnit Date)
		//DateDiff(enum interval, ICriteriaUnit StartDate, ICriteriaUnit EndDate)
		//DateName(enum interval, ICriteriaUnit Date)
		//GetDate()
		//Day(ICriteriaUnit EndDate)
		//Month(ICriteriaUnit EndDate)
		//Year(ICriteriaUnit EndDate)

		//	String functions
		//-------------------------------------------------------------------------------------
		//Left(CriteriaUnitSimple, Int increment)
		//Substring(CriteriaUnitSimple, Int StartingIndex, Int Length)
		//Len(CriteriaUnitSimple)
		//Lower(CriteriaUnitSimple)
		//Upper(CriteriaUnitSimple)
		//LTrim(CriteriaUnitSimple)
		//RTrim(CriteriaUnitSimple)
		//Trim(CriteriaUnitSimple)
		//Lpad(CriteriaUnitSimple, char padding)
		//RPad(CriteriaUnitSimple, char padding)

		//	Math Functions
		//-------------------------------------------------------------------------------------
		//ABS(CriteriaUnitSimple)
		//Ceiling(CriteriaUnitSimple)
		//Floor(CriteriaUnitSimple)
		//Round(CriteriaUnitSimple, Int precision)

		// General Functions
		//-------------------------------------------------------------------------------------
		//Concat(Ordered list of ICriteriaUnits)
		//Coalesce(Ordered list of ICriteriaUnit)
		//Isnull(ICriteriaUnit ExpressionToCheck, ICriteriaUnit Replacement)
		//Nullif(ICriteriaUnit ExpressionToCheck, ICriteriaUnit ExpressionToCheckFor)

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


