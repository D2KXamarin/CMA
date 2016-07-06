using System;
using System.Collections.Generic;

namespace CMA
{
	public class PasswordValidationDetailsModel
	{
		public string abbrivi { get; set; }
		public string parameterType { get; set; }
		public string parameterValue { get; set; }
		public string minvalue { get; set; }
		public string maxvalue { get; set; }
	}

	public class PasswordValidationModel
	{
		public string error_Code { get; set; }
		public string error_msg { get; set; }
		public List<PasswordValidationDetailsModel> PwdValidation { get; set; }
	}

	public class ChangePasswordUpdateModel
	{
		public string UserLoginID { get; set; }
		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }

	}
}

