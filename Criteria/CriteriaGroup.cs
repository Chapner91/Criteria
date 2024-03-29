﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria
{
	public class CriteriaGroup
	{
		public Guid CriteriaGroupID { get; set; }
		public CriteriaGroupOperator CriteriaGroupOperator { get; set; }
		public List<CriteriaGroup> CriteriaGroups { get; set; }
		public List<CriteriaPredicate> CriteriaUnits { get; set; }
	}
	
	public enum CriteriaGroupOperator
	{
		AND,
		OR
	}
}
