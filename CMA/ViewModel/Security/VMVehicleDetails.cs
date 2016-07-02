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
		public int SecurityEntityId ;
		public int VehicleEntityID=0;



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

		static string _PInsurance = string.Empty;

		public string PInsurance {
			get { return _PInsurance; }
			set {
				_PInsurance = value;
				OnPropertyChanged ("PInsurance");
			}
		}
		private bool _IsEnableInsurance = false;

		public bool IsEnableInsurance {
			get{ return _IsEnableInsurance; }
			set {
				_IsEnableInsurance = value;
				OnPropertyChanged ("IsEnableInsurance");
			}
		}

		private bool _IsEnableEdit = false;

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

		void ChangeState(bool flag)
		{
			IsEnableMake = flag;
			IsEnableModel = flag;
			IsEnableInsurance = flag;
			IsEnableType = flag;
			IsEnableSave = flag;
			IsEnableVehicle = flag;
			IsVisibleCancle = flag;
			IsEnableEdit = !flag;
		}

		SecurityVehicleDetailModel securityVehicleDetailModel =null;

		public void LoadData(){
			VehicleEntityID = securityVehicleDetailModel.VehicleEntityID;
			PMake = securityVehicleDetailModel.Make;
			PModel = securityVehicleDetailModel.Model;
			PType = securityVehicleDetailModel.Type;
			PVehicle = securityVehicleDetailModel.VehicleNo;
			PInsurance = securityVehicleDetailModel.InsuranceCompany;
		}


		public async Task LoadVehicelDetails (){
			

			var result = await APIRequest.Instance.GetSecurityVehicleDetail (new SecurityVehicleDetailRequestModel {
				UserLoginID = GlobalVariables.UserLoginID,
				SecurityEntityID =SecurityEntityId
			});

			if (result != null) {
				SecurityVehicleDetailResponceModel responseModelList = JsonConvert.DeserializeObject<SecurityVehicleDetailResponceModel>(result);
				securityVehicleDetailModel = responseModelList.SecurityVehicleDetail [0];
			}

			if (securityVehicleDetailModel != null) {
				
				LoadData ();
			}
			IsEnableEdit = true;
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

