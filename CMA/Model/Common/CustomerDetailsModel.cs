using System;

namespace CMA
{
	public class CustomerDetailsModel
	{
		public string CustomerId { get; set; }
		public string CustomerName { get; set; }
		public string CustomerEntityId { get; set;}
		public string Add { get; set; }
		public string Pincode { get; set; }
		public string Landmark { get; set; }
		public string Mobile { get; set; }
		public string EmailId { get; set; }
		public double Lat { get; set; }
		public double Long { get; set; }
		public string TableName { get; set; }
	}
	public class AccountDetailModel
	{
		public string CustomerEntityId { get; set;}
		public string AccountID { get; set; }
		public string AccountEntityID { get; set; }
		public string TableName { get; set; }
	}
}

