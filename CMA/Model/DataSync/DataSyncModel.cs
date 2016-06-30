using System;
using System.Collections.Generic;

namespace CMA
{
	public class DataSyncModel
	{
	}

	public class DataSyncRequestModel
	{
		public string CustomerEntityID { get; set;}
	}

	public class DataSyncStatusModel : DataSyncRequestModel
	{
		public string BranchCode { get; set;}
		public string BranchName { get; set;}
		public string CustomerID { get; set;}
		public string CustomerName { get; set;}
		public bool ServerToLocalStatus { get; set;}
		public bool LocalToServerStatus { get; set;}
		public bool LocalUpdateStatus { get; set;}
	}


	public class CustomerDetailsSyncRequestModel : DataSyncRequestModel
	{
		
	}

	public class DetailsSyncRequestModel : DataSync
	{
		
	}

	public class CustomerListDataSyncModel
	{
		public string CustomerID { get; set;}
		public string CustomerName { get; set;}
		public string CustomerEntityID { get; set; }
		public bool IsCheched { get; set;}
	}

	public class ServerToLocalModel
	{
		public string TableName { get; set;}
		public string Data { get; set;}
		public bool Status { get; set;}
	}

	public class DataSyncBranchModel
	{
		public string BranchCode { get; set;}
		public string BranchName { get; set;}
	}

	public class LocalDataModel
	{
		public string Data { get; set;}
	}

	public class DataSyncDetailsModel
	{
		public List<LoanDetailsModel> LoanDetails { get; set; }
		public List<SecurityDetailsModel> SecurityDetails { get; set; }
	}

	public class DataSyncActionDetailsModel
	{
		public List<ActionEventDiaryListModel> GetActionEventDiaryList { get; set; }
		public List<ActionEventModel> ActionEventDiaryDetails { get; set; }
	}
}

