using Criteria.CriteriaItems.CriteriaFunctions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.JsonConverters
{
	public class IArgumentConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IArgument);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new InvalidOperationException("ICriteriaFunctionSchemeConverter cannot be used to write JSON, use default serialization");
		}


		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jsonObject = JObject.Load(reader);
			//var criteriaItemType = jsonObject["CriteriaItemType"].Value<string>();
			var argument = new Argument();
			serializer.Populate(jsonObject.CreateReader(), argument);
			return argument;
		}
	}
}
