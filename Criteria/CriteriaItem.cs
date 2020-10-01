using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriteriaHelper
{
	public class CriteriaItem
	{
		private static Dictionary<CriteriaItemOperator, string> _OperatorSQLTranslator = new Dictionary<CriteriaItemOperator, string>
		{
			{ CriteriaItemOperator.equal				, "="	},
			{ CriteriaItemOperator.lessThan				, "<"   },
			{ CriteriaItemOperator.lessThanOrEqual		, "<="  },
			{ CriteriaItemOperator.greaterThan          , ">"	},
			{ CriteriaItemOperator.greaterThanOrEqual   , ">="  },
			{ CriteriaItemOperator.notEqual				, "!="  }
		};

		private static Dictionary<CriteriaItemOperator, string> _OperatorEnglishTranslator = new Dictionary<CriteriaItemOperator, string>
		{
			{ CriteriaItemOperator.equal                , "is equal to"   },
			{ CriteriaItemOperator.lessThan             , "is less than"   },
			{ CriteriaItemOperator.lessThanOrEqual      , "is less than or equal to"  },
			{ CriteriaItemOperator.greaterThan          , "is greater than"   },
			{ CriteriaItemOperator.greaterThanOrEqual   , "is greater than or equal to"  },
			{ CriteriaItemOperator.notEqual             , "is not equal to"  }
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
		public CriteriaItem() {	}

		public CriteriaItem(string json)
		{
			_criteriaItemJson = json;

			var serializer = new JsonSerializer();

			using (var reader = new StringReader(json))
			using (var jsonReader = new JsonTextReader(reader))
			{
				var criteriaItemFromJson = serializer.Deserialize<CriteriaItem>(jsonReader);

				RightSide = criteriaItemFromJson.RightSide;
				CriteriaItemOperator = criteriaItemFromJson.CriteriaItemOperator;
				LeftSide = criteriaItemFromJson.LeftSide;
			}			
		}

		public CriteriaItem(string leftSide, CriteriaItemOperator criteriaItemOperator, string rightSide)
		{
			LeftSide = leftSide;
			CriteriaItemOperator = criteriaItemOperator;
			RightSide = rightSide;
		}

		//------------------------------------------------------------------------------------
		//	Methods
		//------------------------------------------------------------------------------------

		public string GetCriteriaItemEnglish()
		{
			return $"( {LeftSide} {_OperatorEnglishTranslator[CriteriaItemOperator]} {RightSide} )";
		}

		public string GetCriteriaItemSQL()
		{
			return $"( {LeftSide} {_OperatorSQLTranslator[CriteriaItemOperator]} {RightSide} )";
		}

	}

	public enum CriteriaItemOperator
	{
		equal,
		lessThan,
		lessThanOrEqual,
		greaterThan,
		greaterThanOrEqual,
		notEqual		
	}
}
