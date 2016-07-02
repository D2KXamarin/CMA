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
	public class VMSecurityShareDetail: INotifyPropertyChanged
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

		public VMSecurityShareDetail ()
		{
			LoadSecurityShareDetail ();
		}

		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private string _PCompanyKey = string.Empty;

		public string PCompanyKey {
			get { return _PCompanyKey; }
			set {
				_PCompanyKey = value;
				OnPropertyChanged ("PCompanyKey");
			}
		}

		private int _PNoOfUnit;

		public	int PNoOfUnit {
			get { 
				return _PNoOfUnit; 
			}
			set { 
				_PNoOfUnit = value;
				OnPropertyChanged ("PNoOfUnit");

			}
		}

		private double _PCurrentValue;

		public	double PCurrentValue {
			get { 
				return _PCurrentValue; 
			}
			set { 
				_PCurrentValue = value;
				OnPropertyChanged ("PCurrentValue");

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

		private bool _IsEnableCompany = false;

		public bool IsEnableCompany {
			get { return _IsEnableCompany; }
			set {
				_IsEnableCompany = value;
				OnPropertyChanged ("IsEnableCompany");
			}
		}

		private bool _IsEnableUnits = false;

		public bool IsEnableUnits {
			get { return _IsEnableUnits; }
			set {
				_IsEnableUnits = value;
				OnPropertyChanged ("IsEnableUnits");
			}
		}

		private bool _IsEnableCurrentValue = false;

		public bool IsEnableCurrentValue {
			get { return _IsEnableCurrentValue; }
			set {
				_IsEnableCurrentValue = value;
				OnPropertyChanged ("IsEnableCurrentValue");
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

		private SecurityShareDetail _PSecurityShareDetail;

		public SecurityShareDetail PSecurityShareDetail {
			get {

				return _PSecurityShareDetail;
			}
			set {

				_PSecurityShareDetail = value;
				OnPropertyChanged ("PSecurityShareDetail");
			}

		}

		void ChangeState(bool flag)
		{
			IsEnableCompany = flag;
			IsEnableUnits = flag;
			IsEnableCurrentValue = flag;
			IsEnableSave = flag;
			IsVisibleCancle = flag;
			IsEnableEdit = !flag;
		}

		SecurityShareDetail securityShareDetail = null;

		public void LoadData(){
			
			PCompanyKey = securityShareDetail.CompanyKey;
			PNoOfUnit = securityShareDetail.NoOfUnit;
			PCurrentValue = securityShareDetail.CurrentValue;

		}

		public async Task LoadSecurityShareDetail ()
		{
			ActivityIndicator = true;
			try {
				
				var result = await APIRequest.Instance.GetSecurityShareDetailsAPI (new SecurityShareDetailRequestModel {
					UserLoginID = GlobalVariables.UserLoginID,
					SecurityEntityID = GlobalVariables.SecurityEntityID
				});

				if (result != null) {
					SecurityShareDetailResponseModel responseModelList = JsonConvert.DeserializeObject<SecurityShareDetailResponseModel> (result);
					securityShareDetail = responseModelList.SecurityShareDetail [0];
					if (securityShareDetail != null) {

						LoadData ();
					}
					IsEnableEdit = true;

				}
			} catch {
				GlobalVariables.DisplayMessage = "Error";
				MessagingCenter.Send<VMSecurityShareDetail> (this, Strings.Display_Message);
			}
			ActivityIndicator = false;
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

