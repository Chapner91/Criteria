using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriteriaHelper
{
	public class CriteriaItem
	{
		public string LeftSide { get; set; }
		public CriteriaItemOperator CriteriaItemOperator { get; set; }
		public string RightSide { get; set; }


	}

	public enum CriteriaItemOperator
	{
		equal,
		lessThan,
		lessThanOrEqual,
		greaterThan,
		greaterThanOrEqual,
		notEqual		
	}
}
