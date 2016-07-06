using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace CMA
{
	public class VMVehicleDetails:INotifyPropertyChanged
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

		public VMVehicleDetails ()
		{
			//			LoadVehicelDetails ();
		}

		public int SecurityEntityId;
		public string VehicleEntityID;



		static string _PMake = string.Empty;

		public string PMake {
			get { return _PMake; }
			set {
				_PMake = value;
				OnPropertyChanged ("PMake");
			}
		}

		private bool _IsEnableMake = false;

		public bool IsEnableMake {
			get{ return _IsEnableMake; }
			set {
				_IsEnableMake = value;
				OnPropertyChanged ("IsEnableMake");
			}
		}

		static string _PModel = string.Empty;

		public string PModel {
			get { return _PModel; }
			set {
				_PModel = value;
				OnPropertyChanged ("PModel");
			}
		}

		private bool _IsEnableModel = false;

		public bool IsEnableModel {
			get{ return _IsEnableModel; }
			set {
				_IsEnableModel = value;
				OnPropertyChanged ("IsEnableModel");
			}
		}

		static string _PType = string.Empty;

		public string PType {
			get { return _PType; }
			set {
				_PType = value;
				OnPropertyChanged ("PType");
			}
		}

		private bool _IsEnableType = false;

		public bool IsEnableType {
			get{ return _IsEnableType; }
			set {
				_IsEnableType = value;
				OnPropertyChanged ("IsEnableType");
			}
		}

		static string _PVehicle = string.Empty;

		public string PVehicle {
			get { return _PVehicle; }
			set {
				_PVehicle = value;
				OnPropertyChanged ("PVehicle");
			}
		}

		private bool _IsEnableVehicle = false;

		public bool IsEnableVehicle {
			get{ return _IsEnableVehicle; }
			set {
				_IsEnableVehicle = value;
				OnPropertyChanged ("IsEnableVehicle");
			}
		}

		static string _PInsuranceCompany = string.Empty;

		public string PInsuranceCompany {
			get { return _PInsuranceCompany; }
			set {
				_PInsuranceCompany = value;
				OnPropertyChanged ("PInsuranceCompany");
			}
		}

		private bool _IsEnableInsuranceCompany = false;

		public bool IsEnableInsuranceCompany {
			get{ return _IsEnableInsuranceCompany; }
			set {
				_IsEnableInsuranceCompany = value;
				OnPropertyChanged ("IsEnableInsuranceCompany");
			}
		}

		private bool _IsEnableEdit = true;

		public bool IsEnableEdit {
			get{ return _IsEnableEdit; }
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

		void ChangeState (bool flag)
		{
			IsEnableMake = flag;
			IsEnableModel = flag;
			IsEnableInsuranceCompany = flag;
			IsEnableType = flag;
			IsEnableSave = flag;
			IsEnableVehicle = flag;
			IsVisibleCancle = flag;
			IsEnableEdit = !flag;
		}

		SecurityVehicleDetailModel securityVehicleDetailModel = null;
		int Operationflag = 0;

		public void LoadData ()
		{
			VehicleEntityID = securityVehicleDetailModel.VehicleEntityID;
			PMake = securityVehicleDetailModel.Make;
			PModel = securityVehicleDetailModel.Model;
			PType = securityVehicleDetailModel.Type;
			PVehicle = securityVehicleDetailModel.VehicleNo;
			PInsuranceCompany = securityVehicleDetailModel.InsuranceCompany;

		}


		public async Task LoadVehicelDetails ()
		{

			try {
				var result = await APIRequest.Instance.GetSecurityVehicleDetail (new SecurityVehicleDetailRequestModel {
					UserLoginID = GlobalVariables.UserLoginID,
					SecurityEntityID = SecurityEntityId
				});

				if (result != null) {
					SecurityVehicleDetailResponceModel responseModelList = JsonConvert.DeserializeObject<SecurityVehicleDetailResponceModel> (result);

					securityVehicleDetailModel = responseModelList.SecurityVehicleDetail [0];
				}
			} catch {
			}
			if (securityVehicleDetailModel != null) {
				LoadData ();
			} else {
			}
			ChangeState (false);	
		}

		private Command _Edit = null;

		public Command Edit {
			get {
				return _Edit ?? new Command (async delegate(object o) {
					ChangeState (true);				
				});
			}
		}

		private Command _Save = null;

		public Command Save {
			get {
				return _Save ?? new Command (async delegate (object o) {

					SecurityVehicleDetailInsertUpdateModel securityVehicleDetailInsertUpdateModel = new SecurityVehicleDetailInsertUpdateModel ();

					securityVehicleDetailInsertUpdateModel.CustomerEntityID = securityVehicleDetailModel.CustomerEntityID;
					securityVehicleDetailInsertUpdateModel.AccountEntityID = securityVehicleDetailModel.AccountEntityId;



					securityVehicleDetailInsertUpdateModel.UserLoginID = GlobalVariables.UserLoginID;

					securityVehicleDetailInsertUpdateModel.SecurityEntityID = SecurityEntityId;
					securityVehicleDetailInsertUpdateModel.VehicleEntityID = VehicleEntityID;


					securityVehicleDetailInsertUpdateModel.Make = PMake;
					securityVehicleDetailInsertUpdateModel.Model = PModel;
					securityVehicleDetailInsertUpdateModel.Type = PType;
					securityVehicleDetailInsertUpdateModel.VehicleNo = PVehicle;
					securityVehicleDetailInsertUpdateModel.InsuranceCompany =PInsuranceCompany;




					var result = await APIRequest.Instance.SecurityVehicleDetailInsertUpdate (securityVehicleDetailInsertUpdateModel);

					if (result != null) {
						GlobalVariables.DisplayMessage = "Vehicle Details Saved Successfully";
						MessagingCenter.Send<VMVehicleDetails> (this, Strings.VehicleDetails_Success);
					} else {
						MessagingCenter.Send<VMVehicleDetails> (this, Strings.VehicleDetails__FAILURE);
					}

					securityVehicleDetailModel.Make=securityVehicleDetailInsertUpdateModel.Make;
					securityVehicleDetailModel.Model=securityVehicleDetailInsertUpdateModel.Model;
					securityVehicleDetailModel.Type=securityVehicleDetailInsertUpdateModel.Type ;
					securityVehicleDetailModel.VehicleNo=securityVehicleDetailInsertUpdateModel.VehicleNo;
					securityVehicleDetailModel.InsuranceCompany=securityVehicleDetailInsertUpdateModel.InsuranceCompany;
					//				
					ChangeState (false);

				});
			}
		}

		private Command _Cancel = null;

		public Command Cancel {
			get {
				return _Cancel ?? new Command (async delegate(object o) {
					LoadData ();
					ChangeState (false);
				});
			}
		}


	}
}

