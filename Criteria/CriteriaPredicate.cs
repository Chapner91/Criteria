using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriteriaHelper
{
	public class CriteriaPredicate
	{
		private static Dictionary<CriteriaItemOperator, string> _OperatorSQLTranslator = new Dictionary<CriteriaItemOperator, string>
		{
			{ CriteriaItemOperator.Equal				, "{leftSide} = {rightSide}"	},
			{ CriteriaItemOperator.NotEqual				, "{leftSide} != {rightSide}"  },
			{ CriteriaItemOperator.LessThan				, "{leftSide} < {rightSide}"   },
			{ CriteriaItemOperator.LessThanOrEqual		, "{leftSide} <= {rightSide}"  },
			{ CriteriaItemOperator.GreaterThan          , "{leftSide} > {rightSide}"  },
			{ CriteriaItemOperator.GreaterThanOrEqual   , "{leftSide} >= {rightSide}"  },
			{ CriteriaItemOperator.InList				, "{leftSide} IN ( {rightSide} )" },
			{ CriteriaItemOperator.NotInList            , "{leftSide} NOT IN ( {rightSide} )" }
		};

		private static Dictionary<CriteriaItemOperator, string> _OperatorEnglishTranslator = new Dictionary<CriteriaItemOperator, string>
		{
			{ CriteriaItemOperator.Equal                , "{leftSide} is equal to {rightSide}"   },
			{ CriteriaItemOperator.NotEqual             , "{leftSide} is not equal to {rightSide}"  },
			{ CriteriaItemOperator.LessThan             , "{leftSide} is less than {rightSide}"   },
			{ CriteriaItemOperator.LessThanOrEqual      , "{leftSide} is less than or equal to {rightSide}"  },
			{ CriteriaItemOperator.GreaterThan          , "{leftSide} is greater than {rightSide}"   },
			{ CriteriaItemOperator.GreaterThanOrEqual   , "{leftSide} is greater than or equal to {rightSide}"  },
			{ CriteriaItemOperator.InList               , "{leftSide} is in the list ( {rightSide} )" },
			{ CriteriaItemOperator.NotInList            , "{leftSide} is not in the list ( {rightSide} )" }
		};

		private string _criteriaItemJson { get; set; }

		[JsonProperty(PropertyName = "LeftSide")]
		public string LeftSide { get; set; }

		[JsonProperty(PropertyName = "CriteriaItemOperator")]
		public CriteriaItemOperator CriteriaItemOperator { get; set; }

		[JsonProperty(PropertyName = "RightSide")]
		public string RightSide { get; set; }


		//------------------------------------------------------------------------------------
		//	CONSTRUCTORS
		//------------------------------------------------------------------------------------

		public CriteriaPredicate() {	}

		public CriteriaPredicate(string json)
		{
			_criteriaItemJson = json;

			var serializer = new JsonSerializer();

			using (var reader = new StringReader(json))
			using (var jsonReader = new JsonTextReader(reader))
			{
				var criteriaItemFromJson = serializer.Deserialize<CriteriaPredicate>(jsonReader);

				RightSide = criteriaItemFromJson.RightSide;
				CriteriaItemOperator = criteriaItemFromJson.CriteriaItemOperator;
				LeftSide = criteriaItemFromJson.LeftSide;
			}			
		}

		public CriteriaPredicate(string leftSide, CriteriaItemOperator criteriaItemOperator, string rightSide)
		{
			LeftSide = leftSide;
			CriteriaItemOperator = criteriaItemOperator;
			RightSide = rightSide;
		}

		//------------------------------------------------------------------------------------
		//	Methods
		//------------------------------------------------------------------------------------

		public string GetCriteriaPredicateEnglish()
		{
			string result = $"( {_OperatorEnglishTranslator[CriteriaItemOperator].Replace("{leftSide}", LeftSide).Replace("{rightSide}", RightSide)} )";
			return result;
			//return $"( {LeftSide} {_OperatorEnglishTranslator[CriteriaItemOperator]} {RightSide} )";
		}

		public string GetCriteriaPredicateSQL()
		{
			return $"( {_OperatorSQLTranslator[CriteriaItemOperator].Replace("{leftSide}", LeftSide).Replace("{rightSide}", RightSide)} )"; ;
		}

	}

	public enum CriteriaItemOperator
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
