using System;

namespace CMA
{
	public class  LoginRequestModel
	{
		public string UserLoginId { get; set; }
		public string Password { get; set; }
	}
	public class LoginResponseModel
	{
		public int StatusCode { get; set; }
		public string UserLoginID { get; set; }
		public string UserName { get; set; }
		public string UserLocation { get; set; }
		public string UserLocationCode { get; set; }
		public string UserLocationName { get; set;}
		public string LastLogin { get; set; }
		public string TableName { get; set; }

	}
}

