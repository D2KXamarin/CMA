using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CMA
{
	public class VMPropertyDetails:INotifyPropertyChanged
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

		public VMPropertyDetails ()
		{
//			LoadSecurityPropertyDetail ();
		}

		public int SecurityEntityId ;


		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private string _PInsuranceCo = string.Empty;

		public string PInsuranceCo {
			get { return _PInsuranceCo; }
			set {
				_PInsuranceCo = value;
				OnPropertyChanged ("PInsuranceCo");
			}
		}

		private string _PdtpValidUptoDt = string.Empty;

		public string PdtpValidUptoDt {
			get { return _PdtpValidUptoDt; }
			set {
				_PdtpValidUptoDt = value;
				OnPropertyChanged ("PdtpValidUptoDt");
			}
		}

		private string _PropertyEntityID = string.Empty;

		public string PropertyEntityID {
			get { return _PropertyEntityID; }
			set {
				_PropertyEntityID = value;
				OnPropertyChanged ("PropertyEntityID");
			}
		}

		private string _PAddress1 = string.Empty;

		public string PAddress1 {
			get { return _PAddress1; }
			set {
				_PAddress1 = value;
				OnPropertyChanged ("PAddress1");
			}
		}

		private string _PAddress2 = string.Empty;

		public string PAddress2 {
			get { return _PAddress2; }
			set {
				_PAddress2 = value;
				OnPropertyChanged ("PAddress2");
			}
		}

		private string _PAddress3 = string.Empty;

		public string PAddress3 {
			get { return _PAddress3; }
			set {
				_PAddress3 = value;
				OnPropertyChanged ("PAddress3");
			}
		}

		private string _PPincode = string.Empty;

		public string PPincode {
			get { return _PPincode; }
			set {
				_PPincode = value;
				OnPropertyChanged ("PPincode");
			}
		}

		private string _PLandmark = string.Empty;

		public string PLandmark {
			get { return _PLandmark; }
			set {
				_PLandmark = value;
				OnPropertyChanged ("PLandmark");
			}
		}

		private bool _IsEnableInsuranceCo = false;

		public bool IsEnableInsuranceCo {
			get { return _IsEnableInsuranceCo; }
			set {
				_IsEnableInsuranceCo = value;
				OnPropertyChanged ("IsEnableInsuranceCo");
			}
		}

		private bool _IsEnabledtpValidUptoDt = false;

		public bool IsEnabledtpValidUptoDt {
			get { return _IsEnabledtpValidUptoDt; }
			set {
				_IsEnabledtpValidUptoDt = value;
				OnPropertyChanged ("IsEnabledtpValidUptoDt");
			}
		}

		private bool _IsEnableAddress1 = false;

		public bool IsEnableAddress1 {
			get { return _IsEnableAddress1; }
			set {
				_IsEnableAddress1 = value;
				OnPropertyChanged ("IsEnableAddress1");
			}
		}

		private bool _IsEnableAddress2 = false;

		public bool IsEnableAddress2 {
			get { return _IsEnableAddress1; }
			set {
				_IsEnableAddress2 = value;
				OnPropertyChanged ("IsEnableAddress2");
			}
		}

		private bool _IsEnableAddress3 = false;

		public bool IsEnableAddress3 {
			get { return _IsEnableAddress3; }
			set {
				_IsEnableAddress3 = value;
				OnPropertyChanged ("IsEnableAddress3");
			}
		}

		private bool _IsEnablePincode = false;

		public bool IsEnablePincode {
			get { return _IsEnablePincode; }
			set {
				_IsEnablePincode = value;
				OnPropertyChanged ("IsEnablePincode");
			}
		}

		private bool _IsEnableLandmark = false;

		public bool IsEnableLandmark {
			get { return _IsEnableLandmark; }
			set {
				_IsEnableLandmark = value;
				OnPropertyChanged ("IsEnableLandmark");
			}
		}

		private bool _IsEnableEdit = false;

		public bool IsEnableEdit {
			get { return _IsEnableEdit; }
			set {
				_IsEnableEdit = value;
				OnPropertyChanged ("IsEnableEdit");
			}
		}

		private bool _IsEnableSave = false;

		public bool IsEnableSave {
			get{ return _IsEnableSave; }
			set {
				_IsEnableSave = value;
				OnPropertyChanged ("IsEnableSave");
			}
		}

		private bool _IsVisibleCancle = false;

		public bool IsVisibleCancle {
			get{ return _IsVisibleCancle; }
			set {
				_IsVisibleCancle = value;
				OnPropertyChanged ("IsVisibleCancle");
			}
		}

		PropertyDetailsModel securityPropertyDetail =null;
		string Operationflag;

		public void LoadData(){
			PInsuranceCo = securityPropertyDetail.InsuranceCompany;
//			PdtpValidUptoDt = securityPropertyDetail.InsuranceExpiryDt;
			PAddress1 = securityPropertyDetail.Add1;
			PAddress2 = securityPropertyDetail.Add2;
			PAddress3 = securityPropertyDetail.Add3;
			PPincode = securityPropertyDetail.Pincode;
			PLandmark = securityPropertyDetail.Landmark;
		}

		public async Task LoadSecurityPropertyDetail ()
		{
			ActivityIndicator = true;
			try {

				var result = await APIRequest.Instance.GetSecurityPropertyDetailsAPI (new PropertyDetailsRequestModel {
					UserLoginID = GlobalVariables.UserLoginID,
					SecurityEntityID = GlobalVariables.SecurityEntityID
				});

				if (result != null) {
					PropertyDetailsResponseModel responseModelList = JsonConvert.DeserializeObject<PropertyDetailsResponseModel> (result);

					if (responseModelList.SecurityPropertyDetail != null) {
						Operationflag ="2";
						securityPropertyDetail = responseModelList.SecurityPropertyDetail [0];
						if (securityPropertyDetail != null) {
							LoadData ();

						}
					} else {
						Operationflag ="1";
					}
				}
				ChangeState(false);
			} catch {
				GlobalVariables.DisplayMessage = "Error";
				MessagingCenter.Send<VMPropertyDetails> (this, Strings.Display_Message);
			}
			ActivityIndicator = false;
		}

		void ChangeState(bool flag)
		{
			IsEnableInsuranceCo = flag;
			IsEnabledtpValidUptoDt = flag;
			IsEnableAddress1 = flag;
			IsEnableAddress2 = flag;
			IsEnableAddress3 = flag;
			IsEnablePincode = flag;
			IsEnableLandmark = flag;
			IsEnableSave = flag;
			IsVisibleCancle = flag;
			IsEnableEdit = !flag;
		}

		private Command _Save = null;

		public Command Save {
			get {
				return _Save ?? new Command (async delegate(object o) {

					try {

						SecurityPropertyDetailInsertUpdateModel securityPropertyDetailInsertUpdateModel = new SecurityPropertyDetailInsertUpdateModel ();
						if (Operationflag == "2") {
							securityPropertyDetailInsertUpdateModel.CustomerEntityID = securityPropertyDetail.CustomerEntityID;
							securityPropertyDetailInsertUpdateModel.AccountEntityID = securityPropertyDetail.AccountEntityId;

						} else if (Operationflag == "1") {
							securityPropertyDetailInsertUpdateModel.CustomerEntityID = GlobalVariables.CustomerEntityID;
							securityPropertyDetailInsertUpdateModel.AccountEntityID = GlobalVariables.AccountEntityID;
						}

						securityPropertyDetailInsertUpdateModel.UserLoginID = GlobalVariables.UserLoginID;
						securityPropertyDetailInsertUpdateModel.SecurityEntityID = SecurityEntityId;
						securityPropertyDetailInsertUpdateModel.PropertyEntityID=PropertyEntityID;
						securityPropertyDetailInsertUpdateModel.InsuranceCompany=PInsuranceCo;
						securityPropertyDetailInsertUpdateModel.ValidUpTo=PdtpValidUptoDt;
						securityPropertyDetailInsertUpdateModel.Add1=PAddress1;
						securityPropertyDetailInsertUpdateModel.Add2=PAddress2;
						securityPropertyDetailInsertUpdateModel.Add2=PAddress3;
						securityPropertyDetailInsertUpdateModel.Pincode=PPincode;
						securityPropertyDetailInsertUpdateModel.Landmark=PLandmark;
						securityPropertyDetailInsertUpdateModel.OperationFlag = Operationflag;

						var result = await APIRequest.Instance.SecurityPropertyDetailInsertUpdate (securityPropertyDetailInsertUpdateModel);

						if (result != null) {
							GlobalVariables.DisplayMessage = "Property Details Saved Successfully";
							MessagingCenter.Send<VMPropertyDetails> (this, Strings.Display_Message);
						} else {
							GlobalVariables.DisplayMessage = "Error... Please try again";
							MessagingCenter.Send<VMPropertyDetails> (this, Strings.Display_Message);
						}
						//update model with insert update model
						securityPropertyDetail.InsuranceCompany=securityPropertyDetailInsertUpdateModel.InsuranceCompany;
						securityPropertyDetail.ValidUpTo=securityPropertyDetailInsertUpdateModel.ValidUpTo;
						securityPropertyDetail.Add1=securityPropertyDetailInsertUpdateModel.Add1;
						securityPropertyDetail.Add2=securityPropertyDetailInsertUpdateModel.Add2;
						securityPropertyDetail.Add3=securityPropertyDetailInsertUpdateModel.Add3;
						securityPropertyDetail.Pincode=securityPropertyDetailInsertUpdateModel.Pincode;
						securityPropertyDetail.Landmark=securityPropertyDetailInsertUpdateModel.Landmark;
						ChangeState(false);

					} catch {
						GlobalVariables.DisplayMessage = "Error... Please try again";
						MessagingCenter.Send (this, Strings.Display_Message);
					}

				});
			}
		}


		private Command _Edit = null;
		public Command Edit {
			get {
				return _Edit ?? new Command (async delegate(object o) {
					ChangeState(true);				
				});
			}
		}

		private Command _Cancel = null;
		public Command Cancel {
			get {
				return _Cancel ?? new Command (async delegate(object o) {
					LoadData();
					ChangeState(false);
				});
			}
		}

	}
}

