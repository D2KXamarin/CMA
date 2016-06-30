using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CMA
{
	public class VMInstitutionalMemoryDiary : INotifyPropertyChanged
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

		public VMInstitutionalMemoryDiary ()
		{
			//	LoadDefaultStatus ();
		}

		private bool _PAccountLevel = false;

		public bool PAccountLevel {
			get { return _PAccountLevel; }
			set { 
				_PAccountLevel = value;
				OnPropertyChanged ("PAccountLevel");
				if (!PAccountLevel) {
					PAccountID = 0;
				}
				ChangeState ();
			}
		}

		private int _PAccountID;

		public int PAccountID {
			get { return _PAccountID; }
			set {
				_PAccountID = value;
				OnPropertyChanged ("PAccountID");
				ChangeState ();
			}
		}

		private ObservableCollection<AccountDetailModel> _PAccountList = new ObservableCollection<AccountDetailModel> ();

		public ObservableCollection<AccountDetailModel> PAccountList {
			get { return _PAccountList; }
			set {
				_PAccountList = value;
				OnPropertyChanged ("PAccountList");
			}
		}

		private int _PDefaultStatus;

		public int PDefaultStatus {
			get { return _PDefaultStatus; }
			set {
				_PDefaultStatus = value;
				OnPropertyChanged ("PDefaultStatus");
				BindStatusToRemark ();
			}
		}

		public ObservableCollection<DefaultStatusModel> _PDefaultStatusList = new ObservableCollection<DefaultStatusModel> ();

		public ObservableCollection<DefaultStatusModel> PDefaultStatusList {
			get { return _PDefaultStatusList; }
			set {
				_PDefaultStatusList = value;
				OnPropertyChanged ("PDefaultStatusList");
			}
		}

		private string _PRemark = "";

		public string PRemark {
			get { return _PRemark; }
			set {
				_PRemark = value;
				OnPropertyChanged ("PRemark");

				ChangeState ();
			}
		}

		private bool _IsEnableSave = false;

		public bool IsEnableSave {
			get { return _IsEnableSave; }
			set {
				_IsEnableSave = value;
				OnPropertyChanged ("IsEnableSave");
			}
		}

		void ChangeState ()
		{
			if (PRemark.Trim ().Length > 0 && ((PAccountLevel == true && PAccountID > 0) || PAccountLevel == false)) {
				IsEnableSave = true;
			} else {
				IsEnableSave = false;
			}
		}

		public async Task LoadDefaultStatus ()
		{
			if (!SQLiteDatabase.Instance.IsTableExist ("DefaultStatusModel")) {
				var result = await APIRequest.Instance.GetDefaultStatusMaster ();

				if (result != null) {
					var	defaultStatusResponseModel = JsonConvert.DeserializeObject<DefaultStatusResponseModel> (result);

					PDefaultStatusList = new  ObservableCollection<DefaultStatusModel> (defaultStatusResponseModel.DimDefaultStatus);

					SQLiteDatabase.Instance.InsertDefaultStatus (PDefaultStatusList.ToList ());
				}
			} else {
				PDefaultStatusList = new  ObservableCollection<DefaultStatusModel> (SQLiteDatabase.Instance.GetDefaultStatusMaster ());
			}
		}

		public void LoadAccountList ()
		{
			List<AccountDetailModel> listAccountDetailModel = SQLiteDatabase.Instance.GetAccountList ();

			PAccountList = new ObservableCollection<AccountDetailModel> (listAccountDetailModel);
		}

		void BindStatusToRemark ()
		{
			if (PDefaultStatus > 0)
				PRemark = PRemark + " " + PDefaultStatusList [PDefaultStatus - 1].DefaultStatusName;
		}

		public void ClearAll ()
		{
			PAccountLevel = false;
			PAccountID = 0;
			PRemark = "";
			PDefaultStatus = 0;
		}

		private Command _SaveCommand;

		public Command SaveCommand {
			get {
				return _SaveCommand ?? new Command (async delegate(object o) {

					if (!(GlobalVariables.CustomerEntityID == "0" || string.IsNullOrEmpty (GlobalVariables.CustomerEntityID.Trim ()))) {

						try {
							RemarkInsertUpdateRequestModel remarkInsertUpdateRequestModel = new RemarkInsertUpdateRequestModel ();
							remarkInsertUpdateRequestModel.CustomerEntityID = GlobalVariables.CustomerEntityID;
							remarkInsertUpdateRequestModel.AccountEntityID = PAccountLevel ? PAccountList [PAccountID - 1].AccountEntityID : "0";
							remarkInsertUpdateRequestModel.Remark = PRemark;
							remarkInsertUpdateRequestModel.UserLoginID = GlobalVariables.UserLoginID;


						
							var result = await APIRequest.Instance.RemarkInsertUpdate (remarkInsertUpdateRequestModel);


							if (result != null) {
								JObject JsonResult = JObject.Parse (result);
								string ResultValue = JsonResult ["Result"].ToString ();
								if (ResultValue == "1") {
									GlobalVariables.RemarkID = JsonResult ["RemarkID"].ToString ();

									MessagingCenter.Send (this, Strings.RemarkSaveSuccess);
								}

							} else {
								GlobalVariables.DisplayMessage = "Failed... Please try again";
								MessagingCenter.Send (this, Strings.Display_Message);
							}
						} catch {
							GlobalVariables.DisplayMessage = "Error... Please try again";
							MessagingCenter.Send (this, Strings.Display_Message);
						}
					} else {
						GlobalVariables.DisplayMessage = "Please Select Customer from Operation screen";
						MessagingCenter.Send (this, Strings.Display_Message);
					}
				});
			}
		}
	}
}

