using System;
using System.Collections.Generic;

namespace CMA
{
	public class SecurityShareDetail
	{
		public string CustomerEntityID { get; set; }
		public string AccountEntityId { get; set; }
		public string CRMEntityID { get; set; }
		public string CompanyKey { get; set; }
		public string NoOfUnit { get; set; }
		public string CurrentValue { get; set; }
		public string HOSecurityAlt_Key { get; set; }
		public string TableName { get; set; }

	}

	public class SecurityShareDetailRequestModel
	{
		public object UserLoginID { get; set; }
		public object SecurityEntityID { get; set; }
	}

	public class SecurityShareDetailResponseModel
	{
		public List<SecurityShareDetail> SecurityShareDetail { get; set; }
		public List<StatusTable> StatusTable { get; set; }
	}

	public class SecurityShareDetailInsertUpdateModel
	{
		public string CustomerEntityID { get; set; }
		public string AccountEntityID { get; set; }
		public string CRMEntityID { get; set; }
		public string UserLoginID { get; set; }
		public int SecurityEntityID { get; set; }
		public string CompanyKey { get; set; }
		public string NoOfUnit { get; set; }
		public string CurrentValue { get; set; }
		public string HOSecurityAlt_Key { get; set; }
		public string OperationFlag  { get; set; }
	}


}

