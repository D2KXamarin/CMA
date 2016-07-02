using System;
using System.Collections.Generic;

namespace CMA
{
	public class PropertyDetailsModel
	{
		public string InsuranceCompany { get; set; }
		public string InsuranceExpiryDt { get; set; }
		public string Add1 { get; set; }
		public string Add2 { get; set; }
		public string Add3 { get; set; }
		public string Pincode { get; set; }
		public string Landmark { get; set; }
		public string TableName { get; set; }
	}

	public class PropertyDetailsResponseModel
	{
		public List<PropertyDetailsModel> SecurityPropertyDetail { get; set; }
		public List<StatusTable> StatusTable { get; set; }
	}

	public class PropertyDetailsRequestModel
	{
		public string UserLoginID { get; set; }
		public string SecurityEntityID { get; set; }
	}
}

