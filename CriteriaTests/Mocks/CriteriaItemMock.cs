using Criteria;
using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriteriaTests.Mocks
{
	public class CriteriaUnitSimpleStringMock : ICriteriaUnit
	{
		public string CriteriaUnitType => "simple";
		public Guid CriteriaUnitID => Guid.Parse("8f84f401-2de2-42b7-b9e1-939385c0eace");
		public DataType ReturnDataType => DataType.String;
		public bool ReturnsSingleValue => true;
		public string Value => "Test";
		public string SQLValue => Value;
		public string EnglishValue => Value;
		public string Serialize()
		{
			throw new NotImplementedException();
		}
		public ICriteriaUnit Copy() { throw new NotImplementedException(); }

	}

	public class CriteriaUnitCompoundStringMock : ICriteriaUnit
	{
		public string CriteriaUnitType => "compound";
		public Guid CriteriaUnitID => Guid.Parse("08360bb0-6c78-4d8e-b49b-857b385ab190");
		public DataType ReturnDataType => DataType.String;
		public bool ReturnsSingleValue => false;
		public string Value => "Test";
		public string SQLValue => Value;
		public string EnglishValue => Value;
		public string Serialize()
		{
			throw new NotImplementedException();
		}
		public ICriteriaUnit Copy() { throw new NotImplementedException(); }
	}
}
