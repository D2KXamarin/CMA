using System;
using System.Collections.Generic;

namespace CMA
{
	public class PreviousRemarkListModel
	{
		public string CustomerACID { get; set;}
		public string RemarkID { get; set;}
		public string Remark { get; set;}
	}


	public class PreviousRemarkListResponseModel
	{
		public List<PreviousRemarkListModel> GetPreviousRemark { get; set; }
		public List<StatusTable> StatusTable { get; set; }
	}

	public class PreviousRemarkListRequestModel
	{
		public string CustomerEntityID { get; set; }
		public string AccountEntityID { get; set; }
	}
}