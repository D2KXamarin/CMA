using System;
using System.Collections.Generic;
namespace CMA
{

	public class CustomerListModel
	{
		public string CustomerID { get; set; }
		public string CustomerName { get; set; }
		public string CustomerEntityID { get; set; }
	}


	public class Custumercollection
	{
		public List<CustomerListModel> CustomerList { get; set; }
		public List<StatusResultModel> StatusTable { get; set; }
	}
}

