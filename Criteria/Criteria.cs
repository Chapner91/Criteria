using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public class Criteria
	{

		[JsonProperty(PropertyName = "CriteriaID")]
		public Guid CriteriaID { get; set; }

		[JsonProperty(PropertyName = "CriteriaGroupOperator")]
		public CriteriaGroupOperator CriteriaGroupOperator { get; set; }

		[JsonProperty(PropertyName = "CriteriaGroups")]
		public List<CriteriaGroup> CriteriaGroups { get; set; }

		//---------------------------------------------------------
		// Constructors
		//---------------------------------------------------------

		public Criteria()
		{

		}

		public Criteria(string criteriaJson)
		{
			var serializer = new JsonSerializer();

			using (var reader = new StringReader(criteriaJson))
			using (var jsonReader = new JsonTextReader(reader))
			{
				Criteria criteriaFromJson = serializer.Deserialize<Criteria>(jsonReader);

				this.CriteriaID = criteriaFromJson.CriteriaID;
				this.CriteriaGroupOperator = criteriaFromJson.CriteriaGroupOperator;
				this.CriteriaGroups = criteriaFromJson.CriteriaGroups;
			}
		}
	}
}
