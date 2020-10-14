using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.Enums;
using Newtonsoft.Json;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	public class CriteriaFunctionOneArgument : ICriteriaFunction
	{
		[JsonProperty(PropertyName = "CriteriaItemType")]
		public string CriteriaItemType => throw new NotImplementedException();
		[JsonProperty(PropertyName = "CriteriaItemFunctionType")]
		public string CriteriaItemFunctionType => throw new NotImplementedException();
		[JsonProperty(PropertyName = "CriteriaItemID")]
		public Guid CriteriaItemID => throw new NotImplementedException();
		[JsonProperty(PropertyName = "ReturnDataType")]
		public DataType ReturnDataType { get => DataType.Null; set => throw new NotImplementedException(); }
		[JsonProperty(PropertyName = "FunctionName")]
		public string FunctionName => throw new NotImplementedException();

		[JsonIgnore]
		public string Value => throw new NotImplementedException();
		[JsonIgnore]
		public bool ReturnsSingleValue { get; private set; }
		[JsonIgnore]
		public string SQLValue => throw new NotImplementedException();
		[JsonIgnore]
		public string EnglishValue => throw new NotImplementedException();


		//*****************************************************************************
		// ******** CONSTRUCTORS
		//*****************************************************************************

		public CriteriaFunctionOneArgument(string functionName, ICriteriaItem criteriaItem)
		{

		}

		public CriteriaFunctionOneArgument(Guid criteriaItemID, string functionName, ICriteriaItem criteriaItem)
		{ 

		}

		public string Serialize()
		{
			throw new NotImplementedException();
		}
	}
}
