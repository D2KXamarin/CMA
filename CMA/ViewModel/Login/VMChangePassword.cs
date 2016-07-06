
using System.ComponentModel;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CMA
{
	public class VMChangePassword : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			if (PropertyChanged != null) {
				PropertyChanged (this,
					new PropertyChangedEventArgs (propertyName));
			}
		}

		#endregion

		public VMChangePassword ()
		{
			ChangeControlState3 ();

			//ChangeControlState ();
		}



		private static string _PCurrentPassword = string.Empty;
		private static string _PNewPassword = string.Empty;
		private static string _ConfirmPassword = string.Empty;

		public string PCurrentPassword {
			get { return _PCurrentPassword; }
			set {
				if ((value != null && value.Length <= 20) || value == null) {
					_PCurrentPassword = value;
				}
				OnPropertyChanged ("PCurrentPassword");
				ChangeControlState ();
				ChangeControlState3 ();

			}
		}

		public string PNewPassword {
			get { return _PNewPassword; }
			set {
				_PNewPassword = value;
				OnPropertyChanged ("PNewPassword");
				ChangeControlState1 ();
			}
		}

		public string ConfirmPassword {
			get { return _ConfirmPassword; }
			set {
				_ConfirmPassword = value;
				OnPropertyChanged ("ConfirmPassword");
				ChangeControlState2 ();
			}
		}

		private bool _IsNewPasswordEnable;

		public bool IsNewPasswordEnable {
			get { return _IsNewPasswordEnable; }
			set {
				_IsNewPasswordEnable = value;
				OnPropertyChanged ("IsNewPasswordEnable");
			}
		}

		private bool _IsConfirmPasswordEnable;

		public bool IsConfirmPasswordEnable {
			get { return _IsConfirmPasswordEnable; }
			set {
				_IsConfirmPasswordEnable = value;
				OnPropertyChanged ("IsConfirmPasswordEnable");

			}
		}

		private bool _PIsPassword1 = true;

		public bool PIsPassword1 {
			get { return _PIsPassword1; }
			set {
				_PIsPassword1 = value;
				OnPropertyChanged ("PIsPassword1");
			}

		}

		private bool _PShowPassword1 = false;

		public bool PShowPassword1 {
			get { return _PShowPassword1; }
			set {
				_PShowPassword1 = value;
				OnPropertyChanged ("PShowPassword1");
				PIsPassword1 = !value;

			}

		}

		private bool _PIsPassword2 = true;

		public bool PIsPassword2 {
			get { return _PIsPassword2; }
			set {
				_PIsPassword2 = value;
				OnPropertyChanged ("PIsPassword2");
			}

		}

		private bool _PShowPassword2 = false;

		public bool PShowPassword2 {
			get { return _PShowPassword2; }
			set {
				_PShowPassword2 = value;
				OnPropertyChanged ("PShowPassword2");
				PIsPassword2 = !value;

			}

		}

		private bool _PIsPassword3 = true;

		public bool PIsPassword3 {
			get { return _PIsPassword3; }
			set {
				_PIsPassword3 = value;
				OnPropertyChanged ("PIsPassword3");
			}

		}

		private bool _PShowPassword3 = false;

		public bool PShowPassword3 {
			get { return _PShowPassword3; }
			set {
				_PShowPassword3 = value;
				OnPropertyChanged ("PShowPassword3");
				PIsPassword3 = !value;

			}

		}


		private bool _IsChangeButtonEnable = false;

		public bool IsChangeButtonEnable {
			get { return _IsChangeButtonEnable; }
			set {
				_IsChangeButtonEnable = value;
				OnPropertyChanged ("IsChangeButtonEnable");
			}
		}

		//		private bool _IsCancelButtonEnable = false;
		//
		//		public bool IsCancelButtonEnable {
		//			get{ return _IsCancelButtonEnable; }
		//			set {
		//				_IsCancelButtonEnable = value;
		//				OnPropertyChanged ("IsCancelButtonEnable");
		//			}
		//		}
		//


		public void ChangeControlState ()
		{
			if (PCurrentPassword == "") {

				IsNewPasswordEnable = false;
				IsConfirmPasswordEnable = false;
				IsChangeButtonEnable = false;

			} else if (PCurrentPassword.Length > 0) {
				IsNewPasswordEnable = true;




			}


		}

		public void ChangeControlState1 ()
		{
			if (PNewPassword == "") {
				IsConfirmPasswordEnable = false;
				IsChangeButtonEnable = false;
			} else if (PNewPassword.Length > 0 ) {
				IsConfirmPasswordEnable = true;
			}
		}

		public void ChangeControlState2 ()
		{
			if (ConfirmPassword == "") {
				IsChangeButtonEnable = false;

			} else if (ConfirmPassword.Length > 0) {
				IsChangeButtonEnable = true;

			}
		}

		public void ChangeControlState3 ()
		{
			if (PCurrentPassword.Length > 0 && PNewPassword.Length > 0) {
				IsConfirmPasswordEnable = true;
			}

			else if (PCurrentPassword.Length > 0 && PNewPassword == "") {
				IsConfirmPasswordEnable = false;
				PShowPassword3 = true;

			}
		}

		private Command _Reset = null;

		public Command Reset {
			get {
				return _Reset ?? new Command (async delegate(object o) {
					ChangePasswordUpdateModel changePasswordUpdateModel = new ChangePasswordUpdateModel ();
					changePasswordUpdateModel.CurrentPassword = PCurrentPassword;
					changePasswordUpdateModel.NewPassword = PNewPassword;
					changePasswordUpdateModel.UserLoginID = GlobalVariables.UserLoginID;
					if(PNewPassword.Equals(ConfirmPassword)){
						var result = await APIRequest.Instance.ChangePasswordUpdate (changePasswordUpdateModel);


						if (result != null) {

							JObject JsonResult = JObject.Parse (result);
							string ResultValue = JsonResult ["Result"].ToString ();
							if (ResultValue == "1") {
								GlobalVariables.DisplayMessage = "Password Update Successfully";
								MessagingCenter.Send<VMChangePassword> (this, Strings.ChangePassword_Success);
							} else {
								MessagingCenter.Send<VMChangePassword> (this, Strings.ChangePassword__FAILURE);

							}
						}
					}else{
						GlobalVariables.DisplayMessage = "New Password & ConfirmPassword Password Should Be Same!";
						MessagingCenter.Send<VMChangePassword> (this, Strings.Display_Message);
					}


				});
			}
		}

	}
}







