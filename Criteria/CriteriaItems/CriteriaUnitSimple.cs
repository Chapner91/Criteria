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

namespace Criteria.CriteriaUnits
{
	[JsonConverter(typeof(ICriteriaUnitConverter))]
	public class CriteriaUnitSimple : ICriteriaUnit, IEquatable<CriteriaUnitSimple>
	{
		private string _value;

		[JsonProperty(PropertyName = "CriteriaUnitType")]
		public string CriteriaUnitType => "simple";

		[JsonProperty(PropertyName = "CriteriaUnitID")]
		public Guid CriteriaUnitID { get; private set; }

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
					throw new CriteriaUnitTypeMismatchException(ReturnDataType, value);
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

		public CriteriaUnitSimple()	{ }

		public CriteriaUnitSimple(string criteriaUnitJson)
		{
			CriteriaUnitSimple that = Deserialize(criteriaUnitJson);
			
			this.CriteriaUnitID = that.CriteriaUnitID;
			this.ReturnDataType = that.ReturnDataType;
			this.Value = that.Value;
			this.IsValueLiteral = that.IsValueLiteral;
		}

		public CriteriaUnitSimple(DataType dataType, string value, bool isValueLiteral)
		{
			this.CriteriaUnitID = Guid.NewGuid();
			this.ReturnDataType = dataType;
			this.Value = value;
			this.IsValueLiteral = isValueLiteral;
		}

		public CriteriaUnitSimple(Guid criteriaUnitID, DataType dataType, string value, bool isValueLiteral) : this(dataType, value, isValueLiteral)
		{
			this.CriteriaUnitID = criteriaUnitID;
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

		public ICriteriaUnit Copy()
		{
			return new CriteriaUnitSimple(ReturnDataType, Value, IsValueLiteral);
		}

		public bool Equals(CriteriaUnitSimple that)
		{
			return this.Equals((object)that);
		}

		public override bool Equals(object obj)
		{
			CriteriaUnitSimple that = obj as CriteriaUnitSimple;
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

		private static CriteriaUnitSimple Deserialize(string criteriaUnitJson)
		{
			var settings = new JsonSerializerSettings()
			{
				//TypeNameHandling = TypeNameHandling.All
			};

			return JsonConvert.DeserializeObject<CriteriaUnitSimple>(criteriaUnitJson, settings);
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
