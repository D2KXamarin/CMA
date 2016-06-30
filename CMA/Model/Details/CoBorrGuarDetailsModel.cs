using System;
using System.Collections.Generic;

namespace CMA
{
	public class CoBorrGuarDetailsRequestModel
	{
		public string CustomerEntityID { get; set;}
		public string AccountEntityID  { get; set;}
	}
	public class CoBorrGuarDetailsModel
	{
		public string Acid { get; set; }
		public string Name { get; set; }
		public string ADD { get; set; }
		public int PinCode { get; set; }
		public string LandMark { get; set; }
		public double MobileNo { get; set; }
		public string EmailID { get; set; }
		public string TableName { get; set; }
	}

	public class CoBorrGuarDetailsResponseModel
	{
		public List<CoBorrGuarDetailsModel> CoBorrGuar  { get; set; }
		public List<StatusResultModel> StatusTable { get; set; }
	}
}

