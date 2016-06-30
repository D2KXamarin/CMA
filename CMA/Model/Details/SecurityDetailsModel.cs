using System;
using System.Collections.Generic;

namespace CMA
{
	public class SecurityDetailsModel
	{
		public string CustomerEntityID { get; set; }
		public string AccountEntityID { get; set; }
		public string AcId { get; set; }
		public string SecurityCategory { get; set; }
		public string SecurityType { get; set; }
		public string SecurityValue { get; set; }
		public string ValuationDt { get; set; }
		public int SecurityEntityId { get; set; }
		public string TableName { get; set; }
	}

	public class SecurityDetailsRequestModel
	{
		public string CustomerEntityID { get; set; }
		public string AccountEntityID { get; set; }
	}

	public class SecurityDetailsResponseModel
	{
		public List<SecurityDetailsModel> SecurityDetails {get; set;}
		public List<StatusResultModel> StatusTable  { get; set; }

	}
}

