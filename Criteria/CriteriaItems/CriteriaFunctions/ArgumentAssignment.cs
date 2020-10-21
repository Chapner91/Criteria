using Criteria.CriteriaExceptions;
using Criteria.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaUnits.CriteriaFunctions
{
	public class ArgumentAssignment : IEquatable<ArgumentAssignment>
	{
		[JsonProperty(PropertyName = "ArgumentAssignmentID")]
		public Guid ArgumentAssignmentID { get; }

		[JsonProperty(PropertyName = "Argument")]
		[JsonConverter(typeof(IArgumentConverter))]
		public IArgument Argument { get; private set; }

		[JsonConverter(typeof(ICriteriaUnitConverter))]
		[JsonProperty(PropertyName = "CriteriaUnit")]
		private ICriteriaUnit _criteriaUnit;
		[JsonIgnore]
		public ICriteriaUnit CriteriaUnit
		{
			get => _criteriaUnit;
			set
			{
				if(CriteriaUnitTypeMatchesArgumentType(value))
				{
					_criteriaUnit = value;
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

		public ArgumentAssignment(IArgument argument, ICriteriaUnit criteriaUnit) : this (argument)
		{
			this.CriteriaUnit = criteriaUnit;
		}

		public ArgumentAssignment(Guid argumentAssignmentID, IArgument argument, ICriteriaUnit criteriaUnit) : this (argumentAssignmentID, argument)
		{
			this.CriteriaUnit = criteriaUnit;
		}

		public ArgumentAssignment Copy()
		{
			return new ArgumentAssignment(Argument.Copy(), CriteriaUnit.Copy());
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
				return (this.Argument.Equals(that.Argument) && this.CriteriaUnit.Equals(that.CriteriaUnit));
			}
		}

		private bool ArgumentTypeMatchesCriteriaUnitType(IArgument argument)
		{
			if(CriteriaUnit == null)
			{
				return true;
			}
			else if (CriteriaUnit.ReturnDataType == argument.DataType)
			{
				if (argument.RequiresSingleValue == false || (argument.RequiresSingleValue == true && CriteriaUnit.ReturnsSingleValue == true))
				{
					return true;
				}
				else return false;
			}
			else return false;
		}

		private bool CriteriaUnitTypeMatchesArgumentType(ICriteriaUnit criteriaUnit)
		{
			if (criteriaUnit.ReturnDataType == Argument.DataType)
			{
				if (Argument.RequiresSingleValue == false || (Argument.RequiresSingleValue == true && criteriaUnit.ReturnsSingleValue == true))
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
				   EqualityComparer<ICriteriaUnit>.Default.Equals(CriteriaUnit, that.CriteriaUnit);
		}

		public override int GetHashCode()
		{
			var hashCode = 1392270585;
			hashCode = hashCode * -1521134295 + EqualityComparer<IArgument>.Default.GetHashCode(Argument);
			hashCode = hashCode * -1521134295 + EqualityComparer<ICriteriaUnit>.Default.GetHashCode(CriteriaUnit);
			return hashCode;
		}
	}
}
