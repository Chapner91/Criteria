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
	public class Argument : IArgument, IEquatable<Argument>
	{
		[JsonProperty(PropertyName = "Name")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "DataType")]
		public DataType DataType { get; set; }
		[JsonProperty(PropertyName = "RequiresSingleValue")]
		public bool RequiresSingleValue { get; set; }
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

		public IArgument Copy()
		{
			return new Argument(Name, DataType, RequiresSingleValue);
		}

		public bool Equals(Argument that)
		{
			return that != null &&
				this.Name == that.Name &&
				this.RequiresSingleValue == that.RequiresSingleValue &&
				this.DataType == that.DataType;
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
