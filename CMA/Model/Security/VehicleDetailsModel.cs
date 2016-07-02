using System;
using System.Collections.Generic;

namespace CMA
{
	public class SecurityVehicleDetailRequestModel
	{
		public object UserLoginID { get; set; }
		public object SecurityEntityID { get; set; }
	}

	public class SecurityVehicleDetailModel
	{
		public int VehicleEntityID { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		public string Type { get; set; }
		public string VehicleNo { get; set; }
		public string InsuranceCompany { get; set; }
		public string TableName { get; set; }
	}



	public class SecurityVehicleDetailResponceModel
	{
		public List<SecurityVehicleDetailModel> SecurityVehicleDetail { get; set; }
		public List<StatusTable> StatusTable { get; set; }
	}
}

