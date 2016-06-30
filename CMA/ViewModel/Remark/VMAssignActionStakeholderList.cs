using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using XLabs;

namespace CMA
{
	public class VMAssignActionStakeholderList:INotifyPropertyChanged
	{
		public VMAssignActionStakeholderList ()
		{
			GetAssignActionStakeholderList();
			//UpdateAssignedAction (GlobalVariables.EventID);
		}

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

		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}



		private ObservableCollection<StakeholderListModel> _PStakeholderList;

		public ObservableCollection<StakeholderListModel> PStakeholderList {
			get { 
				return _PStakeholderList;
			}
			set {
				_PStakeholderList = value;
				OnPropertyChanged ("PStakeholderList");
			}
		}

		private ObservableCollection<AllocatedStakeholder> _PAllocatedStakeholderList;

		public ObservableCollection<AllocatedStakeholder> PAllocatedStakeholderList {
			get { 
				return _PAllocatedStakeholderList;
			}
			set {
				_PAllocatedStakeholderList = value;
				OnPropertyChanged ("PAllocatedStakeholderList");
			}
		}

		private ObservableCollection<AssignedStakeholder> _PAssignedStakeholderList;

		public ObservableCollection<AssignedStakeholder> PAssignedStakeholderList {
			get { 
				return _PAssignedStakeholderList;
			}
			set {
				_PAssignedStakeholderList = value;
				OnPropertyChanged ("PAssignedStakeholderList");
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
				} catch {
				}
			} 
		}

		public void GetAssignActionStakeholderList ()
		{
			List<AllocatedStakeholder> allocatedStakeholder = new List<AllocatedStakeholder> ();
			if (GlobalVariables.StakeholderType == "P") {
				allocatedStakeholder = SQLiteDatabase.Instance.GetAllocatedAssignPrimaryActionStakeholder (); 
			} else if (GlobalVariables.StakeholderType == "S") {
				allocatedStakeholder = SQLiteDatabase.Instance.GetAllocatedAssignSecondaryActionStakeholder ();
			} else {
				allocatedStakeholder = SQLiteDatabase.Instance.GetAllocatedAssignInfoActionStakeholder ();
			}

			PAllocatedStakeholderList = new ObservableCollection<AllocatedStakeholder> (allocatedStakeholder);



			List<AssignedStakeholder> assignedStakeholder = new List<AssignedStakeholder> ();
			if (GlobalVariables.StakeholderType == "P") {
				assignedStakeholder = SQLiteDatabase.Instance.GetAssignedPrimaryActionStakeholder (); 
			} else if (GlobalVariables.StakeholderType == "S") {
				assignedStakeholder = SQLiteDatabase.Instance.GetAssignedSecondaryActionStakeholder ();
			} else {
				assignedStakeholder = SQLiteDatabase.Instance.GetAssignedInfoActionStakeholder ();
			}

			PAssignedStakeholderList = new ObservableCollection<AssignedStakeholder> (assignedStakeholder);
			foreach (AssignedStakeholder currobjAssign in PAssignedStakeholderList) {
				foreach (AllocatedStakeholder currobjAllocate in PAllocatedStakeholderList) {
					if (currobjAllocate.UserLoginID == currobjAssign.UserLoginID) {
						currobjAllocate.IsAssigned = true;
					}
				}
			}
		}

		private Command _Save = null;

		public Command Save {
			get {
				return _Save ?? new Command (delegate(object o) {

					List<AssignedStakeholder> listAssignActionModel = new List<AssignedStakeholder> ();
					foreach (AllocatedStakeholder AllocatedStakeholder in PAllocatedStakeholderList.Where(obj => obj.IsAssigned == true)) {
						AssignedStakeholder objAssignActionModel = new AssignedStakeholder ();
						objAssignActionModel.EventID = GlobalVariables.AssignActionID;
						objAssignActionModel.UserLoginID = AllocatedStakeholder.UserLoginID;
						objAssignActionModel.UserName = AllocatedStakeholder.UserName;
						objAssignActionModel.StakeholderType = GlobalVariables.StakeholderType;

						listAssignActionModel.Add(objAssignActionModel);
					}

					SQLiteDatabase.Instance.InsertUpdateAssignActionStakHolder (listAssignActionModel); 

					MessagingCenter.Send<VMAssignActionStakeholderList> (this, Strings.AssignActionStakeholderListSuccess);
				});
			}
		}

	}
}







