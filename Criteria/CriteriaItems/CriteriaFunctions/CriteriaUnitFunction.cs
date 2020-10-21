using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.CriteriaExceptions;
using Criteria.CriteriaUnits.CriteriaFunctions;
using Criteria.Enums;
using Criteria.JsonConverters;
using Newtonsoft.Json;

namespace Criteria.CriteriaUnits.CriteriaFunctions
{

	[JsonConverter(typeof(ICriteriaUnitConverter))]
	public class CriteriaUnitFunction : ICriteriaUnit, IEquatable<CriteriaUnitFunction>
	{
		[JsonProperty(PropertyName = "CriteriaUnitType")]
		public string CriteriaUnitType => "function";

		[JsonProperty(PropertyName = "FunctionScheme")]
		[JsonConverter(typeof(ICriteriaFunctionSchemeConverter))]
		private ICriteriaFunctionScheme _functionScheme;
		[JsonProperty(PropertyName = "FunctionName")]
		public string FunctionName { get; private set; }
		[JsonProperty(PropertyName = "CriteriaUnitID")]
		public Guid CriteriaUnitID { get; private set; }
		[JsonIgnore]
		public DataType ReturnDataType => _functionScheme.ReturnDataType;
		[JsonIgnore]
		public bool ReturnsSingleValue => _functionScheme.ReturnsSingleValue;
		public string SQLValue
		{
			get
			{
				string result = _functionScheme.SQLTranslationString;
				foreach(ArgumentAssignment argumentAssignment in _argumentAssignments)
				{
					string criteriaString = argumentAssignment.CriteriaUnit == null ? "NULL" : argumentAssignment.CriteriaUnit.SQLValue;
					string translatorIndex = $"{{{argumentAssignment.Argument.Name}}}";
					result = result.Replace($"{{{argumentAssignment.Argument.Name}}}", criteriaString);
				}
				return result;
			}
		}
		public string EnglishValue
		{
			get
			{
				string result = _functionScheme.EnglishTranslationString;
				foreach (ArgumentAssignment argumentAssignment in _argumentAssignments)
				{
					string criteriaString = argumentAssignment.CriteriaUnit == null ? "UNASSIGNED" : argumentAssignment.CriteriaUnit.EnglishValue;
					string translatorIndex = $"{{{argumentAssignment.Argument.Name}}}";
					result = result.Replace($"{{{argumentAssignment.Argument.Name}}}", criteriaString);
				}
				return result;
			}
		}
		[JsonProperty(PropertyName = "ArgumentAssignments")]
		private List<ArgumentAssignment> _argumentAssignments = new List<ArgumentAssignment>();

		[JsonIgnore]
		public IEnumerable<ArgumentAssignment> ArgumentAssignments { get => _argumentAssignments; }


		//*****************************************************************************
		// ******** CONSTRUCTORS
		//*****************************************************************************

		public CriteriaUnitFunction() { }

		public CriteriaUnitFunction(string functionName, ICriteriaFunctionScheme functionScheme)
		{
			this.CriteriaUnitID = Guid.NewGuid();
			this.FunctionName = functionName;
			this._functionScheme = functionScheme;

			foreach (Argument argument in _functionScheme.Arguments)
			{
				_argumentAssignments.Add(new ArgumentAssignment(argument));
			}
		}

		public CriteriaUnitFunction(Guid criteriaUnitID, string functionName, ICriteriaFunctionScheme functionScheme) : this(functionName, functionScheme)
		{
			this.CriteriaUnitID = criteriaUnitID;
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


		public void AssignArgument(Guid argumentAssignmentID, ICriteriaUnit criteriaUnit)
		{
			var argumentAssignment = _argumentAssignments.Find(x => x.ArgumentAssignmentID == argumentAssignmentID);
			argumentAssignment.CriteriaUnit = criteriaUnit;
		}

		public void AssignArgument(string argumentName, ICriteriaUnit criteriaUnit)
		{
			var argumentAssignment = _argumentAssignments.Find(x => x.Argument.Name == argumentName);
			argumentAssignment.CriteriaUnit = criteriaUnit;
		}

		public bool Equals(CriteriaUnitFunction that)
		{
			return that != null &&
				   this._functionScheme.Equals(that._functionScheme) &&
				   FunctionName == that.FunctionName &&
				   (this._argumentAssignments.Count == that._argumentAssignments.Count && this._argumentAssignments.All(that._argumentAssignments.Contains));
		}


		public ICriteriaUnit Copy()
		{
			var NewArgumentAssignments = new List<ArgumentAssignment>();
			foreach (ArgumentAssignment argAssignment in _argumentAssignments)
			{
				NewArgumentAssignments.Add(argAssignment.Copy());
			}
			var criteriaUnitFunction = new CriteriaUnitFunction(FunctionName, _functionScheme.Copy());
			criteriaUnitFunction._argumentAssignments = NewArgumentAssignments;
			return criteriaUnitFunction;
		}

		public override int GetHashCode()
		{
			var hashCode = 1891270760;
			hashCode = hashCode * -1521134295 + EqualityComparer<ICriteriaFunctionScheme>.Default.GetHashCode(_functionScheme);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FunctionName);
			hashCode = hashCode * -1521134295 + EqualityComparer<List<ArgumentAssignment>>.Default.GetHashCode(_argumentAssignments);
			return hashCode;
		}

		//*****************************************************************************
		// ******** PRIVATE METHODS
		//*****************************************************************************

		public static CriteriaUnitFunction Deserialize(string criteriaUnitJson)
		{
			var settings = new JsonSerializerSettings()
			{
				//TypeNameHandling = TypeNameHandling.All
			};

			return JsonConvert.DeserializeObject<CriteriaUnitFunction>(criteriaUnitJson, settings);
		}
	}
}
