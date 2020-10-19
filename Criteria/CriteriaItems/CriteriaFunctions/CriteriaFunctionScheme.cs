using Criteria.CriteriaExceptions;
using Criteria.Enums;
using Criteria.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	[JsonConverter(typeof(ICriteriaFunctionSchemeConverter))]
	public class CriteriaFunctionScheme : ICriteriaFunctionScheme
	{

		[JsonProperty(PropertyName = "CriteriaFunctionSchemeID")]
		public Guid CriteriaFunctionSchemeID { get; private set; }
		[JsonProperty(PropertyName = "FunctionSchemeName")]
		public string FunctionSchemeName { get; private set; }
		//public string CriteriaFunctionType { get; private set; }
		[JsonProperty(PropertyName = "Arguments")]
		private List<Argument> _arguments = new List<Argument>();
		[JsonIgnore]
		public IEnumerable<Argument> Arguments
		{
			get { return _arguments; }
			private set { _arguments = value.ToList(); }
		}
		[JsonProperty(PropertyName = "ReturnsSingleValue")]
		public bool ReturnsSingleValue { get; set; }
		[JsonProperty(PropertyName = "ReturnDataType")]
		public DataType ReturnDataType { get; set; }
		[JsonProperty(PropertyName = "SQLTranslationString")]
		public string SQLTranslationString { get; private set; }
		[JsonProperty(PropertyName = "EnglishTranslationString")]
		public string EnglishTranslationString { get; private set; }

		//************************************************************************************
		// CONSTRUCTORS
		//************************************************************************************

		public CriteriaFunctionScheme(Guid criteriaFunctionSchemeID, string functionSchemeName, IEnumerable<Argument> arguments,
			bool returnsSingleValue, DataType returnDataType, string sqlTranslationString, string englishTranslationString)
			: this(functionSchemeName, arguments, returnsSingleValue, returnDataType, sqlTranslationString, englishTranslationString)
		{
			CriteriaFunctionSchemeID = criteriaFunctionSchemeID;
			//FunctionSchemeName = functionSchemeName;
			//_arguments = arguments.ToList();
			//ReturnsSingleValue = returnsSingleValue;
			//ReturnDataType = returnDataType;
			//SQLTranslationString = sqlTranslationString;
			//EnglishTranslationString = englishTranslationString;
		}

		public CriteriaFunctionScheme(string functionSchemeName, IEnumerable<Argument> arguments,
			bool returnsSingleValue, DataType returnDataType, string sqlTranslationString, string englishTranslationString)
		{
			CriteriaFunctionSchemeID = Guid.NewGuid();
			FunctionSchemeName = functionSchemeName;
			_arguments = arguments.ToList();
			ReturnsSingleValue = returnsSingleValue;
			ReturnDataType = returnDataType;
			SQLTranslationString = sqlTranslationString;
			EnglishTranslationString = englishTranslationString;
		}

		public CriteriaFunctionScheme()
		{
		}

		//************************************************************************************
		// PUBLIC METHODS
		//************************************************************************************

		public void	AddArgument(Argument argument)
		{
			_arguments.Add(argument);
		}
		public void RemoveArgument(Guid argumentID)
		{
			while (_arguments.Exists(x => x.ArgumentID == argumentID))
			{
				_arguments.RemoveAt(_arguments.FindIndex(x => x.ArgumentID == argumentID));
			}
		}
		public void SetTranslationString(Translator TranslatorToUpdate, string value)
		{
			List<Argument> missingArguments = new List<Argument>();
			foreach (Argument argument in _arguments)
			{
				string searchString = $"{{{argument.Name}}}";
				if (!value.Contains(searchString))
				{
					missingArguments.Add(argument);
				}
			}

			if (missingArguments.Count > 0)
			{
				string missingArgumentsString = "";
				foreach(Argument argument in missingArguments)
				{
					missingArgumentsString += missingArgumentsString == "" ? "" : ",";
					missingArgumentsString += $"{{{argument.Name}}}";
				}
				throw new CriteriaFunctionSchemeUnmappedArgumentException(value, missingArguments, $"The translator string is missing mappings for the following arguments {missingArgumentsString}");
			}
			else
			{
				switch (TranslatorToUpdate)
				{
					case Translator.English: EnglishTranslationString = value; break;
					case Translator.SQL: SQLTranslationString = value; break;
				}
			}
		}
	}

	public enum Translator
	{
		SQL,
		English
	}
}

