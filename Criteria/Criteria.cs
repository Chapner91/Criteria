using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public class Criteria
	{
		public Guid CriteriaID { get; set; }
		public CriteriaGroupOperator CriteriaGroupOperator { get; set; }
		public List<CriteriaGroup> CriteriaGroups { get; set; }
	}
}
