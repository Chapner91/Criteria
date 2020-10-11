using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.CriteriaExceptions;
using Criteria.Enums;
using Criteria.JsonConverters;
using Newtonsoft.Json;

//namespace Criteria.CriteriaItems
//{


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


	/*
	[JsonConverter(typeof(ICriteriaItemConverter))]
	class CriteriaItemFunction : ICriteriaItem
	{
		public string CriteriaItemType => "function";
		public DataType ReturnDataType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string Value => throw new NotImplementedException();


		private List<ICriteriaItem> _criteriaItems = new List<ICriteriaItem>();

		[JsonConverter(typeof(ICriteriaItemListConverter))]
		public List<ICriteriaItem> CriteriaItems
		{
			get => _criteriaItems;
			set
			{
				foreach (ICriteriaItem criteriaItem in value)
				{
					if (!ChildIsCorrectDataType(criteriaItem))
					{
						throw new CriteriaItemTypeMismatchException(ReturnDataType, criteriaItem);
					}
				}
				_criteriaItems = value;
			}
		}


		//*****************************************************************************
		// ******** CONSTRUCTORS
		//*****************************************************************************









		//*****************************************************************************
		// ******** PUBLIC METHODS
		//*****************************************************************************

		public string Serialize()
		{
			throw new NotImplementedException();
		}

		//*****************************************************************************
		// ******** PRIVATE METHODS
		//*****************************************************************************

		private bool ChildIsCorrectDataType(ICriteriaItem criteriaItem)
		{
			if (criteriaItem.ReturnDataType == ReturnDataType)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
*/