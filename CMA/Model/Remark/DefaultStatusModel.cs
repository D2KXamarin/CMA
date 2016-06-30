using System;
using System.Collections.Generic;

namespace CMA
{
	
	public class DefaultStatusModel
	{
		public string DefaultStatusName { get; set; }

	}

	public class StatusTable
	{
		public int StatusCode { get; set; }
		public string StatusMsg { get; set; }
	}

	public class DefaultStatusResponseModel
	{
		public List<DefaultStatusModel> DimDefaultStatus { get; set; }
		public List<StatusTable> StatusTable { get; set; }
	}
}

