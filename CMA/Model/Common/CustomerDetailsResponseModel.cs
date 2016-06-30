using System;
using System.Collections.Generic;

namespace CMA
{
	public class CustomerDetailsResponseModel
	{
		public List<CustomerDetailsModel> CustomerDetails { get; set; }
		public List<AccountDetailModel> AccountDetails { get; set; }
		public List<StatusResultModel> StatusTable { get; set; }
	}
}

