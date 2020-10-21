using Xunit;
using Newtonsoft.Json;
using Criteria.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.CriteriaUnits;
using System.IO;
using Criteria.Enums;

namespace Criteria.JsonConverters.Tests
{
	public class ICriteriaUnitConverterTests
	{
		private CriteriaUnitSimple _criteriaUnitSimple; 
		private string _criteriaUnitSimpleJson;
		private CriteriaUnitCompound _criteriaUnitCompound; 
		private string _criteriaUnitCompoundJson; 


		public ICriteriaUnitConverterTests()
		{
			_criteriaUnitSimple = new CriteriaUnitSimple(DataType.String, "Test", true);
			_criteriaUnitSimpleJson = _criteriaUnitSimple.Serialize();
			_criteriaUnitCompound = new CriteriaUnitCompound(DataType.Numeric, new List<ICriteriaUnit>()
			{
				new CriteriaUnitSimple(DataType.Numeric, "1", true),
				new CriteriaUnitSimple(DataType.Numeric, "2", true)
			});
			_criteriaUnitCompoundJson = _criteriaUnitCompound.Serialize();
		}
		//"{\"CriteriaUnitType\":\"simple\",\"DataType\":1,\"Value\":\"Test\"}";
		//private string _criteriaUnitCompoundJson = "{\"CriteriaUnitType\":\"compound\",\"DataType\":0,\"Value\":\"((1),(2))\",\"CriteriaUnits\":[{\"CriteriaUnitType\":\"simple\",\"DataType\":0,\"Value\":\"1\"},{\"CriteriaUnitType\":\"simple\",\"DataType\":0,\"Value\":\"2\"}]}";


		[Fact()]
		public void Deserialize_CriteriaUnitSimple()
		{
			var converter = new JsonConverter[] { new ICriteriaUnitConverter() };

			ICriteriaUnit criteriaUnit = JsonConvert.DeserializeObject<ICriteriaUnit>(_criteriaUnitSimpleJson, converter);	
			Assert.True(criteriaUnit.CriteriaUnitType == "simple", $"The CriteriaUnitType property of the returned ICriteriaUnit was \"{criteriaUnit.CriteriaUnitType}\" and should have been \"simple\"");
		}

		[Fact()]
		public void Deserialize_CriteriaUnitCompound()
		{
			var converter = new JsonConverter[]	
			{
				new ICriteriaUnitListConverter(),
				new ICriteriaUnitConverter()
			};

			ICriteriaUnit criteriaUnit = JsonConvert.DeserializeObject<ICriteriaUnit>(_criteriaUnitCompoundJson, converter);
			Assert.True(criteriaUnit.CriteriaUnitType == "compound", $"The CriteriaUnitType property of the returned ICriteriaUnit was \"{criteriaUnit.CriteriaUnitType}\" and should have been \"compound\"");
		}
	}
}