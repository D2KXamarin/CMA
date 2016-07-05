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
		int Operationflag;

		public void LoadData(){
			PInsuranceCo = securityPropertyDetail.InsuranceCompany;
			PdtpValidUptoDt = securityPropertyDetail.InsuranceExpiryDt;
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
						Operationflag = 2;
						securityPropertyDetail = responseModelList.SecurityPropertyDetail [0];
						if (securityPropertyDetail != null) {
							LoadData ();

						}
					} else {
						Operationflag = 1;
					}
				}
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

//					try {
//
//						SecurityShareDetailInsertUpdateModel securityShareDetailInsertUpdateModel = new SecurityShareDetailInsertUpdateModel ();
//						if (Operationflag == 2) {
//							securityShareDetailInsertUpdateModel.CustomerEntityID = securityShareDetail.CustomerEntityID;
//							securityShareDetailInsertUpdateModel.AccountEntityID = securityShareDetail.AccountEntityId;
//
//						} else if (Operationflag == 1) {
//							securityShareDetailInsertUpdateModel.CustomerEntityID = GlobalVariables.CustomerEntityID;
//							securityShareDetailInsertUpdateModel.AccountEntityID = GlobalVariables.AccountEntityID;
//						}
//
//						securityShareDetailInsertUpdateModel.UserLoginID = GlobalVariables.UserLoginID;
//						securityShareDetailInsertUpdateModel.CRMEntityID = securityShareDetail.CRMEntityID;
//						securityShareDetailInsertUpdateModel.SecurityEntityID = Convert.ToInt32(GlobalVariables.SecurityEntityID);
//
//						securityShareDetailInsertUpdateModel.NoOfUnit=PNoOfUnit.ToString();
//						securityShareDetailInsertUpdateModel.CurrentValue=PCurrentValue.ToString();
//						//to be added kevin securityShareDetailInsertUpdateModel.HOEntityID=
//						securityShareDetailInsertUpdateModel.OperationFlag = Operationflag;
//
//						var result = await APIRequest.Instance.SecurityShareDetailInsertUpdate (securityShareDetailInsertUpdateModel);
//
//						if (result != null) {
//							GlobalVariables.DisplayMessage = "Gold Details Saved Successfully";
//							MessagingCenter.Send<VMSecurityShareDetail> (this, Strings.ShareDetails_Success);
//						} else {
//							MessagingCenter.Send<VMSecurityShareDetail> (this, Strings.ShareDetails_Failure);
//						}
//						//					securityGoldDetailModelUpdate();
//						securityShareDetail.NoOfUnit=Convert.ToInt32(securityShareDetailInsertUpdateModel.NoOfUnit) ;
//						securityShareDetail.CurrentValue=Convert.ToDouble(securityShareDetailInsertUpdateModel.CurrentValue);
//						ChangeState(false);
//
//					} catch {
//						GlobalVariables.DisplayMessage = "Error... Please try again";
//						MessagingCenter.Send (this, Strings.Display_Message);
//					}
//

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

