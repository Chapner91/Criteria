using Criteria.CriteriaExceptions;
using Criteria.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	public class ArgumentAssignment : IEquatable<ArgumentAssignment>
	{
		[JsonProperty(PropertyName = "ArgumentAssignmentID")]
		public Guid ArgumentAssignmentID { get; }

		[JsonProperty(PropertyName = "Argument")]
		[JsonConverter(typeof(IArgumentConverter))]
		public IArgument Argument { get; private set; }

		[JsonConverter(typeof(ICriteriaItemConverter))]
		[JsonProperty(PropertyName = "CriteriaItem")]
		private ICriteriaItem _criteriaItem;
		[JsonIgnore]
		public ICriteriaItem CriteriaItem
		{
			get => _criteriaItem;
			set
			{
				if(CriteriaItemTypeMatchesArgumentType(value))
				{
					_criteriaItem = value;
				}
				else
				{
					throw new ArgumentTypeException();
				}
			}
		}

		public ArgumentAssignment() { }

		public ArgumentAssignment(IArgument argument)
		{
			this.ArgumentAssignmentID = Guid.NewGuid();
			this.Argument = argument;
		}

		public ArgumentAssignment(Guid argumentAssignmentID, IArgument argument) : this(argument)
		{
			this.ArgumentAssignmentID = argumentAssignmentID;
		}

		public ArgumentAssignment(IArgument argument, ICriteriaItem criteriaItem) : this (argument)
		{
			this.CriteriaItem = criteriaItem;
		}

		public ArgumentAssignment(Guid argumentAssignmentID, IArgument argument, ICriteriaItem criteriaItem) : this (argumentAssignmentID, argument)
		{
			this.CriteriaItem = criteriaItem;
		}

		public ArgumentAssignment Copy()
		{
			return new ArgumentAssignment(Argument.Copy(), CriteriaItem.Copy());
		}

		public override bool Equals(object obj)
		{
			var that = obj as ArgumentAssignment;
			if (that == null)
			{
				return false;
			}
			else
			{
				return (this.Argument.Equals(that.Argument) && this.CriteriaItem.Equals(that.CriteriaItem));
			}
		}

		private bool ArgumentTypeMatchesCriteriaItemType(IArgument argument)
		{
			if(CriteriaItem == null)
			{
				return true;
			}
			else if (CriteriaItem.ReturnDataType == argument.DataType)
			{
				if (argument.RequiresSingleValue == false || (argument.RequiresSingleValue == true && CriteriaItem.ReturnsSingleValue == true))
				{
					return true;
				}
				else return false;
			}
			else return false;
		}

		private bool CriteriaItemTypeMatchesArgumentType(ICriteriaItem criteriaItem)
		{
			if (criteriaItem.ReturnDataType == Argument.DataType)
			{
				if (Argument.RequiresSingleValue == false || (Argument.RequiresSingleValue == true && criteriaItem.ReturnsSingleValue == true))
				{
					return true;
				}
				else return false;
			}
			else return false;
		}

		public bool Equals(ArgumentAssignment that)
		{
			return that != null &&
				   EqualityComparer<IArgument>.Default.Equals(Argument, that.Argument) &&
				   EqualityComparer<ICriteriaItem>.Default.Equals(CriteriaItem, that.CriteriaItem);
		}

		public override int GetHashCode()
		{
			var hashCode = 1392270585;
			hashCode = hashCode * -1521134295 + EqualityComparer<IArgument>.Default.GetHashCode(Argument);
			hashCode = hashCode * -1521134295 + EqualityComparer<ICriteriaItem>.Default.GetHashCode(CriteriaItem);
			return hashCode;
		}
	}
}
