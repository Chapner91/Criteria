﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Criteria.Enums;
using Criteria.JsonConverters;
using Newtonsoft.Json;

namespace Criteria.CriteriaItems
{
	[JsonConverter(typeof(ICriteriaItemConverter))]
	class CriteriaItemFunction : ICriteriaItem
	{
		public string CriteriaItemType => "function";
		public DataType DataType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string Value => throw new NotImplementedException();

		public string Serialize()
		{
			throw new NotImplementedException();
		}
	}
}
