using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public class CriteriaItem
	{
		private string _criteriaItemjson;

		//------------------------------------------------------------------------------------
		//	PROPERTIES
		//------------------------------------------------------------------------------------

		public DataType DataType { get; set; }
		public string Value { get; set; }

		//------------------------------------------------------------------------------------
		//	Constructors
		//------------------------------------------------------------------------------------
		public CriteriaItem() { }

		public CriteriaItem(string json)
		{

		}

		public CriteriaItem(DataType dataType, string Value)
		{

		}

		//------------------------------------------------------------------------------------
		//	METHODS
		//------------------------------------------------------------------------------------
		public string GetAvailableFunctions()
		{
			return "";
		}
	}


	public enum DataType
	{
		String,
		Numeric,
		Date,
		Time,
		DateTime,
		Boolean
	}
}
