using System;
using System.Collections.Generic;
using SQLite;

namespace CMA
{

	public class ActionEventDiaryRequestModel 
	{
		public string RemarkID { get; set; }

	}
	public class ActionEventModel
	{
		public string CustomerEntityID { get; set; }
		public string AccountEntityID { get; set; }
		public int EventID{ get; set; }
		public string WhatIsToBeDone { get; set; }
		public String Status { get; set; }
		public string CommencementDate { get; set; }
		public string CommenceComment { get; set; }
		public string ClosureDate { get; set; }
		public string ClosureComment { get; set; }
		public string TableName { get; set; }
	}

	public class ActionEventDiaryResponseModel 
	{
		public List<ActionEventModel> ActionEventDiaryDetails { get; set; }
		public List<StatusTable> StatusTable { get; set; }
	}

	public class EventStatusModel
	{
		public int StatusKey { get; set;}
		public string StatusValue { get; set;}
	}

	public class ActionEventUpdate
	{
		[PrimaryKey]
		public int EventID { get; set; }
		public string RemarkID { get; set; }
		public string UserLoginID { get; set; }

		public int Status  { get; set; }
		public string CommencementDt { get; set; }
		public string CommencementRemark { get; set; }
		public string ClosureDt { get; set; }
		public string ClosureRemark { get; set; }
	}
}

