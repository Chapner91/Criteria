using Criteria.CriteriaItems.CriteriaFunctions;
using Criteria.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaExceptions
{
	public class CriteriaItemException : Exception
	{
		public CriteriaItemException() : base() { }
		public CriteriaItemException(string message) : base(message) { }
	}


	public class CriteriaItemTypeMismatchException : CriteriaItemException
	{
		public DataType DataType { get; }
		public string Value { get; }
		public ICriteriaItem ExceptionCriteriaItem { get; }

		public CriteriaItemTypeMismatchException() : base() { }
		public CriteriaItemTypeMismatchException(string message) : base(message) { }
		public CriteriaItemTypeMismatchException(DataType dataType, string value) : base($"The value \"{value}\" does not match the DataType {dataType}")
		{
			DataType = dataType;
			Value = value;
		}

		public CriteriaItemTypeMismatchException(DataType dataType, ICriteriaItem exceptionCriteriaItem) : base($"The DataType of the child ICriteriaItem does not match the DataType of the parent ICriteriaItem")
		{
			DataType = dataType;
			ExceptionCriteriaItem = exceptionCriteriaItem;
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
