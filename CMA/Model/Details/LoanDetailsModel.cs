using System;
using System.Collections.Generic;

namespace CMA
{
	public class LoanDetailsModel
	{
		public string CustomerEntityID { get; set; }
		public string AcId { get; set; }
		public int AccountEntityID { get; set; }
		public string Fac { get; set; }
		public string Limit { get; set; }
		public string Bal_OS { get; set; }
		public string Irregular_Since { get; set; }
		public string CAD { get; set; }
		public string CADU { get; set; }
		public string OverdueAmt { get; set; }
		public string Reason { get; set; }
		public string TableName { get; set; }

	}

	public class LoanDetailsRequestModel
	{
		public string CustomerEntityID { get; set; }
		public string AccountEntityID { get; set; }
	}

	public class LoanDetailsResponseModel
	{
		public List<LoanDetailsModel> LoanDetails { get; set; }
		public List<StatusResultModel> StatusTable  { get; set; }
	}
}

