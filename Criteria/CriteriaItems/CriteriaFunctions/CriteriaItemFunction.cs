﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.CriteriaExceptions;
using Criteria.CriteriaItems.CriteriaFunctions;
using Criteria.Enums;
using Criteria.JsonConverters;
using Newtonsoft.Json;

namespace Criteria.CriteriaItems.CriteriaFunctions
{

	[JsonConverter(typeof(ICriteriaItemConverter))]
	public class CriteriaItemFunction : ICriteriaItem
	{
		[JsonProperty(PropertyName = "CriteriaItemType")]
		public string CriteriaItemType => "function";

		[JsonProperty(PropertyName = "FunctionScheme")]
		[JsonConverter(typeof(ICriteriaFunctionSchemeConverter))]
		private ICriteriaFunctionScheme _functionScheme;
		[JsonProperty(PropertyName = "FunctionName")]
		public string FunctionName { get; private set; }
		[JsonProperty(PropertyName = "CriteriaItemID")]
		public Guid CriteriaItemID { get; private set; }
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
					string criteriaString = argumentAssignment.CriteriaItem == null ? "NULL" : argumentAssignment.CriteriaItem.SQLValue;
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
					string criteriaString = argumentAssignment.CriteriaItem == null ? "UNASSIGNED" : argumentAssignment.CriteriaItem.EnglishValue;
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

		public CriteriaItemFunction() { }

		public CriteriaItemFunction(string functionName, ICriteriaFunctionScheme functionScheme)
		{
			this.CriteriaItemID = Guid.NewGuid();
			this.FunctionName = functionName;
			this._functionScheme = functionScheme;

			foreach (Argument argument in _functionScheme.Arguments)
			{
				_argumentAssignments.Add(new ArgumentAssignment(argument));
			}
		}

		public CriteriaItemFunction(Guid criteriaItemID, string functionName, ICriteriaFunctionScheme functionScheme) : this(functionName, functionScheme)
		{
			this.CriteriaItemID = criteriaItemID;
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

		public void AssignArgument(Guid argumentAssignmentID, ICriteriaItem criteriaItem)
		{
			var argumentAssignment = _argumentAssignments.Find(x => x.ArgumentAssignmentID == argumentAssignmentID);
			argumentAssignment.CriteriaItem = criteriaItem;
		}

		public void AssignArgument(string argumentName, ICriteriaItem criteriaItem)
		{
			var argumentAssignment = _argumentAssignments.Find(x => x.Argument.Name == argumentName);
			argumentAssignment.CriteriaItem = criteriaItem;
		}

		//*****************************************************************************
		// ******** PRIVATE METHODS
		//*****************************************************************************

		public static CriteriaItemFunction Deserialize(string criteriaItemJson)
		{
			var settings = new JsonSerializerSettings()
			{
				//TypeNameHandling = TypeNameHandling.All
			};

			return JsonConvert.DeserializeObject<CriteriaItemFunction>(criteriaItemJson, settings);
		}
	}
}