using System;
using Criteria.Enums;

namespace Criteria.CriteriaItems.CriteriaFunctions
{
	public interface IArgument
	{
		Guid ArgumentID { get; }
		DataType DataType { get; }
		string Name { get; }
		bool RequiresSingleValue { get; }
	}
}