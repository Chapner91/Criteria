using Xunit;
using Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criteria.Tests
{
	public class CriteriaTests
	{
		private string _blankCriteriaJson = ""
			+ "{"
			+ "	\"CriteriaID\": \"e4d1ecd2-9cee-4266-b6d2-64b116a43ac8\","
			+ "	\"CriteriaGroupOperator\": \"OR\","
			+ "	\"CriteriaGroups\": [ ]"
			+ "}"
			;

		//private string _basicCriteriaJson = ""
		//	+ "{"
		//	+ "	\"CriteriaID\": \"85395418-7a28-498e-82c9-ae9b05f87bc5\","
		//	+ "	\"CriteriaGroupOperator\": \"OR\","
		//	+ "	\"CriteriaGroupJson\": ["
		//	+ "		{"
		//	+ "			\"CriteriaGroupID\": \"e4d1ecd2-9cee-4266-b6d2-64b116a43ac8\","
		//	+ "			\"CriteriaGroupOperator\": \"AND\","
		//	+ "			\"CriteriaGroupJson\": [],"
		//	+ "			\"CriteriaItems\": ["
		//	+ "				{"
		//	+ "					\"LeftSide\": \"A\","
		//	+ "					\"CriteriaItemOperator\": \"equal\","
		//	+ "					\"RightSide\": \"B\""
		//	+ "				},"
		//	+ "				{"
		//	+ "					\"LeftSide\": \"C\","
		//	+ "					\"CriteriaItemOperator\": \"lessThan\","
		//	+ "					\"RightSide\": \"5000\""
		//	+ "				}"
		//	+ "			]"
		//	+ "		}"
		//	+ "	]"
		//	+ "}"
		//	;

		//private string _basicCriteriaGroupsJson =""
		//	+ "["
		//	+ "		{"
		//	+ "			\"CriteriaGroupID\": \"e4d1ecd2-9cee-4266-b6d2-64b116a43ac8\","
		//	+ "			\"CriteriaGroupOperator\": \"AND\","
		//	+ "			\"CriteriaGroupJson\": [],"
		//	+ "			\"CriteriaItems\": ["
		//	+ "				{"
		//	+ "					\"LeftSide\": \"A\","
		//	+ "					\"CriteriaItemOperator\": \"equal\","
		//	+ "					\"RightSide\": \"B\""
		//	+ "				},"
		//	+ "				{"
		//	+ "					\"LeftSide\": \"C\","
		//	+ "					\"CriteriaItemOperator\": \"lessThan\","
		//	+ "					\"RightSide\": \"5000\""
		//	+ "				}"
		//	+ "			]"
		//	+ "		}"
		//	+ "	]"
		//	;





		[Fact()]
		public void Criteria_InstantiateBlankCriteriaWithJsonString()
		{
			var expected = new Criteria() { CriteriaID = new Guid("e4d1ecd2-9cee-4266-b6d2-64b116a43ac8"), CriteriaGroupOperator = CriteriaGroupOperator.OR, CriteriaGroups = null };
			var actual = new Criteria(_blankCriteriaJson);

			Assert.True
			(
				(expected.CriteriaID == actual.CriteriaID) &&
				(expected.CriteriaGroupOperator == actual.CriteriaGroupOperator)
			);		
		}
	}
}