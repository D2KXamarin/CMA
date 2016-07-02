using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace CMA
{
	public class VMGoldDetals:INotifyPropertyChanged
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

		public VMGoldDetals ()
		{

		}
		public int SecurityEntityId ;



		static string _PQuantity = string.Empty;

		public string PQuantity {
			get { return _PQuantity; }
			set {
				_PQuantity = value;
				OnPropertyChanged ("PQuantity");
			}
		}


		private bool _IsEnableQuantity = false;

		public bool IsEnableQuantity {
			get{ return _IsEnableQuantity; }
			set {
				_IsEnableQuantity = value;
				OnPropertyChanged ("IsEnableQuantity");
			}
		}

		static string _PCarat = string.Empty;

		public string PCarat {
			get { return _PCarat; }
			set {
				_PCarat = value;
				OnPropertyChanged ("PCarat");
			}
		}

		private bool _IsEnableCarat = false;

		public bool IsEnableCarat {
			get{ return _IsEnableCarat; }
			set {
				_IsEnableCarat = value;
				OnPropertyChanged ("IsEnableCarat");
			}
		}

		static string _PMargin = string.Empty;

		public string PMargin {
			get { return _PMargin; }
			set {
				_PMargin = value;
				OnPropertyChanged ("PMargin");
			}
		}

		private bool _IsEnableMargin = false;

		public bool IsEnableMargin {
			get{ return _IsEnableMargin; }
			set {
				_IsEnableMargin = value;
				OnPropertyChanged ("IsEnableMargin");
			}
		}

		static string _PNature = string.Empty;

		public string PNature {
			get { return _PNature; }
			set {
				_PNature = value;
				OnPropertyChanged ("PNature");
			}
		}

		private bool _IsEnableNature = false;

		public bool IsEnableNature {
			get{ return _IsEnableNature; }
			set {
				_IsEnableNature = value;
				OnPropertyChanged ("IsEnableNature");
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
			IsEnableQuantity = flag;
			IsEnableCarat = flag;
			IsEnableMargin = flag;
			IsEnableNature = flag;
			IsEnableSave = flag;
			IsVisibleCancle = flag;
			IsEnableEdit = !flag;
		}

		SecurityGoldDetailModel securityGoldDetailModel = null;

		public void LoadData(){
			PQuantity =securityGoldDetailModel.Quantity;
			PCarat =securityGoldDetailModel.Karat;
			PMargin =securityGoldDetailModel.Margin;
		}

		public async Task LoadGoldDetails (){


			var result = await APIRequest.Instance.GetSecurityGoldDetail (new SecurityGoldDetailRequestModel {
				UserLoginID = GlobalVariables.UserLoginID,
				SecurityEntityID =SecurityEntityId
			});

			if (result != null) {
				SecurityGoldDetailResponceModel responseModelList = JsonConvert.DeserializeObject<SecurityGoldDetailResponceModel>(result);
				securityGoldDetailModel = responseModelList.SecurityGoldDetail [0];
			}

			if (securityGoldDetailModel != null) {
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

