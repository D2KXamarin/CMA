using System;
using System.Collections.Generic;

namespace CMA
{
	public class ActionEventDiaryListRequestModel 
	{
		public string CustomerEntityID { get; set; }
		public string AccountEntityID  { get; set; }
	}
	public class ActionEventDiaryListModel
	{
		public string CustomerEntityID { get; set; }
		public string AccountEntityID { get; set; }
		public string RemarkID { get; set; }
		public string EventID { get; set; }
		public string AcID { get; set; }
		public string RemarkDt { get; set; }
		public string RemarkByWhom { get; set; }
		public string WhatIsToBeDone { get; set; }
		public string DtByWhen { get; set; }
		public string Status { get; set; }
		public string TableName { get; set; }
	}


	public class ActionEventDiaryListResponseModel 
	{
		public List<ActionEventDiaryListModel> GetActionEventDiaryList { get; set; }
		public List<StatusResultModel> StatusTable { get; set; }
	}
}

