using Criteria.CriteriaUnits;
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
	public class ICriteriaUnitListConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(List<ICriteriaUnit>);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new InvalidOperationException("ICriteriaUnitListConverter cannot be used to write JSON, use default serialization");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var criteriaUnits = new List<ICriteriaUnit>();
			var jsonArray = JArray.Load(reader);

			foreach (var Unit in jsonArray)
			{
				var jsonObject = Unit as JObject;
				var criteriaUnitType = jsonObject["CriteriaUnitType"].Value<string>();
				var criteriaUnit = ICriteriaUnitHelper.InstantiateCriteriaUnitByType(criteriaUnitType);
				serializer.Populate(jsonObject.CreateReader(), criteriaUnit);
				criteriaUnits.Add(criteriaUnit);
			}

			return criteriaUnits;
		}
	}
}
