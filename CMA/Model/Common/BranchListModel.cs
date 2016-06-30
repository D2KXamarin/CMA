using System;
using System.Collections.Generic;

namespace CMA
{
	public class BranchListModel
	{
		public string BranchCode {get; set;}
		public string BranchName {get; set;}

	}
	public class BranchListRequestModel
	{
		public string UserLoginId { get; set; }
		public string SearchString { get; set; }
	}
	public class BranchListResponseModel
	{
		public List<StatusResultModel> StatusTable {get; set;}
		public List<BranchListModel> BranchList {get; set;}
	}
}

