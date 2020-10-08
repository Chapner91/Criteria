using Criteria.CriteriaItems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.JsonConverters
{
	public class ICriteriaItemConverter : JsonConverter
	{
		public override bool CanWrite => false;
		public override bool CanRead => true;
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ICriteriaItem);
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new InvalidOperationException("ICriteriaItemConverter cannot be used to write JSON, use default serialization");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jsonObject = JObject.Load(reader);
			var criteriaItemType = jsonObject["CriteriaItemType"].Value<string>();
			var criteriaItem = ICriteriaItemHelper.InstantiateCriteriaItemByType(criteriaItemType);
			serializer.Populate(jsonObject.CreateReader(), criteriaItem);
			return criteriaItem;
		}
	}
}
