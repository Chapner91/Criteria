using Xunit;
using Newtonsoft.Json;
using Criteria.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.CriteriaItems;
using System.IO;
using Criteria.Enums;

namespace Criteria.JsonConverters.Tests
{
	public class ICriteriaItemConverterTests
	{
		private CriteriaItemSimple _criteriaItemSimple; 
		private string _criteriaItemSimpleJson;
		private CriteriaItemCompound _criteriaItemCompound; 
		private string _criteriaItemCompoundJson; 


		public ICriteriaItemConverterTests()
		{
			_criteriaItemSimple = new CriteriaItemSimple(DataType.String, "Test");
			_criteriaItemSimpleJson = _criteriaItemSimple.Serialize();
			_criteriaItemCompound = new CriteriaItemCompound(DataType.Numeric, new List<ICriteriaItem>()
			{
				new CriteriaItemSimple(DataType.Numeric, "1"),
				new CriteriaItemSimple(DataType.Numeric, "2")
			});
			_criteriaItemCompoundJson = _criteriaItemCompound.Serialize();
		}
		//"{\"CriteriaItemType\":\"simple\",\"DataType\":1,\"Value\":\"Test\"}";
		//private string _criteriaItemCompoundJson = "{\"CriteriaItemType\":\"compound\",\"DataType\":0,\"Value\":\"((1),(2))\",\"CriteriaItems\":[{\"CriteriaItemType\":\"simple\",\"DataType\":0,\"Value\":\"1\"},{\"CriteriaItemType\":\"simple\",\"DataType\":0,\"Value\":\"2\"}]}";


		[Fact()]
		public void Deserialize_CriteriaItemSimple()
		{
			var converter = new JsonConverter[] { new ICriteriaItemConverter() };

			ICriteriaItem criteriaItem = JsonConvert.DeserializeObject<ICriteriaItem>(_criteriaItemSimpleJson, converter);	
			Assert.True(criteriaItem.CriteriaItemType == "simple", $"The CriteriaItemType property of the returned ICriteriaItem was \"{criteriaItem.CriteriaItemType}\" and should have been \"simple\"");
		}

		[Fact()]
		public void Deserialize_CriteriaItemCompound()
		{
			var converter = new JsonConverter[]	
			{
				new ICriteriaItemListConverter(),
				new ICriteriaItemConverter()
			};

			ICriteriaItem criteriaItem = JsonConvert.DeserializeObject<ICriteriaItem>(_criteriaItemCompoundJson, converter);
			Assert.True(criteriaItem.CriteriaItemType == "compound", $"The CriteriaItemType property of the returned ICriteriaItem was \"{criteriaItem.CriteriaItemType}\" and should have been \"compound\"");
		}
	}
}