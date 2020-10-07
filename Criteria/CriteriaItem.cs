using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public class CriteriaItem
	{
		public DataType DataType { get; set; }
		public string Value { get; private set; }

		public string Function;
		public List<CriteriaItem> Arguments;

		public CriteriaItem()
		{

		}
	}
}
