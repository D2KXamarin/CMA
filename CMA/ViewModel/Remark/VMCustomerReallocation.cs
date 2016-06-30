using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace CMA
{
	public  class VMCustomerReallocation:INotifyPropertyChanged
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

		public VMCustomerReallocation ()
		{

		}

		private ObservableCollection <StakeholderListModel> _PPriActionStakeholder;

		public ObservableCollection<StakeholderListModel> PPriActionStakeholder {
			get {

				return _PPriActionStakeholder;
			}
			set {
				if (value == null || value.Count == 0) {
					IsEnablePrimary = false;
				} else {
					_PPriActionStakeholder = value;
					OnPropertyChanged ("PPriActionStakeholder");
					IsEnablePrimary = true;

				}
			}

		}

		private bool _IsEnablePrimary = false;

		public bool IsEnablePrimary {
			get{ return _IsEnablePrimary; }
			set {
				_IsEnablePrimary = value;
				OnPropertyChanged ("IsEnablePrimary");
			}
		}

		private ObservableCollection <StakeholderListModel> _PSecActionStakeholder;

		public ObservableCollection<StakeholderListModel> PSecActionStakeholder {
			get {

				return _PSecActionStakeholder;
			}
			set {
				if (value == null || value.Count == 0) {
					IsEnableSecondry = false;
				} else {
					_PSecActionStakeholder = value;
					OnPropertyChanged ("PSecActionStakeholder");
					IsEnableSecondry = true;

				}

			}
		}


		private bool _IsEnableSecondry = false;

		public bool IsEnableSecondry {
			get{ return _IsEnableSecondry; }
			set {
				_IsEnableSecondry = value;
				OnPropertyChanged ("IsEnableSecondry");
			}
		}

		private ObservableCollection <StakeholderListModel> _PInfoStakeholder;

		public ObservableCollection<StakeholderListModel> PInfoStakeholder {
			get {

				return _PInfoStakeholder;
			}
			set {
				if (value == null || value.Count == 0) {
					IsEnableInfo = false;
				} else {
					_PInfoStakeholder = value;
					OnPropertyChanged ("PInfoStakeholder");
					IsEnableInfo = true;
				} 
			}

		}

		public string strPriStakeholder = string.Empty;
		public string strSecStakeholder = string.Empty;
		public string strInfoStakeholder = string.Empty;

		private bool _IsEnableInfo = false;

		public bool IsEnableInfo {
			get{ return _IsEnableInfo; }
			set {
				_IsEnableInfo = value;
				OnPropertyChanged ("IsEnableInfo");
			}
		}

		public async Task LoadStakeholderList ()
		{
			var result = await APIRequest.Instance.GetLoadStakeholderList (new StakeholderListRequestModel {
				CustomerEntityID = GlobalVariables.CustomerEntityID,
				UserLoginID = GlobalVariables.UserLoginID
			});

			if (result != null) {
				try {
					StakeholderListResponseModel responseModelList = JsonConvert.DeserializeObject<StakeholderListResponseModel> (result);
					SQLiteDatabase.Instance.InsertStakHolderDetails (responseModelList);
				} catch (Exception ex) {
				}
			} 
		}

		public void AllotedStakeholderList ()
		{
			try {
				List<StakeholderListModel> ObjStakeholderList = SQLiteDatabase.Instance.GetAllocatedPrimaryActionStakeholder ();
				PPriActionStakeholder = new ObservableCollection<StakeholderListModel> (ObjStakeholderList);

				ObjStakeholderList = SQLiteDatabase.Instance.GetAllocatedSecondaryActionStakeholder ();
				PSecActionStakeholder = new ObservableCollection<StakeholderListModel> (ObjStakeholderList);

				ObjStakeholderList = SQLiteDatabase.Instance.GetAllocatedInfoStakeholder ();
				PInfoStakeholder = new ObservableCollection<StakeholderListModel> (ObjStakeholderList);
			} catch {
			}
		}

		private Command _Save = null;

		public Command Save {
			get {
				return _Save ?? new Command (async delegate(object o) {

					try {
						CustomerReallocationRequestModel customerReallocationRequestModel = new CustomerReallocationRequestModel ();
						customerReallocationRequestModel.CustomerEntityID = GlobalVariables.CustomerEntityID;
						customerReallocationRequestModel.PriActionStakeHolders = strPriStakeholder; 
						customerReallocationRequestModel.SecActionStakeHolders = strSecStakeholder;
						customerReallocationRequestModel.InfoActionStakeHolders = strInfoStakeholder; 

						var result = await APIRequest.Instance.CustomerReAllocationUpdate (customerReallocationRequestModel);

						if (result != null) {
							JObject JsonResult = JObject.Parse (result);
							string ResultValue = JsonResult ["Result"].ToString ();
							if (ResultValue == "1") {
								MessagingCenter.Send (this, Strings.CustomerReallocationSuccess);
							}
						} else {
						
						}

					} catch {
					}
				});

			}
		}
	}
}

