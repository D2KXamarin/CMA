﻿using System;
using System.Collections.Generic;

namespace CMA
{
	public class SecurityGoldDetailRequestModel
	{
		public object UserLoginID { get; set; }
		public object SecurityEntityID { get; set; }
	}

	public class SecurityGoldDetailModel
	{
		public string Quantity { get; set; }
		public string Karat { get; set; }
		public string Margin { get; set; }
		public string TableName { get; set; }
	}


	public class SecurityGoldDetailResponceModel
	{
		public List<SecurityGoldDetailModel> SecurityGoldDetail { get; set; }
		public List<StatusTable> StatusTable { get; set; }
	}
}

