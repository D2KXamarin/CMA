using System;
using System.Collections.Generic;
using SQLite;

namespace CMA
{
	public class AssignActionModel
	{

		public int AssignActionID { get; set; }

		public int RemarkStatusKey { get; set; }

		public string WhatisToBeDone { get; set; }

		public string ByWhen { get; set; }

		public string InfoStakeHolder { get; set; }

		public string PrimaryActionStakeHolder { get; set; }

		public string SecondaryActionStakeHolder { get; set; }

	}

	public class AllocatedStakeholder
	{
		public string UserLoginID { get; set; }
		public string UserName { get; set; }
		public string StakeholderType { get; set; }
		public bool IsAssigned { get; set; }
		public string TableName { get; set; }
	}

	public class AssignedStakeholder
	{
		public int EventID { get; set; }

		public string UserLoginID { get; set; }

		public string UserName { get; set; }

		public string StakeholderType { get; set; }

		public string TableName { get; set; }
	}

	public class AssignActionResponseModel
	{
		public List<AssignActionModel> AssignAction { get; set; }

		public List<AllocatedStakeholder> AllocatedStakeholder { get; set; }

		public List<AssignedStakeholder> AssignedStakeholder { get; set; }

		public List<StatusTable> StatusTable { get; set; }
	}

	public class AssignActionRequestModel
	{
		public string CustomerEntityID { get; set; }

		public string RemarkID { get; set; }
	}

	public class AssignActionInsertUpdateRequestModel
	{
		public string RemarkID { get; set; }

		public int AssignActionID { get; set; }

		public string WhatIsToBeDone { get; set; }

		public string ByWhen { get; set; }

		public string InfoStakeHolder { get; set; }

		public string PrimaryActionStakeHolder { get; set; }

		public string SecondaryActionStakeHolder { get; set; }

	}

}


