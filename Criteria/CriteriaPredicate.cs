using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public class CriteriaPredicate
	{
		private static Dictionary<CriteriaUnitOperator, string> _OperatorSQLTranslator = new Dictionary<CriteriaUnitOperator, string>
		{
			{ CriteriaUnitOperator.Equal				, "{leftSide} = {rightSide}" },
			{ CriteriaUnitOperator.NotEqual				, "{leftSide} != {rightSide}" },
			{ CriteriaUnitOperator.LessThan				, "{leftSide} < {rightSide}" },
			{ CriteriaUnitOperator.LessThanOrEqual		, "{leftSide} <= {rightSide}" },
			{ CriteriaUnitOperator.GreaterThan          , "{leftSide} > {rightSide}" },
			{ CriteriaUnitOperator.GreaterThanOrEqual   , "{leftSide} >= {rightSide}" },
			{ CriteriaUnitOperator.InList				, "{leftSide} IN ( {rightSide} )" },
			{ CriteriaUnitOperator.NotInList            , "{leftSide} NOT IN ( {rightSide} )" }
		};

		private static Dictionary<CriteriaUnitOperator, string> _OperatorEnglishTranslator = new Dictionary<CriteriaUnitOperator, string>
		{
			{ CriteriaUnitOperator.Equal                , "{leftSide} is equal to {rightSide}"   },
			{ CriteriaUnitOperator.NotEqual             , "{leftSide} is not equal to {rightSide}"  },
			{ CriteriaUnitOperator.LessThan             , "{leftSide} is less than {rightSide}"   },
			{ CriteriaUnitOperator.LessThanOrEqual      , "{leftSide} is less than or equal to {rightSide}"  },
			{ CriteriaUnitOperator.GreaterThan          , "{leftSide} is greater than {rightSide}"   },
			{ CriteriaUnitOperator.GreaterThanOrEqual   , "{leftSide} is greater than or equal to {rightSide}"  },
			{ CriteriaUnitOperator.InList               , "{leftSide} is in the list ( {rightSide} )" },
			{ CriteriaUnitOperator.NotInList            , "{leftSide} is not in the list ( {rightSide} )" }
		};

		private string _criteriaUnitJson { get; set; }

		[JsonProperty(PropertyName = "LeftSide")]
		public string LeftSide { get; set; }

		[JsonProperty(PropertyName = "CriteriaUnitOperator")]
		public CriteriaUnitOperator CriteriaUnitOperator { get; set; }

		[JsonProperty(PropertyName = "RightSide")]
		public string RightSide { get; set; }


		//------------------------------------------------------------------------------------
		//	CONSTRUCTORS
		//------------------------------------------------------------------------------------

		public CriteriaPredicate() {	}

		public CriteriaPredicate(string json)
		{
			_criteriaUnitJson = json;

			var serializer = new JsonSerializer();

			using (var reader = new StringReader(json))
			using (var jsonReader = new JsonTextReader(reader))
			{
				var criteriaUnitFromJson = serializer.Deserialize<CriteriaPredicate>(jsonReader);

				RightSide = criteriaUnitFromJson.RightSide;
				CriteriaUnitOperator = criteriaUnitFromJson.CriteriaUnitOperator;
				LeftSide = criteriaUnitFromJson.LeftSide;
			}			
		}

		public CriteriaPredicate(string leftSide, CriteriaUnitOperator criteriaUnitOperator, string rightSide)
		{
			LeftSide = leftSide;
			CriteriaUnitOperator = criteriaUnitOperator;
			RightSide = rightSide;
		}

		//------------------------------------------------------------------------------------
		//	Methods
		//------------------------------------------------------------------------------------

		public string GetCriteriaPredicateEnglish()
		{
			return $"( {_OperatorEnglishTranslator[CriteriaUnitOperator].Replace("{leftSide}", LeftSide).Replace("{rightSide}", RightSide)} )";
		}

		public string GetCriteriaPredicateSQL()
		{
			return $"( {_OperatorSQLTranslator[CriteriaUnitOperator].Replace("{leftSide}", LeftSide).Replace("{rightSide}", RightSide)} )"; ;
		}

	}

	public enum CriteriaUnitOperator
	{
		Equal,
		NotEqual,
		LessThan,
		LessThanOrEqual,
		GreaterThan,
		GreaterThanOrEqual,
		InList,
		NotInList
	}
}
