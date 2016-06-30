using System;
using System.Collections.Generic;

namespace CMA
{
	public class RemarkInsertUpdateRequestModel
	{
		public string CustomerEntityID { get; set; }
		public string AccountEntityID { get; set; }
		public string Remark { get; set;}
		public string UserLoginID { get; set;}
		//public List<AssignActionModel> AssignActionData { get; set;}
	}

//	public class AssignActionModel
//	{
//		public string RemarkId { get; set;}
//		public string WhatIsToBeDone { get; set;}
//		public string ByWhen { get; set;}
//		public string ByWhome { get; set;}
//	}

}

