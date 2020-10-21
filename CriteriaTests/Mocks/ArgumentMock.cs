using Criteria.CriteriaUnits.CriteriaFunctions;
using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriteriaTests.Mocks
{
	public class ArgumentSingleStringMock : IArgument
	{
		public Guid ArgumentID => Guid.Parse("ed52dd9d-1f0c-482a-ada9-77faef448dab");
		public DataType DataType => DataType.String;
		public string Name => "testexpression";
		public bool RequiresSingleValue => true;
		public IArgument Copy() { throw new NotImplementedException(); }
	}

	public class ArgumentMultipleStringMock : IArgument
	{
		public Guid ArgumentID => Guid.Parse("94c2daeb-bff3-4139-89b4-509f3115a847");
		public DataType DataType => DataType.String;
		public string Name => "testexpression";
		public bool RequiresSingleValue => false;
		public IArgument Copy() { throw new NotImplementedException(); }
	}

	public class ArgumentMultipleNumericMock : IArgument
	{
		public Guid ArgumentID => Guid.Parse("ea3f8f81-f531-4acd-9b80-5c0f5b6a60f2");
		public DataType DataType => DataType.Numeric;
		public string Name => "testexpression";
		public bool RequiresSingleValue => false;
		public IArgument Copy() { throw new NotImplementedException(); }
	}
}
