using System;
using System.Collections.Generic;

namespace CMA
{
	public class InsuranceCompanyMasterModel
	{
		public int CompanyKey { get; set; }
		public string CompanyName { get; set; }
		public string TableName { get; set; }
	}

	public class InsuranceCompanyListRequestModel
	{
		public string UserLoginId { get; set; }
		public string SearchString { get; set; }
	}

	public class InsuranceCompanyMasterResponceModel
	{
		public List<InsuranceCompanyMasterModel> InsuranceCompanyMaster { get; set; }
		public List<StatusTable> StatusTable { get; set; }
	}
}

