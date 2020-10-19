using System;
using System.Collections.Generic;
using System.Linq;
using Criteria.CriteriaExceptions;
using Criteria.Enums;
using Criteria.JsonConverters;
using Newtonsoft.Json;

namespace Criteria.CriteriaItems
{
	[JsonConverter(typeof(ICriteriaItemConverter))]
	public class CriteriaItemCompound : ICriteriaItem
	{
		public string CriteriaItemType => "compound";

		[JsonProperty(PropertyName = "CriteriaItemID")]
		public Guid CriteriaItemID { get; private set; }

		[JsonProperty(PropertyName = "ReturnDataType")]
		public DataType ReturnDataType { get; set; }

		private List<ICriteriaItem> _criteriaItems = new List<ICriteriaItem>();

		[JsonConverter(typeof(ICriteriaItemListConverter))]
		[JsonProperty(PropertyName = "CriteriaItems")]
		public IEnumerable<ICriteriaItem> CriteriaItems
		{
			get => _criteriaItems;
			set
			{
				foreach (ICriteriaItem criteriaItem in value)
				{
					if (!ValueIsCorrectDataType(criteriaItem))
					{
						throw new CriteriaItemTypeMismatchException(ReturnDataType, criteriaItem);
					}
				}
				_criteriaItems = value.ToList< ICriteriaItem>();
			}
		}

		[JsonIgnore]
		public bool ReturnsSingleValue => CriteriaItems.Count() > 1 ? false : true;

		[JsonIgnore]
		public string SQLValue
		{
			get
			{
				string result = "(";
				int i = 0;
				foreach (ICriteriaItem criteriaItem in CriteriaItems)
				{
					if (i > 0)
					{
						result += ",";
					}
					result += $"{criteriaItem.SQLValue}";
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
				foreach (ICriteriaItem criteriaItem in CriteriaItems)
				{
					if (i > 0)
					{
						result += ",";
					}
					result += $"{criteriaItem.EnglishValue}";
					i++;
				}
				result += ")";
				return result;
			}
		}

		//*****************************************************************************
		// ******** CONSTRUCTORS
		//*****************************************************************************

		public CriteriaItemCompound() { }

		public CriteriaItemCompound(string criteriaItemJson)
		{
			CriteriaItemCompound that = Deserialize(criteriaItemJson);
			this.CriteriaItemID = that.CriteriaItemID;
			this.ReturnDataType = that.ReturnDataType;
			this.CriteriaItems = that.CriteriaItems;
		}

		public CriteriaItemCompound(DataType dataType, List<ICriteriaItem> criteriaItems)
		{
			this.CriteriaItemID = Guid.NewGuid();
			this.ReturnDataType = dataType;
			this.CriteriaItems = criteriaItems;
		}

		public CriteriaItemCompound(Guid criteriaItemId, DataType dataType, List<ICriteriaItem> criteriaItems) : this(dataType, criteriaItems)
		{
			this.CriteriaItemID = criteriaItemId;
			//this.ReturnDataType = dataType;
			//this.CriteriaItems = criteriaItems;
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

		public void AddCriteriaItem(ICriteriaItem criteriaItem)
		{
			if (ValueIsCorrectDataType(criteriaItem))
			{
				_criteriaItems.Add(criteriaItem);
			}
			else
			{
				throw new CriteriaItemTypeMismatchException(ReturnDataType, criteriaItem);
			}
		}

		public void AddCriteriaItem(int index, ICriteriaItem criteriaItem)
		{
			if (ValueIsCorrectDataType(criteriaItem))
			{
				_criteriaItems.Insert(index, criteriaItem);
			}
			else
			{
				throw new CriteriaItemTypeMismatchException(ReturnDataType, criteriaItem);
			}
		}

		public void RemoveCriteriaItem(Guid criteriaItemID)
		{
			while(_criteriaItems.Exists(x => x.CriteriaItemID == criteriaItemID))
			{
				int index = _criteriaItems.FindIndex(x => x.CriteriaItemID == criteriaItemID);
				_criteriaItems.RemoveAt(index);
			}
		}

		public void RemoveCriteriaItem(ICriteriaItem criteriaItem)
		{
			while(_criteriaItems.Contains(criteriaItem))
			{
				_criteriaItems.Remove(criteriaItem);
			}
		}

		public override bool Equals(object obj)
		{
			var that = obj as CriteriaItemCompound;
			if (that == null)
			{
				return false;
			}
			else
			{
				return
				(
					this.CriteriaItemID == that.CriteriaItemID &&
					this.ReturnDataType == that.ReturnDataType &&
					(
						this._criteriaItems.Count == that.CriteriaItems.Count() &&
						this._criteriaItems.SequenceEqual(that.CriteriaItems) 
					)
				);
			}
		}

		public override int GetHashCode()
		{
			var hashCode = 1365839669;
			hashCode = hashCode * -1521134295 + ReturnDataType.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<List<ICriteriaItem>>.Default.GetHashCode(_criteriaItems);
			return hashCode;
		}


		//*****************************************************************************
		// ******** PRIVATE METHODS
		//*****************************************************************************

		private static CriteriaItemCompound Deserialize(string criteriaItemJson)
		{
			var settings = new JsonSerializerSettings()
			{
				//TypeNameHandling = TypeNameHandling.Objects
			};

			return JsonConvert.DeserializeObject<CriteriaItemCompound>(criteriaItemJson, settings);
		}

		private bool ValueIsCorrectDataType(ICriteriaItem criteriaItem)
		{
			if(criteriaItem.ReturnDataType == ReturnDataType)
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
