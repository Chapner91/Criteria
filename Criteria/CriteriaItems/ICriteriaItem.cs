using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public interface ICriteriaItem
	{
		DataType DataType { get; set; }
		string Value { get; }

		string Serialize();
		//string Deserialize();
	}
}
