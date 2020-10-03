using Criteria.CriteriaExceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
			var serializer = new JsonSerializer();

			using (var reader = new StringReader(json))
			using (var jsonReader = new JsonTextReader(reader))
			{
				CriteriaItem criteriaItemFromJson = serializer.Deserialize<CriteriaItem>(jsonReader);

				DataType = criteriaItemFromJson.DataType;
				// verify that the type of the value can fit the specified Datatype
				if (this.ValueMatchesDataType(criteriaItemFromJson.Value))
				{
					Value = criteriaItemFromJson.Value;
				}
				else
				{
					throw new CriteriaItemDataTypeClashException(DataType, criteriaItemFromJson.Value);
				}			
			}
		}

		public CriteriaItem(DataType dataType, string value)
		{
			DataType = dataType;
			// verify that the type of the value can fit the specified Datatype
			if (this.ValueMatchesDataType(value))
			{
				Value = value;
			} else
			{
				throw new CriteriaItemDataTypeClashException(dataType, value);
			}
		}

		//------------------------------------------------------------------------------------
		//	METHODS
		//------------------------------------------------------------------------------------
		private bool ValueMatchesDataType(string value)
		{
			// the flexibility of a string means that all values fit within the definition. no type checks are to be performed
			if (DataType == DataType.String)
			{
				return true;
			}
			else if (DataType == DataType.DateTime)
			{
				DateTime result;
				if (!DateTime.TryParse(value, out result))
				{
					return false;
				} else { return true; }
			}
			else if (DataType == DataType.Boolean)
			{
				bool result;
				if (!Boolean.TryParse(value, out result))
				{
					return false;
				} else { return true; }
			}
			else if (DataType == DataType.Numeric)
			{
				// I'm using double here because all of the other types will fit into double,
				// The type can be more or less specific than double, but must be able to 
				// convert at least to a double in order to avoid an exception
				double result;
				if (!Double.TryParse(value, out result))
				{
					return false;
				} else { return true; }
			}
			else
			{
				return false;
			}
		}

		public string GetAvailableFunctions()
		{
			return "";
		}
	}


	public enum DataType
	{
		Numeric,
		String,
		DateTime,
		Boolean
	}
}
