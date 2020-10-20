using Criteria.CriteriaExceptions;
using Criteria.Enums;
using Criteria.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaItems
{
	[JsonConverter(typeof(ICriteriaItemConverter))]
	public class CriteriaItemSimple : ICriteriaItem
	{
		private string _value;

		[JsonProperty(PropertyName = "CriteriaItemType")]
		public string CriteriaItemType => "simple";

		[JsonProperty(PropertyName = "CriteriaItemID")]
		public Guid CriteriaItemID { get; private set; }

		[JsonProperty(PropertyName = "ReturnDataType")]
		public DataType ReturnDataType { get; set; }

		[JsonProperty(PropertyName = "IsValueLiteral")]
		public bool IsValueLiteral { get; set; }

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
					throw new CriteriaItemTypeMismatchException(ReturnDataType, value);
				}
			}
		}

		[JsonIgnore]
		public bool ReturnsSingleValue => true;

		[JsonIgnore]
		public string SQLValue
		{
			get
			{
				if(ReturnDataType == DataType.String && IsValueLiteral == true)
				{
					return $"'{Value}'";
				} 
				else
				{
					return Value;
				}
			}
		}
		
		[JsonIgnore]
		public string EnglishValue
		{
			get
			{
				if (ReturnDataType == DataType.String && IsValueLiteral == true)
				{
					return $"\"{Value}\"";
				}
				else
				{
					return Value;
				}
			}
		}

		//*****************************************************************************
		// ******** CONSTRUCTORS
		//*****************************************************************************

		public CriteriaItemSimple()	{ }

		public CriteriaItemSimple(string criteriaItemJson)
		{
			CriteriaItemSimple that = Deserialize(criteriaItemJson);
			
			this.CriteriaItemID = that.CriteriaItemID;
			this.ReturnDataType = that.ReturnDataType;
			this.Value = that.Value;
			this.IsValueLiteral = that.IsValueLiteral;
		}

		public CriteriaItemSimple(DataType dataType, string value, bool isValueLiteral)
		{
			this.CriteriaItemID = Guid.NewGuid();
			this.ReturnDataType = dataType;
			this.Value = value;
			this.IsValueLiteral = isValueLiteral;
		}

		public CriteriaItemSimple(Guid criteriaItemID, DataType dataType, string value, bool isValueLiteral) : this(dataType, value, isValueLiteral)
		{
			this.CriteriaItemID = criteriaItemID;
			//this.ReturnDataType = dataType;
			//this.Value = value;
			//this.IsValueLiteral = isValueLiteral;
		}

		//*****************************************************************************
		// ******** PUBLIC METHODS
		//*****************************************************************************

		public string Serialize()
		{
			var settings = new JsonSerializerSettings()
			{
				//TypeNameHandling = TypeNameHandling.All
			};

			return JsonConvert.SerializeObject(this, settings);
		}

		public ICriteriaItem Copy()
		{
			return new CriteriaItemSimple(ReturnDataType, Value, IsValueLiteral);
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
				return (
					this.ReturnDataType == that.ReturnDataType && 
					this.Value == that.Value && 
					this.IsValueLiteral == that.IsValueLiteral
					);
			}
		}

		public override int GetHashCode()
		{
			var hashCode = -1382053921;
			hashCode = hashCode * -1521134295 + ReturnDataType.GetHashCode();
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
				//TypeNameHandling = TypeNameHandling.All
			};

			return JsonConvert.DeserializeObject<CriteriaItemSimple>(criteriaItemJson, settings);
		}

		private bool ValueIsCorrectDataType(string value)
		{
			if(IsValueLiteral)
			{
				if (ReturnDataType == DataType.DateTime)
				{
					DateTime output;
					return DateTime.TryParse(value, out output);
				}
				else if (ReturnDataType == DataType.Numeric)
				{
					double output;
					return Double.TryParse(value, out output);
				}
				else if (ReturnDataType == DataType.Boolean)
				{
					bool output;
					return Boolean.TryParse(value, out output);
				}
				else if (ReturnDataType == DataType.String)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return true;
			}
		}
	}
}
