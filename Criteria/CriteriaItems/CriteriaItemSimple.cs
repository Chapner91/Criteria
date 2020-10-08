using Criteria.CriteriaExceptions;
using Criteria.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaItems
{
	public class CriteriaItemSimple : ICriteriaItem
	{
		private string _value;
	
		[JsonProperty(PropertyName = "DataType")]
		public DataType DataType { get; set; }

		[JsonProperty(PropertyName = "Value")]
		public string Value
		{
			get => _value;
			set
			{
				if (ValueIsCorrectDataType(value))
				{
					_value = value;
				} 
				else
				{
					throw new CriteriaItemTypeMismatchException(DataType, value);
				}
			}
		}

		//*****************************************************************************
		// ******** CONSTRUCTORS
		//*****************************************************************************

		public CriteriaItemSimple()	{ }

		public CriteriaItemSimple(string criteriaItemJson)
		{
			CriteriaItemSimple criteriaItemFromJson = Deserialize(criteriaItemJson);

			this.DataType = criteriaItemFromJson.DataType;
			this.Value = criteriaItemFromJson.Value;
		}

		public CriteriaItemSimple(DataType dataType, string value)
		{
			this.DataType = dataType;
			this.Value = value;
		}

		//*****************************************************************************
		// ******** PUBLIC METHODS
		//*****************************************************************************

		public string Serialize()
		{
			var settings = new JsonSerializerSettings()
			{
				TypeNameHandling = TypeNameHandling.All
			};

			return JsonConvert.SerializeObject(this, settings);
		}

		public override bool Equals(object obj)
		{
			CriteriaItemSimple that = obj as CriteriaItemSimple;
			if (that == null)
			{
				return false;
			}
			else
			{
				return (this.DataType == that.DataType && this.Value == that.Value);
			}
		}

		public override int GetHashCode()
		{
			var hashCode = -1382053921;
			hashCode = hashCode * -1521134295 + DataType.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
			return hashCode;
		}

		//*****************************************************************************
		// ******** PRIVATE METHODS
		//*****************************************************************************

		private static CriteriaItemSimple Deserialize(string criteriaItemJson)
		{
			var settings = new JsonSerializerSettings()
			{
				TypeNameHandling = TypeNameHandling.All
			};

			return (CriteriaItemSimple)JsonConvert.DeserializeObject(criteriaItemJson, settings);
		}

		private bool ValueIsCorrectDataType(string value)
		{
			if(DataType == DataType.DateTime)
			{
				DateTime output;
				return DateTime.TryParse(value, out output);
			}
			else if (DataType == DataType.Numeric)
			{
				double output;
				return Double.TryParse(value, out output);
			}
			else if (DataType == DataType.Boolean)
			{
				bool output;
				return Boolean.TryParse(value, out output);
			}
			else if (DataType == DataType.String)
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
