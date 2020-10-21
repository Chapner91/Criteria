using Criteria.CriteriaUnits.CriteriaFunctions;
using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaExceptions
{
	public class CriteriaUnitException : Exception
	{
		public CriteriaUnitException() : base() { }
		public CriteriaUnitException(string message) : base(message) { }
	}


	public class CriteriaUnitTypeMismatchException : CriteriaUnitException
	{
		public DataType DataType { get; }
		public string Value { get; }
		public ICriteriaUnit ExceptionCriteriaUnit { get; }

		public CriteriaUnitTypeMismatchException() : base() { }
		public CriteriaUnitTypeMismatchException(string message) : base(message) { }
		public CriteriaUnitTypeMismatchException(DataType dataType, string value) : base($"The value \"{value}\" does not match the DataType {dataType}")
		{
			DataType = dataType;
			Value = value;
		}

		public CriteriaUnitTypeMismatchException(DataType dataType, ICriteriaUnit exceptionCriteriaUnit) : base($"The DataType of the child ICriteriaUnit does not match the DataType of the parent ICriteriaUnit")
		{
			DataType = dataType;
			ExceptionCriteriaUnit = exceptionCriteriaUnit;
		}
	}

	public class ArgumentException : Exception
	{
		public ArgumentException() : base() {}
		public ArgumentException(string message) : base(message) { }
		public ArgumentException(Argument argument, string message) : base(message) { }
	}

	public class ArgumentTypeException : ArgumentException
	{
		public ArgumentTypeException() : base() { }
		public ArgumentTypeException(string message) : base(message) { }
		public ArgumentTypeException(Argument argument, string message) : base(argument, message) { }
	}

	public class CriteriaFunctionSchemeException : Exception
	{
		public CriteriaFunctionSchemeException() : base() { }
		public CriteriaFunctionSchemeException(string message) : base(message) { }
	}

	public class CriteriaFunctionSchemeUnmappedArgumentException : CriteriaFunctionSchemeException
	{
		public string TranslatorString { get; }
		public List<Argument> MissingArguments { get; }

		public CriteriaFunctionSchemeUnmappedArgumentException() : base() { }
		public CriteriaFunctionSchemeUnmappedArgumentException(string message) : base(message) { }
		public CriteriaFunctionSchemeUnmappedArgumentException(string translatorString, List<Argument> missingArguments, string message) : base(message)
		{
			TranslatorString = translatorString;
			MissingArguments = missingArguments;
		}
	}
}
