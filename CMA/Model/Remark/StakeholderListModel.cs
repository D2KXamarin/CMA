using System;
using SQLite;
using System.Collections.Generic;

namespace CMA
{
	public class StakeholderListModel
	{	
		[PrimaryKey]
		public string UserLoginId {get; set;}
		public string UserName {get; set;}
		public bool IsAllocate {get; set;}
		public string StakeholderType {get; set;}
	}


	public class StakeholderListRequestModel
	{
		public string CustomerEntityID  {get; set;}
		public string UserLoginID  {get; set;}
	}

	public class StakeholderListResponseModel
	{
		public List<StakeholderListModel> StakeholderList {get; set;}
		public List<StatusTable> StatusTable {get; set;}
	}
}

