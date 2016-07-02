using System;
using System.Collections.Generic;

namespace CMA
{
	public class SecurityShareDetail
	{
		public string CompanyKey { get; set; }
		public int NoOfUnit { get; set; }
		public double CurrentValue { get; set; }
		public string TableName { get; set; }
	}

	public class SecurityShareDetailRequestModel
	{
		public string UserLoginID { get; set; }
		public string SecurityEntityID { get; set; }
	}

	public class SecurityShareDetailResponseModel
	{
		public List<SecurityShareDetail> SecurityShareDetail { get; set; }
		public List<StatusTable> StatusTable { get; set; }
	}

}

