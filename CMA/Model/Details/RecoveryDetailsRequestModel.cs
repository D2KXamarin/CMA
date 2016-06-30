using System;
using System.Collections.Generic;

namespace CMA
{
	public class RecoveryDetailsRequestModel
	{
		public string CustomerEntityID  { get; set; }
		public string AccountEntityID  { get; set; }
	}
		

	public class RecoveryDetailsModel
	{
		public string AcId { get; set; }
		public double Limit { get; set; }
		public string IrregularSince { get; set; }
		public int Credits_1 { get; set; }
		public int Credits_2 { get; set; }
		public int Credits_3 { get; set; }
		public string TableName { get; set; }
	}



	public class RecoveryDetailsResponseModel
	{
		public List<RecoveryDetailsModel> RecoveryDetails { get; set; }
		public List<StatusResultModel> StatusTable { get; set; }
	}
}

