using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.CriteriaUnits
{
	public interface ICopyable<T>
	{
		T Copy();
	}
}
