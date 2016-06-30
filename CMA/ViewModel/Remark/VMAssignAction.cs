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
	public class VMAssignAction : INotifyPropertyChanged
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

		public VMAssignAction ()
		{

		}

		public string strPriStakeholder = string.Empty;
		public string strSecStakeholder = string.Empty;
		public string strInfoStakeholder = string.Empty;
		public int CurrentIndex = 0;
		public string CurrentMode = string.Empty;


		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}



		private string _PWhatIsToBeDone = string.Empty;

		public string PWhatIsToBeDone {
			get { return _PWhatIsToBeDone; }
			set {
				_PWhatIsToBeDone = value;
				OnPropertyChanged ("PWhatIsToBeDone");
			}
		}

		private DateTime _PByWhenDt = DateTime.Now;

		public DateTime PByWhenDt {
			get {
				return	_PByWhenDt;
			}
			set { 
				_PByWhenDt = value;
				OnPropertyChanged ("PByWhenDt");
			}
		}

		private int _PASIndex;

		public	int PASIndex {
			get { 
				return _PASIndex; 
			}
			set { 
				_PASIndex = value;
				OnPropertyChanged ("PASIndex");

			}
		}

		private int _SASIndex;

		public	int SASIndex {
			get { 
				return _SASIndex; 
			}
			set { 
				_SASIndex = value;
				OnPropertyChanged ("SASIndex");

			}
		}

		private int _IASIndex;

		public	int IASIndex {
			get { 
				return _IASIndex; 
			}
			set { 
				_IASIndex = value;
				OnPropertyChanged ("IASIndex");

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

		private bool _IsEnableSecondry = false;

		public bool IsEnableSecondry {
			get{ return _IsEnableSecondry; }
			set {
				_IsEnableSecondry = value;
				OnPropertyChanged ("IsEnableSecondry");
			}
		}

		private bool _IsEnableInfo = false;

		public bool IsEnableInfo {
			get{ return _IsEnableInfo; }
			set {
				_IsEnableInfo = value;
				OnPropertyChanged ("IsEnableInfo");
			}
		}

		private bool _IsEnablePrev = false;

		public bool IsEnablePrev {
			get { return _IsEnablePrev; }
			set {
				_IsEnablePrev = value;
				OnPropertyChanged ("IsEnablePrev");
			}
		}

		private bool _IsVisiblePrev = false;

		public bool IsVisiblePrev {
			get { return _IsVisiblePrev; }
			set {
				_IsVisiblePrev = value;
				OnPropertyChanged ("IsVisiblePrev");
			}
		}

		private bool _IsEnableNext = false;

		public bool IsEnableNext {
			get { return _IsEnableNext; }
			set {
				_IsEnableNext = value;
				OnPropertyChanged ("IsEnableNext");
			}
		}

		private bool _IsVisibleNext = false;

		public bool IsVisibleNext {
			get { return _IsVisibleNext; }
			set {
				_IsVisibleNext = value;
				OnPropertyChanged ("IsVisibleNext");
			}
		}

		private bool _IsEnableAddMore = false;

		public bool IsEnableAddMore {
			get { return _IsEnableAddMore; }
			set {
				_IsEnableAddMore = value;
				OnPropertyChanged ("IsEnableAddMore");
			}
		}

		private bool _IsVisibleAddMore = false;

		public bool IsVisibleAddMore {
			get { return _IsVisibleAddMore; }
			set {
				_IsVisibleAddMore = value;
				OnPropertyChanged ("IsVisibleAddMore");
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

		private bool _IsVisibleEdit = false;

		public bool IsVisibleEdit {
			get { return _IsVisibleEdit; }
			set {
				_IsVisibleEdit = value;
				OnPropertyChanged ("IsVisibleEdit");
			}
		}

		private bool _IsEnableDelete = false;

		public bool IsEnableDelete {
			get { return _IsEnableDelete; }
			set {
				_IsEnableDelete = value;
				OnPropertyChanged ("IsEnableDelete");
			}
		}

		private bool _IsVisibleDelete = false;

		public bool IsVisibleDelete {
			get { return _IsVisibleDelete; }
			set {
				_IsVisibleDelete = value;
				OnPropertyChanged ("IsVisibleDelete");
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

		private bool _IsVisibleSave = false;

		public bool IsVisibleSave {
			get { return _IsVisibleSave; }
			set {
				_IsVisibleSave = value;
				OnPropertyChanged ("IsVisibleSave");
			}
		}

		private bool _IsEnableCancel = false;

		public bool IsEnableCancel {
			get { return _IsEnableCancel; }
			set {
				_IsEnableCancel = value;
				OnPropertyChanged ("IsEnableCancel");
			}
		}

		private bool _IsVisibleCancel = false;

		public bool IsVisibleCancel {
			get { return _IsVisibleCancel; }
			set {
				_IsVisibleCancel = value;
				OnPropertyChanged ("IsVisibleCancel");
			}
		}

		private bool _IsEnableWhat = false;

		public bool IsEnableWhat {
			get { return _IsEnableWhat; }
			set {
				_IsEnableWhat = value;
				OnPropertyChanged ("IsEnableWhat");
			}
		}

		private bool _IsEnableWhen = false;

		public bool IsEnableWhen {
			get { return _IsEnableWhen; }
			set {
				_IsEnableWhen = value;
				OnPropertyChanged ("IsEnableWhen");
			}
		}

		private bool _IsEnablePriBtn = false;

		public bool IsEnablePriBtn {
			get { return _IsEnablePriBtn; }
			set {
				_IsEnablePriBtn = value;
				OnPropertyChanged ("IsEnablePriBtn");
			}
		}

		private bool _IsEnableSecBtn = false;

		public bool IsEnableSecBtn {
			get { return _IsEnableSecBtn; }
			set {
				_IsEnableSecBtn = value;
				OnPropertyChanged ("IsEnableSecBtn");
			}
		}

		private bool _IsEnableInfoBtn = false;

		public bool IsEnableInfoBtn {
			get { return _IsEnableInfoBtn; }
			set {
				_IsEnableInfoBtn = value;
				OnPropertyChanged ("IsEnableInfoBtn");
			}
		}

		private ObservableCollection <AssignedStakeholder> _PPriActionStakeholder;

		public ObservableCollection<AssignedStakeholder> PPriActionStakeholder {
			get {

				return _PPriActionStakeholder;
			}
			set {
				if (value == null || value.Count == 0) {
					IsEnablePrimary = false;
				} else {
					IsEnablePrimary = true;
				}
				_PPriActionStakeholder = value;
				OnPropertyChanged ("PPriActionStakeholder");
			}

		}

		private ObservableCollection <AssignedStakeholder> _PSecActionStakeholder;

		public ObservableCollection<AssignedStakeholder> PSecActionStakeholder {
			get {

				return _PSecActionStakeholder;
			}
			set {
				if (value == null || value.Count == 0) {
					IsEnableSecondry = false;
				} else {
					
					IsEnableSecondry = true;

				}
				_PSecActionStakeholder = value;
				OnPropertyChanged ("PSecActionStakeholder");
			}
		}

		private ObservableCollection <AssignedStakeholder> _PInfoStakeholder;

		public ObservableCollection<AssignedStakeholder> PInfoStakeholder {
			get {

				return _PInfoStakeholder;
			}
			set {
				if (value == null || value.Count == 0) {
					IsEnableInfo = false;
				} else {
					
					IsEnableInfo = true;
				} 
				_PInfoStakeholder = value;
				OnPropertyChanged ("PInfoStakeholder");
			}

		}



		private AssignActionResponseModel _AssignActionDetails = new AssignActionResponseModel ();

		public AssignActionResponseModel AssignActionDetails {
			get { 
				return _AssignActionDetails;
			}
			set {
				_AssignActionDetails = value;

				OnPropertyChanged ("AssignActionDetails");
			}
		}

		private ObservableCollection<AssignActionModel> _PAssignActionList;

		public ObservableCollection<AssignActionModel> PAssignActionList {
			get { 
				return _PAssignActionList;
			}
			set {
				_PAssignActionList = value;
				OnPropertyChanged ("PAssignActionList");
			}
		}






		public async Task LoadAssignActionStakeholderList ()
		{
			var result = await APIRequest.Instance.GetLoadAssignActionStakeholderList (new AssignActionRequestModel {
				CustomerEntityID = GlobalVariables.CustomerEntityID,
				RemarkID = GlobalVariables.RemarkID
			});

			if (result != null) {
				try {
					AssignActionResponseModel responseModelList = JsonConvert.DeserializeObject<AssignActionResponseModel> (result);
					SQLiteDatabase.Instance.InsertAssignActionStakHolderDetails (responseModelList);

					if (responseModelList.AssignAction == null) {
						GlobalVariables.AssignActionID = 1;
						CurrentMode = "ADD";
					} else {
						PAssignActionList = new ObservableCollection<AssignActionModel> (responseModelList.AssignAction);
						//CurrentIndex = 0;
						CurrentMode = "EDIT";
					}
				} catch {
				}
			} 
		}

		public void LoadAssignAction ()
		{
			
			if (PAssignActionList != null) {
				GlobalVariables.AssignActionID = PAssignActionList [CurrentIndex].AssignActionID;
				PWhatIsToBeDone = PAssignActionList [CurrentIndex].WhatisToBeDone;
				PByWhenDt = Convert.ToDateTime (PAssignActionList [CurrentIndex].ByWhen);



				IsEnablePrev = CurrentIndex == 0 ? false : true;
				IsEnableNext = PAssignActionList.Count - 1 == CurrentIndex ? false : true;

				IsVisiblePrev = true;
				IsVisibleNext = true;
				IsVisibleEdit = true;
				IsVisibleDelete = true;

				if (CurrentMode == "EDIT_STAKE")
					ChangeControlsState (true);
				else
					ChangeControlsState (false);

			} else {
				IsVisibleAddMore = true;
				ChangeControlsState (true);
			}
			AssignedStakeholderList ();
			IsVisibleSave = true;
			IsVisibleCancel = true;
		}

		public void AssignedStakeholderList ()
		{
			try {
				List<AssignedStakeholder> ObjAssignedStakeholderList = SQLiteDatabase.Instance.GetAssignedPrimaryActionStakeholder ();
				PPriActionStakeholder = new ObservableCollection<AssignedStakeholder> (ObjAssignedStakeholderList);

				ObjAssignedStakeholderList = SQLiteDatabase.Instance.GetAssignedSecondaryActionStakeholder ();
				PSecActionStakeholder = new ObservableCollection<AssignedStakeholder> (ObjAssignedStakeholderList);

				ObjAssignedStakeholderList = SQLiteDatabase.Instance.GetAssignedInfoActionStakeholder ();
				PInfoStakeholder = new ObservableCollection<AssignedStakeholder> (ObjAssignedStakeholderList);
			} catch {
			}
		}

		public void ChangeControlsState (bool flag)
		{
			IsEnableWhat = flag;
			IsEnableWhen = flag;
			IsEnablePriBtn = flag;
			IsEnableSecBtn = flag;
			IsEnableInfoBtn = flag;
			IsEnableSave = flag;
			IsEnableCancel = flag;

			if (!flag) {
				CurrentMode = PAssignActionList == null ? "ADD" : "EDIT";
			}

			if (CurrentMode == "ADD") {
				IsEnableAddMore = !flag;
			} else if(GlobalVariables.RemarkStatusKey==20){
					IsEnableEdit = !flag;
					IsEnableDelete = !flag;
				}
		}

		public void ClearAll()
		{
			PWhatIsToBeDone = "";
			PPriActionStakeholder.Clear();
			PSecActionStakeholder.Clear();
			PInfoStakeholder.Clear();
		}

		public bool IsValid ()
		{
			if (string.IsNullOrEmpty (PWhatIsToBeDone.Trim ())) {
				GlobalVariables.DisplayMessage = "Please Enter WhatIsToBeDone";
				return false;
			} else if (PPriActionStakeholder.Count == 0) {
				GlobalVariables.DisplayMessage = "Please Select Minimum One Primary Stakeholder";
				return false;
			} else if (PSecActionStakeholder.Count == 0) {
				GlobalVariables.DisplayMessage = "Please Select Minimum One Secondary Stakeholder";
				return false;
			} else if (PInfoStakeholder.Count == 0) {
				GlobalVariables.DisplayMessage = "Please Select Minimum One Info Stakeholder";
				return false;
			}
			else if (PByWhenDt < DateTime.Today) {
				GlobalVariables.DisplayMessage = "Assign action date cannot be less than current date";
				return false;
			}

			return true;
		}

		private Command _Save = null;

		public Command Save {
			get {
				return _Save ?? new Command (async delegate(object o) {

					try {

						if (IsValid ()) {
							AssignActionInsertUpdateRequestModel assignActionInsertUpdateRequestModel = new AssignActionInsertUpdateRequestModel ();
							assignActionInsertUpdateRequestModel.RemarkID = GlobalVariables.RemarkID;
							assignActionInsertUpdateRequestModel.AssignActionID = GlobalVariables.AssignActionID;
							assignActionInsertUpdateRequestModel.WhatIsToBeDone = PWhatIsToBeDone;
							assignActionInsertUpdateRequestModel.ByWhen = PByWhenDt.Date.ToString ("yyyy-MM-dd");
							assignActionInsertUpdateRequestModel.PrimaryActionStakeHolder = strPriStakeholder; 
							assignActionInsertUpdateRequestModel.SecondaryActionStakeHolder = strSecStakeholder;
							assignActionInsertUpdateRequestModel.InfoStakeHolder = strInfoStakeholder; 

							var result = await APIRequest.Instance.AssignActionInsertUpdate (assignActionInsertUpdateRequestModel);

							if (result != null) {
								JObject JsonResult = JObject.Parse (result);
								string ResultValue = JsonResult ["Result"].ToString ();
								if (ResultValue == "1") {
									MessagingCenter.Send (this, Strings.AssignActionInsertUpdateSuccess);
								} else {
									GlobalVariables.DisplayMessage = "Error... Please try again";
									MessagingCenter.Send (this, Strings.Display_Message);
								}
							} else {
								GlobalVariables.DisplayMessage = "Error... Please try again";
								MessagingCenter.Send (this, Strings.Display_Message);
							}

							ChangeControlsState (false);
						} else {
							MessagingCenter.Send (this, Strings.Display_Message);
						}

					} catch {
						GlobalVariables.DisplayMessage = "Error... Please try again";
						MessagingCenter.Send (this, Strings.Display_Message);
					}


				});
			}
		}



		private Command _EditCommand = null;

		public Command EditCommand {
			get {
				return  _EditCommand ?? new Command (delegate(object o) {
					ChangeControlsState (true);
				});
			}
		}

		private Command _CancelCommand = null;

		public Command CancelCommand {
			get {
				return  _CancelCommand ?? new Command (delegate(object o) {

					ChangeControlsState (false);
				});
			}
		}
	}
}


