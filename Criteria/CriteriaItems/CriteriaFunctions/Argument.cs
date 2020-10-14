using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	public class Argument
	{
		public string Name { get; private set; }
		public DataType DataType { get; private set; }
		public bool RequiresSingleValue { get; private set; }


		public Argument(string name, DataType dataType, bool requiresSingleValue)
		{
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
