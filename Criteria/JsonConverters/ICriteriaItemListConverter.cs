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
	public class ICriteriaItemListConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(List<ICriteriaItem>);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new InvalidOperationException("ICriteriaItemListConverter cannot be used to write JSON, use default serialization");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var criteriaItems = new List<ICriteriaItem>();
			var jsonArray = JArray.Load(reader);

			foreach (var item in jsonArray)
			{
				var jsonObject = item as JObject;
				var criteriaItemType = jsonObject["CriteriaItemType"].Value<string>();
				var criteriaItem = ICriteriaItemHelper.InstantiateCriteriaItemByType(criteriaItemType);
				serializer.Populate(jsonObject.CreateReader(), criteriaItem);
				criteriaItems.Add(criteriaItem);
			}

			return criteriaItems;
		}
	}
}
