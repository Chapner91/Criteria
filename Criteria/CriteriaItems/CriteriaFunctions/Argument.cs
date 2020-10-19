using Criteria.Enums;
using Criteria.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	[JsonConverter(typeof(IArgumentConverter))]
	public class Argument : IArgument
	{
		[JsonProperty(PropertyName = "Name")]
		public string Name { get; private set; }
		[JsonProperty(PropertyName = "DataType")]
		public DataType DataType { get; private set; }
		[JsonProperty(PropertyName = "RequiresSingleValue")]
		public bool RequiresSingleValue { get; private set; }
		[JsonProperty(PropertyName = "ArgumentID")]
		public Guid ArgumentID { get; private set; }

		public Argument() { }

		public Argument(Guid argumentID, string name, DataType dataType, bool requiresSingleValue) : this(name, dataType, requiresSingleValue)
		{
			this.ArgumentID = argumentID;
		}

		public Argument(string name, DataType dataType, bool requiresSingleValue)
		{
			this.ArgumentID = Guid.NewGuid();
			Name = name;
			DataType = dataType;
			RequiresSingleValue = requiresSingleValue;
		}

		public override bool Equals(object obj)
		{
			var that = obj as Argument;
			bool result = false;
			if (that == null)
			{
				return false;
			}
			else if(
				this.ArgumentID == that.ArgumentID &&
				this.Name == that.Name && 
				this.RequiresSingleValue == that.RequiresSingleValue &&
				this.DataType == that.DataType
				)
			{
				result = true;
			}
			return result;
		}

		public override int GetHashCode()
		{
			var hashCode = 1138087743;
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
			hashCode = hashCode * -1521134295 + DataType.GetHashCode();
			hashCode = hashCode * 31 + (RequiresSingleValue.GetHashCode());
			return hashCode;
		}
	}

}
