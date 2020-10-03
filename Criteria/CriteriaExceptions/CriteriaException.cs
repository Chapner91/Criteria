using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaExceptions
{
	public class CriteriaException : Exception
	{
		public CriteriaException() : base()
		{

		}

		public CriteriaException(string message) : base(message)
		{

		}
	}

	public class CriteriaItemException : CriteriaException
	{
		private CriteriaItem _criteriaItem;

		public CriteriaItemException() : base()
		{

		}

		public CriteriaItemException(string message) : base(message)
		{

		}


		public CriteriaItemException(CriteriaItem criteriaItem) : base()
		{
			_criteriaItem = criteriaItem;
		}
	}

	public class CriteriaItemDataTypeClashException : CriteriaItemException
	{
		public CriteriaItemDataTypeClashException() : base()
		{

		}

		public CriteriaItemDataTypeClashException(DataType dataType, string value) : base($"CriteriaItem has a specified DataType of {dataType} which does not match the value {value}")
		{
			
		}
	}
}
