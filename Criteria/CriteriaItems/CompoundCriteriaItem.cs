using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.CriteriaExceptions;
using Criteria.Enums;
using Newtonsoft.Json;

namespace Criteria.CriteriaItems
{
	public class CompoundCriteriaItem : ICriteriaItem
	{
		public DataType DataType { get; set; }
		public string Value
		{
			get
			{
				string result = "(";
				int i = 0;
				foreach(ICriteriaItem criteriaItem in CriteriaItems)
				{
					if (i > 0)
					{
						result += ",";
					}
					result += $"({criteriaItem.Value})";
					i++;
				}
				result += ")";
				return result;
			}
		}
		private List<ICriteriaItem> _criteriaItems = new List<ICriteriaItem>();
		public List<ICriteriaItem> CriteriaItems
		{
			get => _criteriaItems;
			set
			{
				foreach (ICriteriaItem criteriaItem in value)
				{
					if (!ChildIsCorrectDataType(criteriaItem))
					{
						throw new CriteriaItemTypeMismatchException(DataType, criteriaItem);
					}
				}
				_criteriaItems = value;
			}
		}

		//*****************************************************************************
		// ******** CONSTRUCTORS
		//*****************************************************************************

		public CompoundCriteriaItem() { }

		public CompoundCriteriaItem(string criteriaItemJson)
		{
			CompoundCriteriaItem that = Deserialize(criteriaItemJson);
			this.DataType = that.DataType;
			this.CriteriaItems = that.CriteriaItems;
		}

		public CompoundCriteriaItem(DataType dataType, List<ICriteriaItem> criteriaItems)
		{
			this.DataType = dataType;
			this.CriteriaItems = criteriaItems;
		}

		//*****************************************************************************
		// ******** PUBLIC METHODS
		//*****************************************************************************

		public string Serialize()
		{
			var settings = new JsonSerializerSettings()
			{
				TypeNameHandling = TypeNameHandling.Objects
			};

			return JsonConvert.SerializeObject(this, settings);
		}

		public override bool Equals(object obj)
		{
			var that = obj as CompoundCriteriaItem;
			if (that == null)
			{
				return false;
			}
			else
			{
				return 
				(
					this.DataType == that.DataType && 
					(
						this.CriteriaItems.All(that.CriteriaItems.Contains) && 
						that.CriteriaItems.All(this.CriteriaItems.Contains)
					)
				);
			}
		}

		public override int GetHashCode()
		{
			var hashCode = 1365839669;
			hashCode = hashCode * -1521134295 + DataType.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<List<ICriteriaItem>>.Default.GetHashCode(CriteriaItems);
			return hashCode;
		}

		//*****************************************************************************
		// ******** PRIVATE METHODS
		//*****************************************************************************

		private static CompoundCriteriaItem Deserialize(string criteriaItemJson)
		{
			var settings = new JsonSerializerSettings()
			{
				TypeNameHandling = TypeNameHandling.Objects
			};

			return (CompoundCriteriaItem)JsonConvert.DeserializeObject(criteriaItemJson, settings);
		}

		private bool ChildIsCorrectDataType(ICriteriaItem criteriaItem)
		{
			if(criteriaItem.DataType == DataType)
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
