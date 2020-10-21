using System;
using System.Collections.Generic;
using System.Linq;
using Criteria.CriteriaExceptions;
using Criteria.Enums;
using Criteria.JsonConverters;
using Newtonsoft.Json;

namespace Criteria.CriteriaUnits
{
	[JsonConverter(typeof(ICriteriaUnitConverter))]
	public class CriteriaUnitCompound : ICriteriaUnit, IEquatable<CriteriaUnitCompound>
	{
		public string CriteriaUnitType => "compound";

		[JsonProperty(PropertyName = "CriteriaUnitID")]
		public Guid CriteriaUnitID { get; private set; }

		[JsonProperty(PropertyName = "ReturnDataType")]
		public DataType ReturnDataType { get; set; }

		private List<ICriteriaUnit> _criteriaUnits = new List<ICriteriaUnit>();

		[JsonConverter(typeof(ICriteriaUnitListConverter))]
		[JsonProperty(PropertyName = "CriteriaUnits")]
		public IEnumerable<ICriteriaUnit> CriteriaUnits
		{
			get => _criteriaUnits;
			set
			{
				foreach (ICriteriaUnit criteriaUnit in value)
				{
					if (!ValueIsCorrectDataType(criteriaUnit))
					{
						throw new CriteriaUnitTypeMismatchException(ReturnDataType, criteriaUnit);
					}
				}
				_criteriaUnits = value.ToList< ICriteriaUnit>();
			}
		}

		[JsonIgnore]
		public bool ReturnsSingleValue => CriteriaUnits.Count() > 1 ? false : true;

		[JsonIgnore]
		public string SQLValue
		{
			get
			{
				string result = "(";
				int i = 0;
				foreach (ICriteriaUnit criteriaUnit in CriteriaUnits)
				{
					if (i > 0)
					{
						result += ",";
					}
					result += $"{criteriaUnit.SQLValue}";
					i++;
				}
				result += ")";
				return result;
			}
		}

		[JsonIgnore]
		public string EnglishValue
		{
			get
			{
				string result = "(";
				int i = 0;
				foreach (ICriteriaUnit criteriaUnit in CriteriaUnits)
				{
					if (i > 0)
					{
						result += ",";
					}
					result += $"{criteriaUnit.EnglishValue}";
					i++;
				}
				result += ")";
				return result;
			}
		}

		//*****************************************************************************
		// ******** CONSTRUCTORS
		//*****************************************************************************

		public CriteriaUnitCompound() { }

		public CriteriaUnitCompound(string criteriaUnitJson)
		{
			CriteriaUnitCompound that = Deserialize(criteriaUnitJson);
			this.CriteriaUnitID = that.CriteriaUnitID;
			this.ReturnDataType = that.ReturnDataType;
			this.CriteriaUnits = that.CriteriaUnits;
		}

		public CriteriaUnitCompound(DataType dataType, List<ICriteriaUnit> criteriaUnits)
		{
			this.CriteriaUnitID = Guid.NewGuid();
			this.ReturnDataType = dataType;
			this.CriteriaUnits = criteriaUnits;
		}

		public CriteriaUnitCompound(Guid criteriaUnitId, DataType dataType, List<ICriteriaUnit> criteriaUnits) : this(dataType, criteriaUnits)
		{
			this.CriteriaUnitID = criteriaUnitId;
		}

		//*****************************************************************************
		// ******** PUBLIC METHODS
		//*****************************************************************************

		public string Serialize()
		{
			var settings = new JsonSerializerSettings()
			{
				//TypeNameHandling = TypeNameHandling.Objects
			};

			return JsonConvert.SerializeObject(this, settings);
		}

		public ICriteriaUnit Copy()
		{
			var criteriaUnits = new List<ICriteriaUnit>();
			foreach(ICriteriaUnit criteriaUnit in CriteriaUnits)
			{
				criteriaUnits.Add(criteriaUnit.Copy());
			}
			return new CriteriaUnitCompound(ReturnDataType, criteriaUnits);
		}

		public void AddCriteriaUnit(ICriteriaUnit criteriaUnit)
		{
			if (ValueIsCorrectDataType(criteriaUnit))
			{
				_criteriaUnits.Add(criteriaUnit);
			}
			else
			{
				throw new CriteriaUnitTypeMismatchException(ReturnDataType, criteriaUnit);
			}
		}

		public void AddCriteriaUnit(int index, ICriteriaUnit criteriaUnit)
		{
			if (ValueIsCorrectDataType(criteriaUnit))
			{
				_criteriaUnits.Insert(index, criteriaUnit);
			}
			else
			{
				throw new CriteriaUnitTypeMismatchException(ReturnDataType, criteriaUnit);
			}
		}

		public void RemoveCriteriaUnit(Guid criteriaUnitID)
		{
			while(_criteriaUnits.Exists(x => x.CriteriaUnitID == criteriaUnitID))
			{
				int index = _criteriaUnits.FindIndex(x => x.CriteriaUnitID == criteriaUnitID);
				_criteriaUnits.RemoveAt(index);
			}
		}

		public void RemoveCriteriaUnit(ICriteriaUnit criteriaUnit)
		{
			while(_criteriaUnits.Contains(criteriaUnit))
			{
				_criteriaUnits.Remove(criteriaUnit);
			}
		}

		public bool Equals(CriteriaUnitCompound that)
		{
			return this.Equals((object)that);
		}

		public override bool Equals(object obj)
		{
			var that = obj as CriteriaUnitCompound;
			if (that == null)
			{
				return false;
			}
			else
			{
				return
				(
					this.ReturnDataType == that.ReturnDataType &&
					(
						this._criteriaUnits.Count == that.CriteriaUnits.Count() &&
						this._criteriaUnits.SequenceEqual(that.CriteriaUnits) 
					)
				);
			}
		}

		public override int GetHashCode()
		{
			var hashCode = 1365839669;
			hashCode = hashCode * -1521134295 + ReturnDataType.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<List<ICriteriaUnit>>.Default.GetHashCode(_criteriaUnits);
			return hashCode;
		}


		//*****************************************************************************
		// ******** PRIVATE METHODS
		//*****************************************************************************

		private static CriteriaUnitCompound Deserialize(string criteriaUnitJson)
		{
			var settings = new JsonSerializerSettings()
			{
				//TypeNameHandling = TypeNameHandling.Objects
			};

			return JsonConvert.DeserializeObject<CriteriaUnitCompound>(criteriaUnitJson, settings);
		}

		private bool ValueIsCorrectDataType(ICriteriaUnit criteriaUnit)
		{
			if(criteriaUnit.ReturnDataType == ReturnDataType)
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
